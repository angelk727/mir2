using System.Drawing;
using Server.MirDatabase;
using Server.MirObjects;
using S = ServerPackets;

namespace Server.MirEnvir
{
    public sealed class ValorBattlefieldSystem
    {
        private enum Phase { Idle, Registration, Active, Settling }

        private sealed class Member
        {
            public PlayerObject Player;
            public ValorTeam Team;
            public AttackMode PreviousMode;
            public int ReturnMap;
            public Point ReturnLocation;
            public int Personal, Kills, Deaths;
            public long ReviveAt, PreviousBrownTime;
        }

        private sealed class ValorMonument : MonsterObject
        {
            private readonly int _monumentHealth;

            protected override bool CanMove => false;
            protected override bool CanAttack => false;
            protected override bool CanRegen => false;

            public ValorMonument(MonsterInfo info, int monumentHealth) : base(info)
            {
                _monumentHealth = monumentHealth;
                Direction = MirDirection.Up;
            }

            public override void RefreshAll()
            {
                base.RefreshAll();
                Stats[Stat.HP] = _monumentHealth;
            }

            protected override void Attack() { }
            protected override void FindTarget() { }
            protected override void ProcessTarget() { }
            protected override void ProcessSearch() { }
            protected override void ProcessRoam() { }
            protected override void ProcessRegen() { }

            public override void Turn(MirDirection direction) { }

            public override bool Walk(MirDirection direction) => false;

            public override int Pushed(MapObject pusher, MirDirection direction, int distance) => 0;

            public override void ApplyPoison(Poison poison, MapObject caster = null,
                bool noResist = false, bool ignoreDefence = true)
            { }

            public override void PoisonDamage(int amount, MapObject attacker) { }

            public override int Struck(int damage,
                DefenceType type = DefenceType.ACAgility) => 0;

            private int Hit(MapObject attacker, int damage, DefenceType type)
            {
                if (attacker == null || Dead || !Envir.Main.Valor.CanAttackObjective(this, attacker))
                    return 0;

                Envir.Main.Valor.RecordDamage(this, attacker);

                Broadcast(new S.ObjectStruck
                {
                    ObjectID = ObjectID,
                    AttackerID = attacker.ObjectID,
                    Direction = Direction,
                    Location = CurrentLocation
                });

                ChangeHP(-1);
                return 1;
            }

            public override int Attacked(HumanObject attacker, int damage,
                DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
            {
                int result = Hit(attacker, damage, type);

                if (result != 0)
                {
                    if (damageWeapon)
                        attacker.DamageWeapon();

                    attacker.GatherElement();
                }

                return result;
            }

            public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility) => Hit(attacker, damage, type);
        }

        private sealed class Objective
        {
            public string Name;
            public ValorTeam Side;
            public bool Regular, Boss;
            public bool BossSpawned;
            public Point Location;
            public MonsterObject Monster;
            public ValorTeam Owner;
            public long RespawnAt;
            public MapObject LastDamager;
        }

        private readonly object _sync = new object();

        private readonly List<PlayerObject> _applications = new List<PlayerObject>();

        private readonly Dictionary<PlayerObject, Member> _members = new Dictionary<PlayerObject, Member>();

        private readonly Objective[] _monuments =
        {
          new Objective { Name = "太阳碑", Location = new Point(201, 198) },
          new Objective { Name = "月亮碑", Location = new Point(77, 196) },
          new Objective { Name = "闪电碑", Location = new Point(324, 207) }
        };

        private static readonly string[] WaveMonsterNames =
        {
            "红野猪0",
            "黑野猪0",
            "月魔蜘蛛0",
            "钢牙蜘蛛0",
            "浪人鬼0",
            "腐蚀鬼0",
            "冰牛魔0",
            "牛头魔0",
            "火焰沃玛0",
            "沃玛战士0",
            "紫虎虫0",
            "魔眼虫0",
            "大老鼠0",
            "楔蛾0",
            "邪恶钳虫0",
            "黑色恶蛆0",
            "巨型蠕虫0",
            "跳跳蜂0"
        };

        private readonly Objective[] _bosses =
        {
            new Objective
            {
                Name = "邪牛天王0",
                Side = ValorTeam.Red,
                Boss = true,
                Location = new Point(339, 122)
            },

            new Objective
            {
                Name = "邪牛天王0",
                Side = ValorTeam.Blue,
                Boss = true,
                Location = new Point(64, 281)
            }
        };

        private readonly List<Objective> _waveMonsters =
            new List<Objective>();

        private readonly ValorHonorStore _honor =
            new ValorHonorStore();

        private ValorSettings _settings;
        private Map _map;
        private Phase _phase;

        private long _ends;
        private long _scoreAt;
        private long _statusAt;
        private long _settleAt;
        private long _secondWaveAt;

        private int _waveNumber;
        private int _redScore;
        private int _blueScore;

        private ValorTeam _winner;

        private bool _returning;

        private static Envir World => Envir.Main;

        private static MessageQueue MessageQueue =>
            MessageQueue.Instance;

        private static readonly Point BlueSpawn =
            new Point(50, 42);

        private static readonly Point RedSpawn =
            new Point(354, 360);

        private static Point Spawn(ValorTeam team)
        {
            return team == ValorTeam.Red
                ? RedSpawn
                : BlueSpawn;
        }

        private static string TeamName(ValorTeam team)
        {
            if (team == ValorTeam.Red)
                return "红方";

            if (team == ValorTeam.Blue)
                return "蓝方";

            return "无";
        }

        public bool HasPlayer(PlayerObject player)
        {
            lock (_sync)
                return player != null && (_applications.Contains(player) || _members.ContainsKey(player));
        }

        public bool Contains(PlayerObject player)
        {
            lock (_sync) return player != null && _members.ContainsKey(player);
        }

        public bool IsMap(Map map)
        {
            lock (_sync)
            {
                if (map == null)
                    return false;

                string mapName = Path.GetFileNameWithoutExtension(map.Info.FileName);
                string battleMapName = _settings?.MapFileName ?? "valor";

                if (!string.Equals(mapName, battleMapName, StringComparison.OrdinalIgnoreCase))
                    return false;

                return true;
            }
        }

        public bool IsParticipant(PlayerObject player)
        {
            lock (_sync)
            {
                return Contains(player) && player.Node != null && player.CurrentMap == _map;
            }
        }

        public bool CanEnter(PlayerObject player, Map map)
        {
            lock (_sync)
            {
                return !IsMap(map) || IsGMVisitor(player) || ((_phase == Phase.Active || _phase == Phase.Settling) && Contains(player));
            }
        }

        private static bool IsGMVisitor(PlayerObject player)
        {
            return player != null && player.IsGM;
        }

        public bool TryGetRelationship(PlayerObject first, PlayerObject second, out bool hostile)
        {
            lock (_sync)
            {
                hostile = false;

                //if (first == null || second == null || !IsMap(first.CurrentMap) || !IsMap(second.CurrentMap))
                if (first == null || second == null || (!IsMap(first.CurrentMap) && !IsMap(second.CurrentMap)))
                    return false;

                hostile = _phase == Phase.Active && IsParticipant(first) && IsParticipant(second) && _members[first].Team != _members[second].Team;

                return true;
            }
        }

        public bool TryGetNameColour(PlayerObject player, out Color colour)
        {
            lock (_sync)
            {
                colour = Color.White;

                if (!IsParticipant(player))
                    return false;

                colour = _members[player].Team == ValorTeam.Red
                    ? Color.Red
                    : Color.Blue;

                return true;
            }
        }

        public bool TryGetPetRelationship(MonsterObject target, MapObject source, out bool canAttack)
        {
            lock (_sync)
            {
                canAttack = false;

                if (target == null || target.Master == null)
                    return false;

                var first = Owner(target.Master);
                var second = Owner(source);

                if (!TryGetRelationship(first, second, out bool hostile))
                    return false;

                canAttack = hostile && target.CurrentMap == first.CurrentMap && source.CurrentMap == second.CurrentMap && !target.InSafeZone && !source.InSafeZone;

                return true;
            }
        }

        private static PlayerObject Owner(MapObject source)
        {
            if (source is PlayerObject player)
                return player;

            if (source is HeroObject hero)
                return hero.Owner;

            if (source is MonsterObject pet && pet.Master != source)
                return Owner(pet.Master);

            return null;
        }

        private Objective Find(MonsterObject monster)
        {
            return _monuments.Concat(_bosses).Concat(_waveMonsters).FirstOrDefault(o => o.Monster == monster);
        }

        public bool IsObjective(MonsterObject monster)
        {
            lock (_sync)
                return monster != null && Find(monster) != null;
        }

        public bool CanAttackObjective(MonsterObject monster, MapObject source)
        {
            lock (_sync)
            {
                var objective = Find(monster);
                var player = Owner(source);

                if (_phase != Phase.Active || objective == null || player == null || !IsParticipant(player) || player.Dead)
                    return false;

                return !_monuments.Contains(objective) || objective.Owner != _members[player].Team;
            }
        }

        public void RecordDamage(MonsterObject monster, MapObject source)
        {
            lock (_sync)
            {
                var objective = Find(monster);

                if (objective != null && CanAttackObjective(monster, source))
                    objective.LastDamager = source;
            }
        }

        public bool OnMonsterDeath(MonsterObject monster)
        {
            lock (_sync)
            {
                var objective = Find(monster);

                if (objective == null)
                    return false;

                var killer = Owner(objective.LastDamager);

                bool valid = _phase == Phase.Active && killer != null && IsParticipant(killer) && !killer.Dead;

                if (_monuments.Contains(objective))
                {
                    if (valid)
                    {
                        objective.Owner = _members[killer].Team;

                        Announce($"{TeamName(objective.Owner)}占领了{objective.Name}！");
                    }

                    objective.RespawnAt = World.Time + 1000;
                }
                else if (objective.Regular)
                {
                    if (valid)
                    {
                        int points = World.Random.Next(1, 4);

                        AwardPersonal(killer, points);

                        killer.ReceiveChat($"勇气：个人积分 +{points}。", ChatType.System);
                    }
                }
                else if (objective.Boss && valid)
                {
                    ValorTeam team = _members[killer].Team;

                    foreach (var member in _members.Values)
                    {
                        var ally = member.Player;

                        if (member.Team != team || !IsParticipant(ally) || ally.Dead || Functions.MaxDistance(ally.CurrentLocation, objective.Location) > _settings.BufferRadius)
                            continue;

                        AwardPersonal(ally, 15);

                        ally.AddBuff(BuffType.Valor, ally, _settings.BufferDurationSeconds * 1000, new Stats
                        {
                            [Stat.MinDC] = _settings.BufferAttackBonus,
                            [Stat.MaxDC] = _settings.BufferAttackBonus,
                            [Stat.MinMC] = _settings.BufferAttackBonus,
                            [Stat.MaxMC] = _settings.BufferAttackBonus,
                            [Stat.MinSC] = _settings.BufferAttackBonus,
                            [Stat.MaxSC] = _settings.BufferAttackBonus,
                            [Stat.MinAC] = _settings.BufferDefenceBonus,
                            [Stat.MaxAC] = _settings.BufferDefenceBonus,
                            [Stat.MinMAC] = _settings.BufferDefenceBonus,
                            [Stat.MaxMAC] = _settings.BufferDefenceBonus
                        });

                        ally.ReceiveChat("勇气：个人积分 +15，并获得 5 分钟增益效果。", ChatType.System);
                    }
                }

                monster.HP = 0;
                monster.Dead = true;

                RemoveMonster(monster);

                objective.Monster = null;
                objective.LastDamager = null;

                if (objective.Regular)
                {
                    _waveMonsters.Remove(objective);

                    if (!_waveMonsters.Any(o => o.Side == objective.Side)) SpawnBoss(objective.Side);
                }

                if (_redScore >= 7500 || _blueScore >= 7500)
                {
                    Finish();
                    return true;
                }

                SendStatus(true);
                return true;
            }
        }

        private void AwardPersonal(PlayerObject player, int points)
        {
            _members[player].Personal += points;

            if (_members[player].Team == ValorTeam.Red) _redScore += points;
            else
                _blueScore += points;
        }

        private bool Configure(PlayerObject requester)
        {
            try
            {
                _settings = ValorSettings.Load();

                _map = World.GetMapByNameAndInstance(_settings.MapFileName);

                if (_map == null || !_map.ValidPoint(BlueSpawn) || !_map.ValidPoint(RedSpawn) || _monuments.Any(o => !_map.ValidPoint(o.Location) || World.GetMonsterInfo(o.Name) == null || World.GetMonsterInfo(o.Name).Stats[Stat.HP] <= 0) || _bosses.Any(o => !_map.ValidPoint(o.Location)))
                {
                    throw new InvalidDataException("请配置勇气战场地图、出生点，以及太阳碑、月亮碑、闪电碑怪物名称。");
                }

                var missing = WaveMonsterNames.Concat(new[] { "邪牛天王0" }).Where(name => World.GetMonsterInfo(name) == null || World.GetMonsterInfo(name).Stats[Stat.HP] <= 0).ToList();

                if (missing.Count != 0)
                {
                    throw new InvalidDataException("勇气战场需要以下怪物模板，并且这些模板必须配置生命值：" + string.Join("、", missing));
                }

                if (_map.Info.NoFight || _map.Info.RequiredGroup || _map.Info.NoTeleport)
                {
                    throw new InvalidDataException("勇气战场地图必须允许战斗和传送，并且不能要求队伍。");
                }

                if (_map.Info.SafeZones.Any(z => _monuments.Concat(_bosses).Any(o => Functions.InRange(z.Location, o.Location, z.Size))))
                {
                    throw new InvalidDataException("据点和 Boss 出现位置不能位于安全区内。");
                }
                return true;
            }
            catch (Exception ex)
            {
                requester.ReceiveChat("勇气战场无法开启：" + ex.Message, ChatType.System);
                MessageQueue.Enqueue("勇气战场配置错误：" + ex);
                return false;
            }
        }

        public void Open(PlayerObject requester, bool gmCommand = false)
        {
            lock (_sync)
            {
                if (requester == null || requester.Node == null || (gmCommand && !requester.IsGM))
                    return;

                if (!gmCommand && !AtRegistrationNpc(requester))
                    return;

                if (_phase != Phase.Idle)
                {
                    requester.ReceiveChat("勇气战场报名或战斗已经进行中。", ChatType.System);
                    return;
                }

                if (!Configure(requester))
                    return;

                _phase = Phase.Registration;
                _ends = World.Time + 60000;

                Announce("勇气战场已经开启，请前往战场等地区 NPC 处报名。");
            }
        }

        private static bool AtRegistrationNpc(PlayerObject player)
        {
            var npc = NPCObject.Get(player.NPCObjectID);//默认地图：0 坐标：327, 258

            bool valid = npc != null && npc.CurrentMap == player.CurrentMap && npc.CurrentMap != null &&
                string.Equals(npc.CurrentMap.Info.FileName, "battle_waiting", StringComparison.OrdinalIgnoreCase) &&
                npc.CurrentLocation == new Point(92, 92) &&
                Functions.MaxDistance(player.CurrentLocation, npc.CurrentLocation) <= Globals.DataRange;

            if (!valid)
            {
                player.ReceiveChat("请前往战场等地区的勇气战场 NPC。", ChatType.System);
            }

            return valid;
        }

        public void Register(PlayerObject player)
        {
            lock (_sync)
            {
                if (player == null || player.Node == null || !AtRegistrationNpc(player))
                    return;

                if (_phase != Phase.Registration || World.Time >= _ends)
                {
                    player.ReceiveChat("勇气战场报名已经结束，请通过 NPC 重新开启新的战场。", ChatType.System);
                    return;
                }

                if (player.Dead || Contains(player) || _applications.Contains(player))
                {
                    player.ReceiveChat("你不能重复报名，也不能在死亡状态下报名。", ChatType.System);
                    return;
                }

                if (_applications.Count >= _settings.MaximumPlayers)
                {
                    player.ReceiveChat("勇气战场人数已满。", ChatType.System);
                    return;
                }

                _applications.Add(player);

                player.SetTimer("ValorRegistration", (int)Math.Max(1, (_ends - World.Time + 999) / 1000), 1);

                player.ReceiveChat("你已成功报名勇气战场。", ChatType.System);
            }
        }

        public void ShowHonor(PlayerObject player)
        {
            lock (_sync)
            {
                if (player == null)
                    return;

                try
                {
                    player.ReceiveChat($"勇气荣誉：{_honor.Get(player.Info.Index):N0} / 200,000", ChatType.System);
                }
                catch (Exception ex)
                {
                    player.ReceiveChat("勇气荣誉暂时无法查询：" + ex.Message, ChatType.System);
                }
            }
        }

        public int GetHonor(PlayerObject player)
        {
            lock (_sync)
                return _honor.Get(player.Info.Index);
        }

        public void BuyShopReward(PlayerObject player, ItemInfo info, ushort count)
        {
            lock (_sync)
            {
                if (player == null || player.Dead || player.NPCPage == null || !string.Equals(player.NPCPage.Key, NPCScript.HonorBuyKey, StringComparison.OrdinalIgnoreCase) || info == null || count == 0 || count > info.StackSize)
                    return;

                try
                {
                    var reward = ValorSettings.Load().Rewards.FirstOrDefault(r => World.GetItemInfo(r.Item)?.Index == info.Index);

                    if (reward == null)
                        return;

                    long cost = (long)reward.HonorCost * count;
                    int balance = _honor.Get(player.Info.Index);

                    if (cost > balance)
                    {
                        player.ReceiveChat("你的荣誉不足。", ChatType.System);
                        return;
                    }

                    var item = World.CreateFreshItem(info);

                    if (item == null)
                        return;

                    item.Count = count;

                    if (!player.CanGainItem(item))
                    {
                        player.ReceiveChat("背包空间或负重不足。", ChatType.System);
                        return;
                    }

                    _honor.SetMany(new Dictionary<int, int>
                    {
                        [player.Info.Index] = balance - (int)cost
                    });

                    player.GainItem(item);

                    player.Enqueue(new S.NPCHonorGoods
                    {
                        BalanceOnly = true,
                        Balance = balance - (int)cost
                    });
                }
                catch (Exception ex)
                {
                    player.ReceiveChat("荣誉购买失败：" + ex.Message, ChatType.System);
                    MessageQueue.Enqueue("荣誉购买错误：" + ex);
                }
            }
        }

        public void Exchange(PlayerObject player, int rewardIndex)
        {
            lock (_sync)
            {
                if (player == null || player.Dead || !AtRegistrationNpc(player))
                    return;

                try
                {
                    var settings = ValorSettings.Load();

                    if (rewardIndex < 0 || rewardIndex >= settings.Rewards.Count)
                    {
                        player.ReceiveChat("该勇气奖励尚未配置。", ChatType.System);
                        return;
                    }

                    var reward = settings.Rewards[rewardIndex];
                    var info = World.GetItemInfo(reward.Item);

                    var item = info == null ? null : World.CreateFreshItem(info);

                    if (item == null || !player.CanGainItem(item))
                    {
                        player.ReceiveChat("奖励无法领取，或者背包空间/负重不足。", ChatType.System);
                        return;
                    }

                    int balance = _honor.Get(player.Info.Index);

                    if (balance < reward.HonorCost)
                    {
                        player.ReceiveChat($"该奖励需要 {reward.HonorCost:N0} 点荣誉。", ChatType.System);
                        return;
                    }

                    _honor.SetMany(new Dictionary<int, int>
                    {
                        [player.Info.Index] = balance - reward.HonorCost
                    });

                    player.GainItem(item);
                    ShowHonor(player);
                }
                catch (Exception ex)
                {
                    player.ReceiveChat("勇气奖励暂时无法领取：" + ex.Message, ChatType.System);
                    MessageQueue.Enqueue("勇气兑换错误：" + ex);
                }
            }
        }

        public bool RouteNormalChat(PlayerObject player, Packet packet)
        {
            lock (_sync)
            {
                if (!IsMap(player.CurrentMap))
                {
                    return false;
                }

                if (!IsParticipant(player))
                {
                    return true;
                }

                foreach (var member in _members.Values)
                {
                    if (IsParticipant(member.Player) && member.Team == _members[player].Team && Functions.MaxDistance(player.CurrentLocation, member.Player.CurrentLocation) <= Globals.DataRange)
                    {
                        member.Player.Enqueue(packet);
                    }
                }

                return true;
            }
        }

        public void OnPlayerDeath(PlayerObject player)
        {
            lock (_sync)
            {
                if (!IsParticipant(player))
                {
                    return;
                }

                var member = _members[player];

                member.Deaths++;
                member.ReviveAt = World.Time + 2000;

                var killer = Owner(player.LastHitter);

                if (killer != null && TryGetRelationship(player, killer, out bool hostile) && hostile)
                {
                    _members[killer].Kills++;
                }
            }
        }

        public void OnMapChanged(PlayerObject player)
        {
            lock (_sync)
            {
                if (!_returning && Contains(player) && player.CurrentMap != _map)
                {
                    Leave(player, false);
                }
            }
        }

        public void Leave(PlayerObject player, bool returnHome = true)
        {
            lock (_sync)
            {
                if (player == null)
                {
                    return;
                }

                if (_applications.Remove(player))
                {
                    player.ExpireTimer("ValorRegistration");
                }

                if (!_members.TryGetValue(player, out var member))
                {
                    return;
                }

                try
                {
                    _honor.SetMany(new Dictionary<int, int>
                    {
                        [player.Info.Index] = ValorRules.AddHonor(_honor.Get(player.Info.Index), member.Personal)
                    });
                }
                catch (Exception ex)
                {
                    MessageQueue.Enqueue("勇气战场离场荣誉保存失败：" + ex);
                    return;
                }

                _members.Remove(player);

                Restore(member, returnHome);

                player.Enqueue(new S.ValorStatus());

                player.ReceiveChat($"你已离开勇气战场。本次获得 {member.Personal} 点个人荣誉。", ChatType.System);
            }
        }

        private void Restore(Member member, bool returnHome)
        {
            var player = member.Player;

            player.AMode = member.PreviousMode;
            player.BrownTime = member.PreviousBrownTime;

            player.RemoveBuff(BuffType.Valor);
            player.ExpireTimer("ValorRegistration");

            player.Enqueue(new S.ChangeAMode
            {
                Mode = player.AMode
            });

            if (returnHome && player.Node != null)
            {
                if (player.Dead)
                {
                    player.Revive(player.Stats[Stat.HP], true);
                }

                var map = World.GetMap(member.ReturnMap);
                var location = member.ReturnLocation;

                if (map == null || !map.ValidPoint(location))
                {
                    map = World.GetMap(player.BindMapIndex);
                    location = player.BindLocation;
                }

                if (map != null && map.ValidPoint(location))
                {
                    _returning = true;

                    try
                    {
                        player.Teleport(map, location);
                    }
                    finally
                    {
                        _returning = false;
                    }
                }
            }

            player.RefreshNameColour();
        }

        public void Process()
        {
            lock (_sync)
            {
                if (_phase == Phase.Registration && World.Time >= _ends)
                {
                    Start();
                }

                if (_phase == Phase.Settling)
                {
                    if (World.Time >= _settleAt)
                    {
                        Settle();
                    }

                    return;
                }

                if (_phase != Phase.Active)
                {
                    return;
                }

                foreach (var member in _members.Values.ToList())
                {
                    var player = member.Player;

                    if (player.Node == null || player.CurrentMap != _map)
                    {
                        Leave(player, false);
                        continue;
                    }

                    if (player.Dead && member.ReviveAt > 0 && World.Time >= member.ReviveAt)
                    {
                        player.Revive(player.Stats[Stat.HP], true);
                        player.Teleport(_map, Spawn(member.Team));
                        member.ReviveAt = 0;
                    }
                }

                if (World.Time >= _ends || !_members.Values.Any(m => m.Team == ValorTeam.Red) || !_members.Values.Any(m => m.Team == ValorTeam.Blue))
                {
                    Finish();
                    return;
                }

                if (_waveNumber == 1 && World.Time >= _secondWaveAt)
                {
                    if (!StartWave(2))
                    {
                        Cancel("勇气战场已取消：第二波怪物无法生成。");
                        return;
                    }

                    Announce("勇气战场：第二波怪物已经出现！");
                    SendStatus(true);
                }

                foreach (var boss in _bosses)
                {
                    if (!boss.BossSpawned && !_waveMonsters.Any(o => o.Side == boss.Side))
                    {
                        SpawnBoss(boss.Side);
                    }
                }

                foreach (var objective in _monuments)
                {
                    if (objective.Monster == null && World.Time >= objective.RespawnAt && !SpawnObjective(objective))
                    {
                        objective.RespawnAt = World.Time + 1000;
                    }
                }

                if (World.Time >= _scoreAt)
                {
                    _scoreAt = World.Time + _settings.ScoreIntervalSeconds * 1000;

                    _redScore += Ownership(ValorTeam.Red);
                    _blueScore += Ownership(ValorTeam.Blue);

                    foreach (var member in _members.Values)
                    {
                        if (member.Player.Dead || !IsParticipant(member.Player))
                        {
                            continue;
                        }

                        if (_monuments.Any(o => o.Owner == member.Team && Functions.MaxDistance(o.Location, member.Player.CurrentLocation) <= _settings.MonumentRadius))
                        {
                            member.Personal += _settings.PersonalPointsPerTick;

                            if (member.Team == ValorTeam.Red)
                            {
                                _redScore += _settings.PersonalPointsPerTick;
                            }
                            else
                            {
                                _blueScore += _settings.PersonalPointsPerTick;
                            }
                        }
                    }

                    if (_redScore >= 7500 || _blueScore >= 7500)
                    {
                        Finish();
                        return;
                    }
                }

                if (World.Time >= _statusAt)
                {
                    _statusAt = World.Time + 1000;
                    SendStatus(true);
                }
            }
        }

        private int Ownership(ValorTeam team)
        {
            return ValorRules.OwnershipPoints(_monuments[0].Owner == team, _monuments[1].Owner == team, _monuments[2].Owner == team);
        }

        private bool SpawnObjective(Objective objective)
        {
            var monster = new ValorMonument(World.GetMonsterInfo(objective.Name), MonumentHealth(objective));

            if (monster == null || !monster.Spawn(_map, objective.Location))
                return false;

            objective.Monster = monster;
            objective.LastDamager = null;

            monster.Target = null;

            if (_monuments.Contains(objective))
            {
                monster.NameColour = objective.Owner == ValorTeam.Red ? Color.Red : objective.Owner == ValorTeam.Blue ? Color.Blue : Color.White;
                monster.Broadcast(new S.ObjectColourChanged {ObjectID =monster.ObjectID, NameColour = monster.NameColour});
            }
            return true;
        }

        private bool SpawnBoss(ValorTeam side)
        {
            var boss = _bosses.First(o => o.Side == side);

            if (boss.Monster != null) return true;

            var monster = new MonsterObject(World.GetMonsterInfo(boss.Name));

            if (!monster.Spawn(_map, boss.Location)) return false;

            boss.Monster = monster;
            boss.BossSpawned = true;
            boss.LastDamager = null;

            Announce($"勇气战场：{TeamName(side)}一侧的牛魔王已经出现！");
            return true;
        }

        private bool StartWave(int number)
        {
            foreach (var objective in _waveMonsters)
                RemoveMonster(objective.Monster);

            _waveMonsters.Clear();

            foreach (var boss in _bosses)
            {
                RemoveMonster(boss.Monster);

                boss.Monster = null;
                boss.LastDamager = null;
                boss.BossSpawned = false;
            }

            foreach (var boss in _bosses)
            {
                var used = new HashSet<Point>();
                foreach (var name in WaveMonsterNames)
                {
                    Point location = FindWaveLocation(boss.Location, used);

                    if (location == Point.Empty) return false;
                    var objective = new Objective {Name = name, Side = boss.Side, Regular = true, Location = location };
                    var monster =new MonsterObject(World.GetMonsterInfo(name));
                    if (!monster.Spawn(_map, location)) return false;

                    objective.Monster = monster;
                    _waveMonsters.Add(objective);
                    used.Add(location);
                }
            }
            _waveNumber = number;
            return true;
        }

        private Point FindWaveLocation(Point center, HashSet<Point> used)
        {
            for (int radius = 1; radius <= 12; radius++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    for (int x = -radius; x <= radius; x++)
                    {
                        if (Math.Max(Math.Abs(x), Math.Abs(y)) != radius)
                            continue;

                        var point = new Point(center.X + x, center.Y + y);

                        if (_map.ValidPoint(point) && !used.Contains(point) && !_map.Info.SafeZones.Any(z => Functions.InRange(z.Location, point, z.Size)))
                        {
                            return point;
                        }
                    }
                }
            }

            return Point.Empty;
        }

        private void Start()
        {
            _applications.RemoveAll(p => p.Node == null || p.Dead || IsMap(p.CurrentMap));

            if (_applications.Count < 2)
            {
                Cancel("勇气战场已取消：符合条件的报名玩家少于两人。");
                return;
            }

            foreach (var o in _monuments)
            {
                if (!SpawnObjective(o))
                {
                    Cancel("勇气战场已取消：无法生成战场据点。");
                    return;
                }
            }

            _phase = Phase.Active;
            _ends = World.Time + 1200000;
            _secondWaveAt = World.Time + 600000;
            _scoreAt = World.Time + _settings.ScoreIntervalSeconds * 1000;

            int redLevels = 0;
            int blueLevels = 0;
            int redCount = 0;
            int blueCount = 0;

            foreach (var player in _applications.OrderByDescending(p => p.Level).ThenBy(p => p.Name))
            {
                ValorTeam team;

                if (redCount < blueCount)
                {
                    team = ValorTeam.Red;
                }
                else if (redCount == blueCount && redLevels <= blueLevels)
                {
                    team = ValorTeam.Red;
                }
                else
                {
                    team = ValorTeam.Blue;
                }

                var member = new Member
                {
                    Player = player,
                    Team = team,
                    PreviousMode = player.AMode,
                    PreviousBrownTime = player.BrownTime,
                    ReturnMap = player.BindMapIndex,
                    ReturnLocation = player.BindLocation
                };

                _members.Add(player, member);

                if (!player.Teleport(_map, Spawn(team)))
                {
                    _members.Remove(player);
                    Restore(member, false);
                    continue;
                }

                if (team == ValorTeam.Red)
                {
                    redCount++;
                    redLevels += player.Level;
                }
                else
                {
                    blueCount++;
                    blueLevels += player.Level;
                }

                player.AMode = AttackMode.Valor;

                player.Enqueue(new S.ChangeAMode
                {
                    Mode = player.AMode
                });

                player.RefreshNameColour();
                player.ExpireTimer("ValorRegistration");

                player.ReceiveChat($"勇气战场：你被分配到{TeamName(team)}。攻击据点即可占领它们！", ChatType.Announcement);
            }

            foreach (var applicant in _applications)
                applicant.ExpireTimer("ValorRegistration");

            _applications.Clear();

            if (redCount == 0 || blueCount == 0)
            {
                Cancel("勇气战场已取消：无法组成双方队伍。");
                return;
            }

            if (!StartWave(1))
            {
                Cancel("勇气战场已取消：第一波怪物无法生成。");
                return;
            }

            Announce("勇气战场已经开始！首先达到 7,500 分的队伍获胜，战斗时间上限为 20 分钟。");

            SendStatus(true);
        }

        private void Finish()
        {
            _winner = ValorRules.Winner(_redScore, _blueScore);
            _settleAt = World.Time + 5000;
            _phase = Phase.Settling;
        }

        private void Settle()
        {
            try
            {
                var changes = new Dictionary<int, int>();

                foreach (var member in _members.Values)
                {
                    bool completed = IsParticipant(member.Player);

                    int award = ValorRules.HonorAward(member.Personal, completed, completed && member.Team == _winner, _settings.CompletionHonor, _settings.VictoryHonor);

                    changes[member.Player.Info.Index] = ValorRules.AddHonor(_honor.Get(member.Player.Info.Index), award);
                }

                _honor.SetMany(changes);
            }
            catch (Exception ex)
            {
                _settleAt = World.Time + 5000;

                MessageQueue.Enqueue("勇气战场结算失败，将在 5 秒后重试：" + ex);
                return;
            }

            SendStatus(false);

            if (_winner == ValorTeam.None)
            {
                Announce("勇气战场以平局结束。");
            }
            else
            {
                Announce($"勇气战场结束！{TeamName(_winner)}获胜！红方 {_redScore:N0} 分 / 蓝方 {_blueScore:N0} 分。");
            }

            var members = _members.Values.ToList();

            _members.Clear();

            foreach (var member in members)
            {
                if (member.Player.Node != null)
                {
                    Restore(member, true);
                    ShowHonor(member.Player);
                }
            }

            Reset();
        }

        private void SendStatus(bool active)
        {
            var rows = _members.Values
                .OrderByDescending(m => m.Personal)
                .ThenByDescending(m => m.Kills)
                .ThenBy(m => m.Player.Name)
                .Select(m => new S.ValorRow
                {
                    Name = m.Player.Name,
                    Level = m.Player.Level,
                    Team = (byte)m.Team,
                    Personal = m.Personal,
                    Kills = m.Kills,
                    Deaths = m.Deaths,
                    Bonus = !active && IsParticipant(m.Player) ? _settings.CompletionHonor + (m.Team == _winner ? _settings.VictoryHonor : 0) : 0,
                    Honor = _honor.Get(m.Player.Info.Index)
                })
                .ToList();

            var packet = new S.ValorStatus
            {
                Active = active,
                RedScore = _redScore,
                BlueScore = _blueScore,
                Seconds = active ? (int)Math.Max(0, (_ends - World.Time + 999) / 1000) : 0,
                Winner = (byte)_winner,
                Rows = rows
            };

            SetMonumentProgress(_monuments[0], out packet.SunDamage, out packet.SunHealth, out packet.SunAttacker);
            SetMonumentProgress(_monuments[1], out packet.MoonDamage, out packet.MoonHealth, out packet.MoonAttacker);
            SetMonumentProgress(_monuments[2], out packet.LightningDamage, out packet.LightningHealth, out packet.LightningAttacker);

            packet.RedMonsters = _waveMonsters.Count(o => o.Side == ValorTeam.Red);
            packet.BlueMonsters = _waveMonsters.Count(o => o.Side == ValorTeam.Blue);

            bool redBoss = _bosses.Any(o => o.Side == ValorTeam.Red && o.Monster != null);
            bool blueBoss = _bosses.Any(o => o.Side == ValorTeam.Blue && o.Monster != null);

            if (blueBoss)
            {
                packet.BlueMonsterState = 991;
            }
            else if (packet.BlueMonsters > 0)
            {
                packet.BlueMonsterState = 990;
            }
            else
            {
                packet.BlueMonsterState = 992;
            }

            if (redBoss)
            {
                packet.RedMonsterState = 994;
            }
            else if (packet.RedMonsters > 0)
            {
                packet.RedMonsterState = 993;
            }
            else
            {
                packet.RedMonsterState = 995;
            }

            foreach (var member in _members.Values)
            {
                if (member.Player.Node != null)
                {
                    member.Player.Enqueue(packet);
                }
            }
        }

        private void SetMonumentProgress(Objective objective, out int damage, out int health, out byte attacker)
        {
            int maximum = MonumentHealth(objective);

            if (maximum == 0)
            {
                maximum = Math.Max(0, World.GetMonsterInfo(objective.Name).Stats[Stat.HP]);
            }

            if (objective.Monster == null)
            {
                health = maximum;
            }
            else
            {
                health = Math.Max(0, objective.Monster.HP);
            }

            damage = Math.Max(0, maximum - health);

            var player = Owner(objective.LastDamager);

            if (player != null && _members.TryGetValue(player, out var member))
            {
                attacker = (byte)member.Team;
            }
            else
            {
                attacker = 0;
            }
        }

        private static int MonumentHealth(Objective objective)
        {
            if (objective.Name == "太阳碑")
            {
                return 300;
            }

            if (objective.Name == "月亮碑" || objective.Name == "闪电碑")
            {
                return 100;
            }
            return 0;
        }

        private void Cancel(string reason)
        {
            Announce(reason);

            foreach (var player in _applications) player.ExpireTimer("ValorRegistration");

            var members = _members.Values.ToList();

            _members.Clear();

            foreach (var member in members) Restore(member, true);

            Reset();
        }

        private static void RemoveMonster(MonsterObject monster)
        {
            if (monster == null || monster.Node == null || monster.CurrentMap == null) return;

            Map map = monster.CurrentMap;

            map.RemoveObject(monster);

            if (map.MonsterCount > 0) map.MonsterCount--;

            if (World.MonsterCount > 0) World.MonsterCount--;

            monster.Despawn();
        }

        private void Reset()
        {
            foreach (var o in _monuments.Concat(_bosses))
            {
                RemoveMonster(o.Monster);

                o.Monster = null;
                o.Owner = ValorTeam.None;
                o.LastDamager = null;
                o.RespawnAt = 0;
                o.BossSpawned = false;
            }

            foreach (var o in _waveMonsters) RemoveMonster(o.Monster);

            _waveMonsters.Clear();
            _waveNumber = 0;
            _secondWaveAt = 0;
            _applications.Clear();
            _members.Clear();
            _redScore = 0;
            _blueScore = 0;
            _winner = ValorTeam.None;
            _phase = Phase.Idle;
            _map = null;
        }

        private static void Announce(string text)
        {
            World.Broadcast(new S.Chat { Message = text, Type = ChatType.Announcement });
        }
    }
}