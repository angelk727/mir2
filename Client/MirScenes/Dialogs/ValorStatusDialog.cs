using Client.MirControls;
using Client.MirGraphics;
using S = ServerPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class ValorStatusDialog : MirControl
    {
        public bool Active { get; private set; }
        private readonly MirControl _hud;
        private readonly ScoreFill _blueFill, _redFill;
        private readonly MirLabel _blueScore, _redScore, _clock;
        private readonly MirLabel[] _damage = new MirLabel[3], _health = new MirLabel[3];
        private readonly MirLabel _blueCount, _redCount;
        private readonly MirImageControl _sun, _moon, _lightning;
        private readonly MirImageControl _blueMonsters, _redMonsters;
        private readonly MirImageControl _board;
        private MirImageControl _boardButton;
        private readonly MirLabel _boardSummary, _pageLabel;
        private readonly MirLabel[,] _cells = new MirLabel[15, 7];
        private S.ValorStatus _status;
        private int _page;

        public ValorStatusDialog()
        {
            Size = new Size(684, 116);
            Location = new Point(Math.Max(0, (Settings.ScreenWidth - 684) / 2), 0);
            DrawControlTexture = false;
            NotControl = false;

            _hud = new MirControl { Parent = this, Location = new Point(208, 0), Size = new Size(330, 116), DrawControlTexture = false };
            Sprite(_hud, 970, 22, 0, 80, 36);

            _blueFill = new ScoreFill
            {
                Parent = _hud,
                Library = Libraries.Prguse2,
                Index = 971,
                Location = new Point(30, 12),
                Size = new Size(82, 22),
                AutoSize = false,
                FillFromRight = false
            };

            _redFill = new ScoreFill
            {
                Parent = _hud,
                Library = Libraries.Prguse2,
                Index = 972,
                Location = new Point(130, 11),
                Size = new Size(82, 22),
                AutoSize = false,
                FillFromRight = true
            };

            _blueScore = Label(_hud, new Point(24, 5), new Size(73, 22), Color.White, 10F);
            _redScore = Label(_hud, new Point(106, 5), new Size(73, 22), Color.White, 10F);
            _blueScore.DrawFormat = _redScore.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            _boardButton = new MirImageControl
            {
                Parent = this,
                Library = Libraries.Prguse2,
                Index = 976,
                Location = new Point(420, 10),
                Size = new Size(32, 32),
                Visible = true,
                Hint = "点击查看战场排行榜。"
            };
            _boardButton.Click += (o, e) => ToggleBoard();

            for (int i = 0; i < 3; i++)
            {
                int x = i * 84;

                Sprite(_hud, 973, x, 34, 76, 19);

                _damage[i] = Label(_hud, new Point(x + 2, 34), new Size(21, 18), Color.White, 8F);
                _health[i] = Label(_hud, new Point(x + 46, 34), new Size(25, 18), Color.White, 8F);

                _damage[i].DrawFormat = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
                _health[i].DrawFormat = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
            }

            _moon = Sprite(_hud, 980, 29, 34, 20, 20);
            _sun = Sprite(_hud, 983, 113, 34, 20, 20);
            _lightning = Sprite(_hud, 986, 197, 34, 20, 20);

            _blueMonsters = Sprite(_hud, 990, 9, 57, 34, 35);
            _redMonsters = Sprite(_hud, 993, 200, 57, 34, 35);

            _blueCount = Label(_hud, new Point(46, 67), new Size(37, 19), Color.White, 9F);
            _redCount = Label(_hud, new Point(170, 67), new Size(35, 19), Color.White, 9F);

            _clock = Label(GameScene.Scene, new Point(Math.Max(0, Settings.ScreenWidth - 199), Math.Max(90, Settings.ScreenHeight - 290)), new Size(170, 45), Color.FromArgb(244, 224, 157), 21F);
            _clock.Visible = false;
            _clock.NotControl = true;
            _clock.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            _board = new MirImageControl
            {
                Parent = this,
                Library = Libraries.Title,
                Index = 725,
                Size = new Size(684, 463),
                AutoSize = false,
                Location = new Point(0, 0),
                Visible = false
            };

            var title = Label(_board, new Point(230, 5), new Size(220, 22), Color.White, 11F);
            title.Text = "荣耀战场";
            title.BackColour = Color.FromArgb(255, 25, 22, 15);
            title.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            _boardSummary = Label(_board, new Point(222, 33), new Size(239, 25), Color.White, 8F);
            _boardSummary.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            var close = Label(_board, new Point(657, 3), new Size(23, 23), Color.White, 10F);
            close.Click += (o, e) => CloseBoard();

            int[] positions = { 16, 170, 211, 303, 394, 485, 576 };
            int[] widths = { 153, 40, 91, 90, 90, 90, 90 };
            string[] headers = { "名称", "等级", "个人", "击杀", "死亡", "奖励", "荣誉" };

            for (int col = 0; col < 7; col++)
            {
                var header = Label(_board, new Point(positions[col], 98), new Size(widths[col], 21), Color.Gold, 8F);
                header.Text = headers[col];
                header.BackColour = Color.FromArgb(255, 35, 33, 22);
                header.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

                for (int row = 0; row < 15; row++)
                {
                    _cells[row, col] = Label(_board, new Point(positions[col] + 2, 120 + row * 20), new Size(widths[col] - 4, 20), Color.White, 8F);
                    _cells[row, col].DrawFormat = col == 0 ? TextFormatFlags.VerticalCenter : TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                }
            }

            var previous = Label(_board, new Point(200, 433), new Size(60, 22), Color.Gold, 9F);
            previous.Text = "< 上一页";
            previous.Click += (o, e) => { if (_page > 0) { _page--; RefreshRows(); } };

            var next = Label(_board, new Point(425, 433), new Size(60, 22), Color.Gold, 9F);
            next.Text = "下一页 >";
            next.Click += (o, e) => { if (_status != null && (_page + 1) * 15 < _status.Rows.Count) { _page++; RefreshRows(); } };

            _pageLabel = Label(_board, new Point(285, 433), new Size(115, 22), Color.White, 9F);
            _pageLabel.DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        }

        private static MirLabel Label(MirControl parent, Point location, Size size, Color colour, float fontSize)
            => new MirLabel
            {
                Parent = parent,
                Location = location,
                Size = size,
                ForeColour = colour,
                BackColour = Color.Transparent,
                Font = new Font(Settings.FontName, fontSize),
                OutLine = true,
                OutLineColour = Color.Black,
                NotControl = false
            };

        private static MirImageControl Sprite(MirControl parent, int index, int x, int y, int width, int height)
            => new MirImageControl
            {
                Parent = parent,
                Library = Libraries.Prguse2,
                Index = index,
                Location = new Point(x, y),
                Size = new Size(width, height),
                AutoSize = false
            };

        private static string Team(byte team) => team == 1 ? "红方" : team == 2 ? "蓝方" : "中立";

        private static int MonumentSprite(int neutral, byte team) => neutral + (team == 1 ? 2 : team == 2 ? 1 : 0);

        private void ToggleBoard()
        {
            _board.Visible = !_board.Visible;

            if (_board.Visible)
            {
                _boardButton.Index = 977;
            }
            else
            {
                _boardButton.Index = 976;
            }

            UpdateSize();
            RefreshRows();
        }

        public void CloseBoard()
        {
            _board.Visible = false;
            _boardButton.Index = 976;

            UpdateSize();

            if (!Active)
                Visible = false;
        }

        public void UpdateStatus(S.ValorStatus status)
        {
            bool wasActive = Active;

            Active = status.Active;
            _status = status;

            if (status.Rows.Count == 0)
            {
                Visible = false;
                _clock.Visible = false;
                _board.Visible = false;
                return;
            }

            Visible = true;
            _hud.Visible = Active;
            _clock.Visible = Active;

            _clock.Text = $"{status.Seconds / 60:00}:{status.Seconds % 60:00}";

            _blueScore.Text = status.BlueScore.ToString("N0");
            _redScore.Text = status.RedScore.ToString("N0");

            _blueFill.Points = status.BlueScore;
            _redFill.Points = status.RedScore;

            _damage[0].Text = status.MoonDamage.ToString();
            _health[0].Text = status.MoonHealth.ToString();

            _damage[1].Text = status.SunDamage.ToString();
            _health[1].Text = status.SunHealth.ToString();

            _damage[2].Text = status.LightningDamage.ToString();
            _health[2].Text = status.LightningHealth.ToString();

            _blueCount.Text = status.BlueMonsters.ToString();
            _redCount.Text = status.RedMonsters.ToString();

            _blueMonsters.Index = status.BlueMonsterState;
            _redMonsters.Index = status.RedMonsterState;

            _sun.Index = MonumentSprite(983, status.SunAttacker);
            _moon.Index = MonumentSprite(980, status.MoonAttacker);
            _lightning.Index = MonumentSprite(986, status.LightningAttacker);

            _boardSummary.Text = Active
                ? $"红方 {status.RedScore} / 蓝方 {status.BlueScore}"
                : status.Winner == 0
                    ? "结果：平局"
                    : $"获胜方：{Team(status.Winner)}";

            if (!Active && wasActive)
            {
                _board.Visible = true;
                _boardButton.Index = 977;
            }

            UpdateSize();
            RefreshRows();
        }

        private void UpdateSize() => Size = new Size(684, _board.Visible ? 463 : 116);

        private sealed class ScoreFill : MirImageControl
        {
            private int _points;

            public bool FillFromRight { get; set; }

            public int Points
            {
                set { _points = Math.Max(0, Math.Min(7500, value)); }
            }

            public ScoreFill()
            {
                DrawImage = false;
            }

            protected internal override void DrawControl()
            {
                base.DrawControl();

                if (Library == null || _points == 0) return;

                Size imageSize = Library.GetSize(Index);

                if (imageSize.Width <= 0 || imageSize.Height <= 0) return;

                int width = (int)((long)imageSize.Width * _points / 7500);

                if (width == 0)
                    width = 1;

                int start = FillFromRight ? imageSize.Width - width : 0;
                Point destination = new Point(DisplayLocation.X + start, DisplayLocation.Y);

                Library.Draw(Index, new Rectangle(start, 0, width, imageSize.Height), destination, Color.White, false);
            }
        }

        private void RefreshRows()
        {
            if (_status == null) return;

            _page = Math.Min(_page, Math.Max(0, (_status.Rows.Count - 1) / 15));
            _pageLabel.Text = $"{_page + 1} / {Math.Max(1, (_status.Rows.Count + 14) / 15)}";

            for (int row = 0; row < 15; row++)
            {
                int index = _page * 15 + row;

                var entry = index < _status.Rows.Count ? _status.Rows[index] : null;

                string[] values = entry == null
                    ? new string[7]
                    : new[]
                    {
                        entry.Name,
                        entry.Level.ToString(),
                        entry.Personal.ToString(),
                        entry.Kills.ToString(),
                        entry.Deaths.ToString(),
                        entry.Bonus.ToString(),
                        entry.Honor.ToString()
                    };

                for (int col = 0; col < 7; col++)
                {
                    _cells[row, col].Text = values[col] ?? "";
                    _cells[row, col].ForeColour = entry == null
                        ? Color.White
                        : entry.Team == 1
                            ? Color.Red
                            : Color.CornflowerBlue;
                }
            }
        }
    }
}