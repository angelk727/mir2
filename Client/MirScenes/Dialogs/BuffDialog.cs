using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;

namespace Client.MirScenes.Dialogs
{
    public class BuffDialog : MirImageControl
    {
        public List<ClientBuff> Buffs = new List<ClientBuff>();

        protected MirButton _expandCollapseButton;
        protected MirLabel _buffCountLabel;
        protected List<MirImageControl> _buffList = new List<MirImageControl>();
        protected bool _fadedOut, _fadedIn;
        protected int _buffCount;
        protected long _nextFadeTime;
        public Func<bool> GetExpandedParameter;
        public Action<bool> SetExpandedParameter;
        private bool ExpandedBuffWindow
        {
            get { return GetExpandedParameter(); }
            set { SetExpandedParameter(value); }
        }

        protected const long FadeDelay = 55;
        protected const float FadeRate = 0.2f;

        public BuffDialog()
        {
            Index = 20;
            Library = Libraries.Prguse2;
            Movable = false;
            Size = new Size(44, 34);
            Location = new Point(Settings.ScreenWidth - 170, 0);
            Sort = true;

            Opacity = 0f;
            _fadedOut = true;

            _expandCollapseButton = new MirButton
            {
                Index = 7,
                HoverIndex = 8,
                Size = new Size(16, 15),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 9,
                Sound = SoundList.ButtonA,
                Opacity = 0f
            };
            _expandCollapseButton.Click += (o, e) =>
            {
                if (_buffCount == 1)
                {
                    ExpandedBuffWindow = true;
                }
                else
                {
                    ExpandedBuffWindow = !ExpandedBuffWindow;
                }

                UpdateWindow();
            };

            _buffCountLabel = new MirLabel
            {
                Parent = this,
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                NotControl = true,
                Sort = true,
                Visible = false,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
            };
        }

        public void CreateBuff(ClientBuff buff)
        {
            var buffImage = BuffImage(buff.Type);

            var buffLibrary = Libraries.BuffIcon;

            if (buffImage >= 20000)
            {
                buffImage -= 20000;
                buffLibrary = Libraries.MagIcon;
            }

            if (buffImage >= 10000)
            {
                buffImage -= 10000;
                buffLibrary = Libraries.Prguse2;
            }

            var image = new MirImageControl
            {
                Library = buffLibrary,
                Parent = this,
                Visible = true,
                Sort = false,
                Index = buffImage
            };

            _buffList.Insert(0, image);
            UpdateWindow();
        }

        public void RemoveBuff(int buffId)
        {
            _buffList[buffId].Dispose();
            _buffList.RemoveAt(buffId);

            UpdateWindow();
        }

        public void Process()
        {
            if (!Visible) return;

            if (_buffList.Count != _buffCount)
            {
                UpdateWindow();
            }

            for (var i = 0; i < _buffList.Count; i++)
            {
                var image = _buffList[i];
                var buff = Buffs[i];

                var buffImage = BuffImage(buff.Type);
                var buffLibrary = Libraries.BuffIcon;

                //ArcherSpells - VampireShot,PoisonShot
                if (buffImage >= 30000)
                {
                    buffImage -= 30000;
                    buffLibrary = Libraries.MagIcon;
                }

                if (buffImage >= 20000)
                {
                    buffImage -= 20000;
                    buffLibrary = Libraries.Prguse2;
                }

                if (buffImage >= 10000)
                {
                    buffImage -= 10000;
                    buffLibrary = Libraries.Prguse;
                }

                var location = new Point(Size.Width - 10 - 23 - (i * 23) + ((10 * 23) * (i / 10)), 6 + ((i / 10) * 24));

                image.Location = new Point(location.X, location.Y);
                image.Hint = ExpandedBuffWindow ? BuffString(buff) : CombinedBuffText();
                image.Index = buffImage;
                image.Library = buffLibrary;

                if (ExpandedBuffWindow || !ExpandedBuffWindow && i == 0)
                {
                    image.Visible = true;
                    image.Opacity = 1f;
                }
                else
                {
                    image.Visible = false;
                    image.Opacity = 0.6f;
                }

                if (buff.Paused || buff.Infinite || !(Math.Round((buff.ExpireTime - CMain.Time) / 1000D) <= 5))
                    continue;

                var time = (buff.ExpireTime - CMain.Time) / 100D;

                if (Math.Round(time) % 10 < 5)
                    image.Index = -1;
            }

            if (IsMouseOver(CMain.MPoint))
            {
                if (_buffCount == 0 || (!_fadedIn && CMain.Time <= _nextFadeTime))
                    return;

                Opacity += FadeRate;
                _expandCollapseButton.Opacity += FadeRate;

                if (Opacity > 1f)
                {
                    Opacity = 1f;
                    _expandCollapseButton.Opacity = 1f;
                    _fadedIn = true;
                    _fadedOut = false;
                }

                _nextFadeTime = CMain.Time + FadeDelay;
            }
            else
            {
                if (!_fadedOut && CMain.Time <= _nextFadeTime)
                    return;

                Opacity -= FadeRate;
                _expandCollapseButton.Opacity -= FadeRate;

                if (Opacity < 0f)
                {
                    Opacity = 0f;
                    _expandCollapseButton.Opacity = 0f;
                    _fadedOut = true;
                    _fadedIn = false;
                }

                _nextFadeTime = CMain.Time + FadeDelay;
            }
        }

        private void UpdateWindow()
        {
            _buffCount = _buffList.Count;

            var baseImage = 20;
            var heightOffset = Location.Y;

            //foreach (var dialog in GameScene.Scene.BuffDialogs)
            //{
            //    if (dialog.Category == Category) break;

            //    if (dialog.Buffs.Count > 0)
            //    {
            //        heightOffset += dialog.Size.Height;
            //    }
            //}

            if (_buffCount > 0 && ExpandedBuffWindow)
            {
                var oldWidth = Size.Width;

                if (_buffCount <= 10)
                    Index = baseImage + _buffCount - 1;
                else if (_buffCount > 10)
                    Index = baseImage + 10;
                else if (_buffCount > 20)
                    Index = baseImage + 11;
                else if (_buffCount > 30)
                    Index = baseImage + 12;
                else if (_buffCount > 40)
                    Index = baseImage + 13;

                var newX = Location.X - Size.Width + oldWidth;
                var newY = heightOffset;
                Location = new Point(newX, newY);

                _buffCountLabel.Visible = false;

                _expandCollapseButton.Location = new Point(Size.Width - 15, 0);
                Size = new Size((_buffCount > 10 ? 10 : _buffCount) * 23, 24 + (_buffCount / 10) * 24);
            }
            else if (_buffCount > 0 && !ExpandedBuffWindow)
            {
                var oldWidth = Size.Width;

                Index = 20;

                var newX = Location.X - Size.Width + oldWidth;
                var newY = heightOffset;
                Location = new Point(newX, newY);

                _buffCountLabel.Visible = true;
                _buffCountLabel.Text = $"{_buffCount}";
                _buffCountLabel.Location = new Point(Size.Width / 2 - _buffCountLabel.Size.Width / 2, Size.Height / 2 - 10);
                _buffCountLabel.BringToFront();

                _expandCollapseButton.Location = new Point(Size.Width - 15, 0);
                Size = new Size(44, 34);
            }
        }

        public string BuffString(ClientBuff buff)
        {
            string text = RegexFunctions.SeperateCamelCase(buff.Type.ToString()) + "\n";
            bool overridestats = false;

            switch (buff.Type)
            {
                case BuffType.游戏管理:
                    GMOptions options = (GMOptions)buff.Values[0];

                    if (options.HasFlag(GMOptions.GameMaster)) text += "-隐身模式\n";
                    if (options.HasFlag(GMOptions.Superman)) text += "-无敌模式\n";
                    if (options.HasFlag(GMOptions.Observer)) text += "-观察模式\n";
                    break;
                case BuffType.精神状态:
                    switch (buff.Values[0])
                    {
                        case 0:
                            text += "集中 (完全伤害)\n射击不能穿过屏障\n";
                            break;
                        case 1:
                            text += "穿透 (最小伤害)\n射击可以穿过屏障\n";
                            break;
                        case 2:
                            text += "组队 (中等伤害)\n不能偷袭\n";
                            break;
                    }
                    break;
                case BuffType.隐身术:
                    text += "施法后对怪物有隐身效果\n走动或跑动后解除特效\n对反隐身怪物视等级无效\n";
                    break;
                case BuffType.隐身戒指:
                    text += "戒指加持隐身术效果\n";
                    break;
                case BuffType.月影术:
                    text += "通过隐身来隐藏自己\n在此状态下攻击造成更高伤害\n";
                    break;
                case BuffType.轻身步:
                    text += "增加移动速度\n";
                    break;
                case BuffType.先天气功:
                    overridestats = true;
                    text += string.Format("被攻击后有 {0}%几率\n恢复 {1}点生命值\n", buff.Stats[Stat.气功盾恢复数率], buff.Stats[Stat.气功盾恢复生命值]);
                    break;
                case BuffType.烈火身:
                    text += "召唤替身进行一次爆炸攻击\n对非反隐怪物隐身并可移动\n";
                    break;
                case BuffType.吸血地闪:
                    text += "释放某些技能\n可以附加吸血效果\n";
                    break;
                case BuffType.毒魔闪:
                    text += "释放某些技能\n可以附加毒素效果\n";
                    break;
                case BuffType.气流术:
                    text += "一定时间内获得气\n";
                    break;
                case BuffType.深延术:
                    overridestats = true;
                    text += string.Format("增加魔法攻击: {0}-{1}\n法力值消耗增加 {2}%\n", buff.Stats[Stat.MinMC], buff.Stats[Stat.MaxMC], buff.Stats[Stat.法力值消耗数率]);
                    break;
                case BuffType.天上秘术:
                    text += "开启后(天霜冰环)和(流星火雨)\n无需引导\n";
                    break;
                case BuffType.万效符:
                    overridestats = true;
                    text += string.Format("受到的伤害减少{0}%", buff.Stats[Stat.伤害减免数率] );
                    break;
                case BuffType.万效符秘籍:
                    overridestats = true;
                    text += string.Format("受到的伤害减少{0}%", buff.Stats[Stat.伤害减免数率] );
                    break;
                case BuffType.变形效果:
                    text += "改变外形\n功能：免助跑\n";
                    break;
                case BuffType.衣钵相传:
                    text += "拜师后经验加成\n";
                    break;
                case BuffType.火传穷薪:
                    text += "收徒后伤害加成\n";
                    break;
                case BuffType.公会特效:
                    text += GameScene.Scene.GuildDialog.ActiveStats;
                    break;
                case BuffType.新人特效:
                    text += "新人公会特效加成\n";
                    break;
                case BuffType.失明状态:
                    overridestats = true;
                    text += string.Format("进入黑暗状态\n准确度有所降低\n");
                    break;
                case BuffType.安息之气:
                    text += "安息之气的加成\n";
                    break;
                case BuffType.远古气息:
                    text += "远古气息的加成\n";
                    break;
                case BuffType.华丽雨光:
                    text += "来自雨光的加成\n";
                    break;
                case BuffType.龙之特效:
                    text += "特效增益加成\n";
                    break;
                case BuffType.龙的特效:
                    text += "特效增益加成\n";
                    break;
                case BuffType.组队加成:
                    text += "来自组队的加成\n";
                    break;
                case BuffType.强化队伍:
                    text += "来自队伍的加成\n";
                    break;
                case BuffType.英雄灵气:
                    text += "英雄在线加成\n";
                    break;
                case BuffType.攻击型绝技://新添加 功能--绝技盒BUFF相关
                case BuffType.防御型绝技:
                case BuffType.技能型绝技:
                case BuffType.共用型绝技:
                    text += "绝技盒增益加成\n";
                    break;
                case BuffType.绝对封锁:
                    overridestats = true;
                    text += string.Format("封锁期间禁用药水\n生命值降低 25%\n法力值降低 25%\n");
                    break;
                case BuffType.古代宗师祝福:
                    text += "来自宗师的祝福\n";
                    break;
                case BuffType.黄金宗师祝福:
                    text += "宗师的黄金祝福\n";
                    break;
                case BuffType.暗影侵袭:
                    text += "来自陵寝秘空的侵袭\n";
                    break;
                case BuffType.烈火焚烧:
                    overridestats = true;
                    text += string.Format("每秒持续 {0}生命值\n", buff.Stats[Stat.HP]);
                    break;
            }

            if (!overridestats)
            {
                foreach (var val in buff.Stats.Values)
                {
                    var c = val.Value < 0 ? "降低" : "提高";
                    var key = val.Key.ToString();

                    var strKey = RegexFunctions.SeperateCamelCase(key.Replace("速率", "").Replace("倍率", "").Replace("数率", ""));

                    var sign = "";

                    if (key.Contains("数率"))
                        sign = "%";
                    else if (key.Contains("倍率"))
                        sign = "倍";

                    var txt = $"{c} {strKey} : {val.Value}{sign}\n";

                    text += txt;
                }
            }

            if (buff.Paused)
            {
                text += GameLanguage.ExpirePaused;
            }
            else if (buff.Infinite)
            {
                text += GameLanguage.ExpireNever;
            }
            else
            {
                text += string.Format(GameLanguage.Expire, Functions.PrintTimeSpanFromSeconds(Math.Round((buff.ExpireTime - CMain.Time) / 1000D)));
            }

            if (!string.IsNullOrEmpty(buff.Caster)) text += string.Format("\n特效来源: {0}", buff.Caster);

            return text;
        }

        private string CombinedBuffText()
        {
            string text = "激活特效\n";
            var stats = new Stats();

            for (var i = 0; i < _buffList.Count; i++)
            {
                var buff = Buffs[i];

                stats.Add(buff.Stats);
            }

            foreach (var val in stats.Values)
            {
                var c = val.Value < 0 ? "降低" : "提高";
                var key = val.Key.ToString();

                var strKey = RegexFunctions.SeperateCamelCase(key.Replace("速率", "").Replace("倍率", "").Replace("数率", ""));

                var sign = "";

                if (key.Contains("数率"))
                    sign = "%";
                else if (key.Contains("倍率"))
                    sign = "倍";

                var txt = $"{c} {strKey} : {val.Value}{sign}\n";

                text += txt;
            }

            return text;
        }

        private int BuffImage(BuffType type)
        {
            switch (type)
            {
                //Skills
                case BuffType.血龙剑法:
                    return 30152;
                case BuffType.天上秘术:
                    return 30154;
                case BuffType.剑气爆:
                    return 30098;
                case BuffType.金刚不坏:
                    return 30160;
                case BuffType.金刚不坏秘籍:
                    return 30168;
                case BuffType.天务:
                    return 30144;
                case BuffType.深延术:
                    return 30146;
                case BuffType.魔法盾:
                    return 30060;
                case BuffType.金刚术:
                    return 30196;
                case BuffType.隐身术:
                    return 30034;
                case BuffType.体迅风:
                    return 30120;
                case BuffType.幽灵盾:
                    return 30026;
                case BuffType.神圣战甲术:
                    return 30028;
                case BuffType.护身气幕:
                    return 30100;
                case BuffType.无极真气:
                    return 30070;
                case BuffType.诅咒术:
                    return 30090;
                case BuffType.先天气功:
                    return 30114;
                case BuffType.万效符:
                case BuffType.万效符秘籍:
                    return 184;

                case BuffType.轻身步:
                    return 30134;
                case BuffType.风身术:
                    return 30136;
                case BuffType.月影术:
                    return 30130;
                case BuffType.烈火身:
                    return 30140;

                case BuffType.气流术:
                    return 30192;
                case BuffType.吸血地闪:
                    return 30200;
                case BuffType.毒魔闪:
                    return 30204;
                case BuffType.精神状态:
                    return 59;

                //Monster
                case BuffType.惩戒真言:
                    return 115;
                case BuffType.至尊威严:
                    return 122;
                case BuffType.组队加成:
                    return 106;
                case BuffType.御体之力:
                    return 107;
                case BuffType.失明状态:
                    return 88;
                case BuffType.死亡印记:
                    return 111;
                case BuffType.寒冰护甲:
                    return 4;
                case BuffType.伤口加深:
                    return 94;
                case BuffType.麻痹状态:
                    return 96;
                case BuffType.绝对封锁:
                    return 61;
                case BuffType.Mon564NSealing:
                    return 61;
                case BuffType.防御诅咒:
                    return 67;
                case BuffType.万效符爆杀:
                    return 193;
                case BuffType.烈火焚烧:
                    return 548;

                //Special
                case BuffType.游戏管理:
                    return 20121;
                case BuffType.General:
                    return 20122;
                case BuffType.隐身戒指:
                    return 24;
                case BuffType.金币辉煌:
                    return 10907;
                case BuffType.包容万斤:
                    return 10872;
                case BuffType.变形效果:
                    return 10890;
                case BuffType.火传穷薪:
                case BuffType.衣钵相传:
                    return 80;
                case BuffType.心心相映:
                    return 179;
                case BuffType.公会特效:
                    return 63;
                case BuffType.精力充沛:
                    return 508;
                case BuffType.时间之殇:
                    return 75;
                case BuffType.技巧项链:
                    return 60;
                case BuffType.新人特效:
                    return 10903;
                case BuffType.安息之气:
                    return 334;
                case BuffType.远古气息:
                    return 334;
                case BuffType.华丽雨光:
                    return 30;
                case BuffType.龙之特效:
                    return 31;
                case BuffType.龙的特效:
                    return 32;
                case BuffType.奇异药水:
                    return 510;
                case BuffType.天灵水:
                    return 326;
                case BuffType.玉清水:
                    return 327;
                case BuffType.甜筒HP:
                    return 33;
                case BuffType.甜筒MP:
                    return 34;
                case BuffType.内尔族的灵药:
                    return 352;
                case BuffType.英雄灵气:
                    return 44;
                case BuffType.攻击型绝技:
                    return 136;
                case BuffType.防御型绝技:
                    return 137;
                case BuffType.技能型绝技:
                    return 138;
                case BuffType.共用型绝技:
                    return 139;
                case BuffType.强化队伍:
                    return 540;
                case BuffType.暗影侵袭:
                    return 541;
                case BuffType.古代宗师祝福:
                    return 542;
                case BuffType.黄金宗师祝福:
                    return 543;
                case BuffType.摩鲁的赤色药剂:
                    return 544;
                case BuffType.摩鲁的青色药剂:
                    return 545;
                case BuffType.摩鲁的黄色药剂:
                    return 546;

                //Stats
                case BuffType.攻击力提升:
                case BuffType.魔法力提升:
                case BuffType.道术力提升:
                case BuffType.攻击速度提升:
                case BuffType.生命值提升:
                case BuffType.法力值提升:
                case BuffType.防御提升:
                case BuffType.魔法防御提升:
                case BuffType.背包负重提升:
                case BuffType.准确命中提升:
                case BuffType.敏捷躲避提升:
                case BuffType.获取经验提升:
                case BuffType.物品掉落提升:
                case BuffType.技能经验提升:
                    return 10893;
                default:
                    return 0;
            }
        }
    }


    //UNFINISHED
    public class ClientPoisonBuff
    {
        public PoisonType Type;
        public string Caster;
        public int Value;
        public int TickSpeed;
        public long ExpireTime;
    }

    public sealed class PoisonBuffDialog : MirImageControl
    {
        public List<ClientPoisonBuff> Buffs = new List<ClientPoisonBuff>();

        private MirButton _expandCollapseButton;
        private MirLabel _buffCountLabel;
        private List<MirImageControl> _buffList = new List<MirImageControl>();
        private bool _fadedOut, _fadedIn;
        private int _buffCount;
        private long _nextFadeTime;

        private const long FadeDelay = 55;
        private const float FadeRate = 0.2f;

        public PoisonBuffDialog()
        {
            Index = 40;
            Library = Libraries.Prguse2;
            Movable = false;
            Size = new Size(44, 34);
            Location = new Point(Settings.ScreenWidth - 170, 0);
            Sort = true;

            Opacity = 0f;
            _fadedOut = true;

            _expandCollapseButton = new MirButton
            {
                Index = 7,
                HoverIndex = 8,
                Size = new Size(16, 15),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 9,
                Sound = SoundList.ButtonA,
                Opacity = 0f
            };
            _expandCollapseButton.Click += (o, e) =>
            {
                if (_buffCount == 1)
                {
                    Settings.ExpandedBuffWindow = true;
                }
                else
                {
                    Settings.ExpandedBuffWindow = !Settings.ExpandedBuffWindow;
                }

                UpdateWindow();
            };

            _buffCountLabel = new MirLabel
            {
                Parent = this,
                AutoSize = true,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                NotControl = true,
                Sort = true,
                Visible = false,
                ForeColour = Color.Yellow,
                OutLineColour = Color.Black,
            };
        }

        public void CreateBuff(ClientPoisonBuff buff)
        {
            var buffImage = BuffImage(buff.Type);

            var buffLibrary = Libraries.BuffIcon;

            if (buffImage >= 20000)
            {
                buffImage -= 20000;
                buffLibrary = Libraries.MagIcon;
            }

            if (buffImage >= 10000)
            {
                buffImage -= 10000;
                buffLibrary = Libraries.Prguse2;
            }

            var image = new MirImageControl
            {
                Library = buffLibrary,
                Parent = this,
                Visible = true,
                Sort = false,
                Index = buffImage
            };

            _buffList.Insert(0, image);
            UpdateWindow();
        }

        public string BuffString(ClientPoisonBuff buff)
        {
            string text = RegexFunctions.SeperateCamelCase(buff.Type.ToString()) + "\n";

            switch (buff.Type)
            {
                case PoisonType.Green:
                    {
                        var tick = buff.TickSpeed / 1000;
                        var tickName = tick > 1 ? "seconds" : "second";

                        text += $"Recieve {buff.Value} damage every {tick} {tickName}.\n";
                    }
                    break;
                case PoisonType.Red:
                    {
                        var tick = buff.TickSpeed / 1000;
                        var tickName = tick > 1 ? "seconds" : "second";

                        text += $"减少护甲防御 10% {tick} {tickName}\n";
                    }
                    break;
                case PoisonType.Slow:
                    text += "移动速度降低\n";
                    break;
                case PoisonType.Frozen:
                    text += "不能施法及移动\n和攻击\n";
                    break;
                case PoisonType.Stun:
                    {
                        var tick = buff.TickSpeed / 1000;
                        var tickName = tick > 1 ? "seconds" : "second";

                        text += $"提高伤害 20% {tick} {tickName}.\n";
                    }
                    break;
                case PoisonType.Paralysis:
                    text += "麻痹状态：不能攻击和移动\n";
                    break;
                case PoisonType.DelayedExplosion:
                    text += "定时爆破\n";
                    break;
                case PoisonType.Bleeding:
                    {
                        var tick = buff.TickSpeed / 1000;
                        var tickName = tick > 1 ? "seconds" : "second";

                        text += $"赋予 {buff.Value} 每次伤害 {tick} {tickName}.\n";
                    }
                    break;
                case PoisonType.LRParalysis:
                    text += "不能攻击和移动\n受到攻击后失效\n";
                    break;
                case PoisonType.Blindness:
                    text += "失明状态\n";
                    break;
                case PoisonType.Dazed:
                    text += "降低攻击\n";
                    break;
            }

            text += string.Format(GameLanguage.Expire, Functions.PrintTimeSpanFromSeconds(Math.Round((buff.ExpireTime - CMain.Time) / 1000D)));

            if (!string.IsNullOrEmpty(buff.Caster)) text += string.Format("\nCaster: {0}", buff.Caster);

            return text;
        }

        private int BuffImage(PoisonType type)
        {
            switch (type)
            {
                case PoisonType.Green:
                    return 221;
                case PoisonType.Red:
                    return 222;
                case PoisonType.Slow:
                    return 225;
                case PoisonType.Frozen:
                    return 223;
                case PoisonType.Stun:
                    return 224;
                case PoisonType.Paralysis:
                    return 233;
                case PoisonType.DelayedExplosion:
                    return 229;
                case PoisonType.Bleeding:
                    return 231;
                case PoisonType.LRParalysis:
                    return 233;
                case PoisonType.Blindness:
                    return 226;
                case PoisonType.Dazed:
                    return 230;
                default:
                    return 0;
            }
        }

        public void Process()
        {
            if (!Visible) return;

            if (_buffList.Count != _buffCount)
            {
                UpdateWindow();
            }

            for (var i = 0; i < _buffList.Count; i++)
            {
                var image = _buffList[i];
                var buff = Buffs[i];

                var buffImage = BuffImage(buff.Type);
                var buffLibrary = Libraries.BuffIcon;

                //ArcherSpells - VampireShot,PoisonShot
                if (buffImage >= 20000)
                {
                    buffImage -= 20000;
                    buffLibrary = Libraries.MagIcon;
                }

                if (buffImage >= 10000)
                {
                    buffImage -= 10000;
                    buffLibrary = Libraries.Prguse2;
                }

                var location = new Point(Size.Width - 10 - 23 - (i * 23) + ((10 * 23) * (i / 10)), 6 + ((i / 10) * 24));

                image.Location = new Point(location.X, location.Y);
                image.Hint = Settings.ExpandedBuffWindow ? BuffString(buff) : CombinedBuffText();
                image.Index = buffImage;
                image.Library = buffLibrary;

                if (Settings.ExpandedBuffWindow || !Settings.ExpandedBuffWindow && i == 0)
                {
                    image.Visible = true;
                    image.Opacity = 1f;
                }
                else
                {
                    image.Visible = false;
                    image.Opacity = 0.6f;
                }

                if (!(Math.Round((buff.ExpireTime - CMain.Time) / 1000D) <= 5))
                    continue;

                var time = (buff.ExpireTime - CMain.Time) / 100D;

                if (Math.Round(time) % 10 < 5)
                    image.Index = -1;
            }

            if (IsMouseOver(CMain.MPoint))
            {
                if (_buffCount == 0 || (!_fadedIn && CMain.Time <= _nextFadeTime))
                    return;

                Opacity += FadeRate;
                _expandCollapseButton.Opacity += FadeRate;

                if (Opacity > 1f)
                {
                    Opacity = 1f;
                    _expandCollapseButton.Opacity = 1f;
                    _fadedIn = true;
                    _fadedOut = false;
                }

                _nextFadeTime = CMain.Time + FadeDelay;
            }
            else
            {
                if (!_fadedOut && CMain.Time <= _nextFadeTime)
                    return;

                Opacity -= FadeRate;
                _expandCollapseButton.Opacity -= FadeRate;

                if (Opacity < 0f)
                {
                    Opacity = 0f;
                    _expandCollapseButton.Opacity = 0f;
                    _fadedOut = true;
                    _fadedIn = false;
                }

                _nextFadeTime = CMain.Time + FadeDelay;
            }
        }

        private void UpdateWindow()
        {
            _buffCount = _buffList.Count;

            var baseImage = 20;
            var heightOffset = 36;

            if (_buffCount > 0 && Settings.ExpandedBuffWindow)
            {
                var oldWidth = Size.Width;

                if (_buffCount <= 10)
                    Index = baseImage + _buffCount - 1;
                else if (_buffCount > 10)
                    Index = baseImage + 10;
                else if (_buffCount > 20)
                    Index = baseImage + 11;
                else if (_buffCount > 30)
                    Index = baseImage + 12;
                else if (_buffCount > 40)
                    Index = baseImage + 13;

                var newX = Location.X - Size.Width + oldWidth;
                var newY = heightOffset;
                Location = new Point(newX, newY);

                _buffCountLabel.Visible = false;

                _expandCollapseButton.Location = new Point(Size.Width - 15, 0);
                Size = new Size((_buffCount > 10 ? 10 : _buffCount) * 23, 24 + (_buffCount / 10) * 24);
            }
            else if (_buffCount > 0 && !Settings.ExpandedBuffWindow)
            {
                var oldWidth = Size.Width;

                Index = 20;

                var newX = Location.X - Size.Width + oldWidth;
                var newY = heightOffset;
                Location = new Point(newX, newY);

                _buffCountLabel.Visible = true;
                _buffCountLabel.Text = $"{_buffCount}";
                _buffCountLabel.Location = new Point(Size.Width / 2 - _buffCountLabel.Size.Width / 2, Size.Height / 2 - 10);
                _buffCountLabel.BringToFront();

                _expandCollapseButton.Location = new Point(Size.Width - 15, 0);
                Size = new Size(44, 34);
            }
        }

        private string CombinedBuffText()
        {
            string text = "中毒\n";

            return text;
        }
    }

}

