namespace Server.MirEnvir
{
    // Honor and exchanged inventory items use the same existing character persistence.
    public sealed class ValorHonorStore
    {
        public int Get(int characterIndex)
        {
            var info = Envir.Main.GetCharacterInfo(characterIndex);
            if (info == null) throw new InvalidOperationException("荣耀战场角色记录不存在");
            return info.ValorHonor;
        }

        public void SetMany(Dictionary<int, int> changes)
        {
            // Resolve every record before applying any change.
            var records = changes.Select(p => new { Info = Envir.Main.GetCharacterInfo(p.Key), Points = p.Value }).ToList();
            if (records.Any(p => p.Info == null)) throw new InvalidOperationException("荣耀战场角色记录不存在");
            foreach (var record in records) record.Info.ValorHonor = Math.Clamp(record.Points, 0, 200000);
        }
    }
}
