using System.Text.Json;

namespace Server.MirEnvir
{
    public sealed class ValorSettings
    {
        public string MapFileName { get; set; } = "valor";
        public int MaximumPlayers { get; set; } = 100;
        // Unspecified by the supplied guide: these are adjustable server defaults.
        public int ScoreIntervalSeconds { get; set; } = 5;
        public int PersonalPointsPerTick { get; set; } = 1;
        public int MonumentRadius { get; set; } = 8;
        public int CompletionHonor { get; set; } = 50;
        public int VictoryHonor { get; set; } = 100;
        public int BufferDurationSeconds { get; set; } = 300;
        public int BufferRadius { get; set; } = 8;
        public int BufferAttackBonus { get; set; } = 10;
        public int BufferDefenceBonus { get; set; } = 10;
        public List<ValorReward> Rewards { get; set; } = new List<ValorReward>();

        public static ValorSettings Load()
        {
            const string path = "ValorSettings.json";
            ValorSettings settings;

            if (File.Exists(path))
            {
                settings = JsonSerializer.Deserialize<ValorSettings>(
                    File.ReadAllText(path),
                    new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true
                    });
            }
            else
            {
                settings = new ValorSettings();

                File.WriteAllText(
                    path,
                    JsonSerializer.Serialize(
                        settings,
                        new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }));
            }

            if (settings == null || string.IsNullOrWhiteSpace(settings.MapFileName)
                || settings.MaximumPlayers < 2 || settings.MaximumPlayers > 100
                || settings.ScoreIntervalSeconds < 1 || settings.ScoreIntervalSeconds > 60
                || settings.PersonalPointsPerTick < 0 || settings.PersonalPointsPerTick > 100
                || settings.MonumentRadius < 1 || settings.MonumentRadius > 30
                || settings.CompletionHonor < 0 || settings.CompletionHonor > 200000
                || settings.VictoryHonor < 0 || settings.VictoryHonor > 200000
                || settings.BufferDurationSeconds < 1 || settings.BufferDurationSeconds > 1200
                || settings.BufferRadius < 1 || settings.BufferRadius > 30
                || settings.BufferAttackBonus < 0 || settings.BufferAttackBonus > 1000
                || settings.BufferDefenceBonus < 0 || settings.BufferDefenceBonus > 1000
                || settings.Rewards == null || settings.Rewards.Any(r => r == null
                    || string.IsNullOrWhiteSpace(r.Item)
                    || r.HonorCost < 1
                    || r.HonorCost > 200000))
                throw new InvalidDataException("无效的 ValorSettings.json 配置值。");

            settings.MapFileName = Path.GetFileNameWithoutExtension(settings.MapFileName);

            return settings;
        }
    }

    public sealed class ValorReward
    {
        public string Item { get; set; } = "";
        public int HonorCost { get; set; }
    }
}
