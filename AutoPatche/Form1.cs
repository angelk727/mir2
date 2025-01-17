using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AutoPatche
{
    public partial class Form1 : Form
    {
        TextBox RemarkContent,
            OutDirPath,
            IPAddrContent,
            DirPathConten,
            VersionNO;

        public Form1()
        {
            InitializeComponent();
            RemarkContent = textBox2;
            OutDirPath = textBox3;
            IPAddrContent = textBox4;
            DirPathConten = textBox5;
            VersionNO = textBox6;
            LoadSettings();
        }

        string DirPath = string.Empty;

        private void button2_Click(object sender, EventArgs e)
        {
            string FILE = SelectDirectory();
            if (string.IsNullOrEmpty(FILE))
            {
                MessageBox.Show("Ŀ¼·�������ڣ���ѡ����Ч��Ŀ¼·��");
                return;
            }
            textBox1.Text = DirPath = FILE;
        }

        private string SelectDirectory()
        {
            string FILE = string.Empty;
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    FILE = folderBrowserDialog.SelectedPath;
                    MessageBox.Show($"Selected Directory: {DirPath}");
                }
            }
            return FILE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(DirPath))
            {
                GenerateDirectoryJson();
            }
            else
            {
                MessageBox.Show("Ŀ¼·�������ڣ���ѡ����Ч��Ŀ¼·��");
            }
        }

        private void GenerateDirectoryJson()
        {
            if (string.IsNullOrEmpty(DirPath))
            {
                MessageBox.Show("δѡ���κ�Ŀ¼");
                return;
            }

            if (!ValidateOutputDirectory(OutDirPath.Text))
            {
                MessageBox.Show("���Ŀ¼��Ч����������Ч�����Ŀ¼·��");
                return;
            }

            //if (!ValidateIPAddress(IPAddrContent.Text))
            //{
            //    MessageBox.Show("IP��ַ��Ч����������Ч��IP��ַ");
            //    return;
            //}

            if (!ValidateVersion(VersionNO.Text))
            {
                MessageBox.Show("�汾����Ч����������Ч�İ汾��");
                return;
            }

            if (string.IsNullOrEmpty(DirPathConten.Text))
            {
                MessageBox.Show("Ŀ¼·����Ч����������Ч��Ŀ¼·��");
                return;
            }
            RemarkContent.Text=string.Empty;
            string outputPath = DirPathConten.Text; // �滻Ϊ������·��
            string outputDirectory = OutDirPath.Text;
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            outputPath = Path.Combine(outputDirectory, outputPath);
            var assets = new Dictionary<string, Asset>();
            var files = Directory.GetFiles(DirPath, "*", SearchOption.AllDirectories);
            int totalFiles = files.Length;
            ProcessDirectory(DirPath, totalFiles);

            void ProcessDirectory(string currentDirectory, int totalFiles)
            {
                var files = Directory.GetFiles(currentDirectory, "*", SearchOption.TopDirectoryOnly);
                var directories = Directory.GetDirectories(currentDirectory, "*", SearchOption.TopDirectoryOnly);

                foreach (var filePath in files)
                {
                    if (File.Exists(filePath))
                    {
                        string relativePath = filePath.Replace(DirPath, "").Replace("\\", "/").TrimStart('/');
                        string md5 = CalculateMD5(filePath);
                        long size = new FileInfo(filePath).Length;

                        assets[relativePath] = new Asset { md5 = md5, size = size };
                    }
                }

                foreach (var directory in directories)
                {
                    ProcessDirectory(directory, totalFiles);
                }

                // ���½�����
                RemarkContent.Text += $"���ڴ���Ŀ¼: {currentDirectory} ({assets.Count}/{totalFiles})" + Environment.NewLine;
            }

            var root = new Root
            {
                assets = assets,
                packageUrl = IPAddrContent.Text,
                VersionUrl = IPAddrContent.Text + "/" + DirPathConten.Text,
                version = VersionNO.Text
            };

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            File.WriteAllText(outputPath, json);
            // ���������
            RemarkContent.Text += "Ŀ¼JSON�������: " + outputPath + Environment.NewLine;
            MessageBox.Show("Ŀ¼JSON�������: " + outputPath);
        }

        private static string CalculateMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return System.BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private bool ValidateOutputDirectory(string path)
        {
            return Directory.Exists(Path.GetDirectoryName(path));
        }

        private bool ValidateIPAddress(string ipAddress)
        {
            string pattern = @"^(http:\/\/|https:\/\/)?([a-zA-Z0-9\-\.]+)(:\d+)?(\/)?$";
            return Regex.IsMatch(ipAddress, pattern);
        }

        private bool ValidateVersion(string version)
        {
            string pattern = @"^\d+\.\d+\.\d+\.\d+$";
            return Regex.IsMatch(version, pattern);
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
                RemarkContent.Text = settings.RemarkContent;
                OutDirPath.Text = settings.OutDirPath;
                IPAddrContent.Text = settings.IPAddrContent;
                DirPathConten.Text = settings.DirPathConten;
                VersionNO.Text = settings.VersionNO;
            }
        }

        private void SaveSettings()
        {
            var settings = new Settings
            {
                RemarkContent = RemarkContent.Text,
                OutDirPath = OutDirPath.Text,
                IPAddrContent = IPAddrContent.Text,
                DirPathConten = DirPathConten.Text,
                VersionNO = VersionNO.Text
            };
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            base.OnFormClosing(e);
        }

        public class Asset
        {
            public string md5 { get; set; }
            public long size { get; set; }
        }

        public class Root
        {
            public Dictionary<string, Asset> assets { get; set; }
            public string packageUrl { get; set; }
            public string VersionUrl { get; set; }
            public string version { get; set; }
        }

        public class Settings
        {
            public string RemarkContent { get; set; }
            public string OutDirPath { get; set; }
            public string IPAddrContent { get; set; }
            public string DirPathConten { get; set; }
            public string VersionNO { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FILE = SelectDirectory();
            if (string.IsNullOrEmpty(FILE))
            {
                MessageBox.Show("Ŀ¼·�������ڣ���ѡ����Ч��Ŀ¼·��");
                return;
            }
            OutDirPath.Text = FILE;
        }
    }
}
