using System.IO;

public class ClientMonsterInfo
{
    public int Index;
    public string Name = string.Empty;
    public string GameName = string.Empty;
    public Monster Image;
    public byte Effect, ViewRange, CoolEye;
    public ushort AI, Level;
    public byte Light;
    public ushort AttackSpeed, MoveSpeed;
    public uint Experience;
    public bool CanTame, CanPush, AutoRev, Undead, CanRecall, IsBoss;
    // Stats for detailed tooltip
    public Stats Stats;

    public ClientMonsterInfo()
    {
        Stats = new Stats();
    }

    public ClientMonsterInfo(BinaryReader reader)
    {
        Index = reader.ReadInt32();
        Name = reader.ReadString();
        GameName = reader.ReadString();
        Image = (Monster)reader.ReadUInt16();
        AI = reader.ReadUInt16();
        Effect = reader.ReadByte();
        Level = reader.ReadUInt16();
        ViewRange = reader.ReadByte();
        CoolEye = reader.ReadByte();
        Light = reader.ReadByte();
        AttackSpeed = reader.ReadUInt16();
        MoveSpeed = reader.ReadUInt16();
        Experience = reader.ReadUInt32();
        CanPush = reader.ReadBoolean();
        CanTame = reader.ReadBoolean();
        AutoRev = reader.ReadBoolean();
        Undead = reader.ReadBoolean();
        CanRecall = reader.ReadBoolean();
        IsBoss = reader.ReadBoolean();
        // Read stats
        Stats = new Stats(reader);
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Index);
        writer.Write(Name);
        writer.Write(GameName);
        writer.Write((ushort)Image);
        writer.Write(AI);
        writer.Write(Effect);
        writer.Write(Level);
        writer.Write(ViewRange);
        writer.Write(CoolEye);
        writer.Write(Light);
        writer.Write(AttackSpeed);
        writer.Write(MoveSpeed);
        writer.Write(Experience);
        writer.Write(CanPush);
        writer.Write(CanTame);
        writer.Write(AutoRev);
        writer.Write(Undead);
        writer.Write(CanRecall);
        writer.Write(IsBoss);
        // Write stats
        Stats.Save(writer);
    }

}

