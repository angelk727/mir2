using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static AutoPatche.Form1;

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
            InitializeDataGridView();
            // ��� DragEnter �� DragDrop �¼��������
            dataGridView1.DragEnter += new DragEventHandler(dataGridView1_DragEnter);
            dataGridView1.DragDrop += new DragEventHandler(dataGridView1_DragDrop);
        
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("FileName", "�ļ���");
            dataGridView1.Columns.Add("Md5", "MD5");
            dataGridView1.Columns.Add("Size", "�ļ���С");
        }
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (Directory.Exists(file))
                    {
                        AddFilesFromDirectory(file);
                    }
                    else
                    {
                        AddFileToDataGridView(file);
                    }
                }
            }
        }

        private void AddFilesFromDirectory(string directory)
        {
            var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                AddFileToDataGridView(file);
            }
        }

        Dictionary<string, AssetEntry> Updateassets = new Dictionary<string, AssetEntry>();
        private void AddFileToDataGridView(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string md5 = CalculateMD5(file);
            var assetEntry = new AssetEntry
            {
                Md5 = md5,
                Size = fileInfo.Length
            };
            string Updatefile = fileInfo.FullName.Replace(DirPath, "");
            dataGridView1.Rows.Add(Updatefile, assetEntry.Md5, assetEntry.Size);

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
                GenerateDirectoryIni();
            }
            else
            {
                MessageBox.Show("Ŀ¼·�������ڣ���ѡ����Ч��Ŀ¼·��");
            }
        }

        private void GenerateDirectoryIni()
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
            RemarkContent.Text = string.Empty;
            string outputPath = DirPathConten.Text; // �滻Ϊ������·��
            string outputDirectory = OutDirPath.Text;
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            outputPath = Path.Combine(outputDirectory, outputPath);
            var assets = new Dictionary<string, AssetEntry>();
            var files = Directory.GetFiles(DirPath, "*", SearchOption.AllDirectories);
            int totalFiles = files.Length;
            assets.Clear();
            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("����Ӹ����ļ�");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string fileName = row.Cells["FileName"].Value.ToString().Replace("\\", "/").TrimStart('/');
                string md5 = row.Cells["Md5"].Value.ToString();
                long size = Convert.ToInt64(row.Cells["Size"].Value);
                assets[fileName] = new AssetEntry { Md5 = md5, Size = size };
            }
            //ProcessDirectory(DirPath, totalFiles);
            //void ProcessDirectory(string currentDirectory, int totalFiles)
            //{
            //    var files = Directory.GetFiles(currentDirectory, "*", SearchOption.TopDirectoryOnly);
            //    var directories = Directory.GetDirectories(currentDirectory, "*", SearchOption.TopDirectoryOnly);

            //    foreach (var filePath in files)
            //    {
            //        if (File.Exists(filePath))
            //        {
            //            string relativePath = filePath.Replace(DirPath, "").Replace("\\", "/").TrimStart('/');
            //            string md5 = CalculateMD5(filePath);
            //            long size = new FileInfo(filePath).Length;
            //            assets[relativePath] = new AssetEntry { Md5 = md5, Size = size };
            //        }
            //    }

            //    foreach (var directory in directories)
            //    {
            //        ProcessDirectory(directory, totalFiles);
            //    }

            //    // ���½�����
            //    RemarkContent.Text += $"���ڴ���Ŀ¼: {currentDirectory} ({assets.Count}/{totalFiles})" + Environment.NewLine;
            //}

            var iniContent = new StringBuilder();
            iniContent.AppendLine("[RemoteManifest]");
            iniContent.AppendLine($"version={VersionNO.Text}");
            iniContent.AppendLine($"packageUrl={IPAddrContent.Text}");
            iniContent.AppendLine($"VersionUrl={IPAddrContent.Text}/{DirPathConten.Text}");
            iniContent.AppendLine("assets=");
            foreach (var asset in assets)
            {
                iniContent.AppendLine($"[{asset.Key}]");
                iniContent.AppendLine($"md5={asset.Value.Md5}");
                iniContent.AppendLine($"size={asset.Value.Size}");
            }

            File.WriteAllText(outputPath, iniContent.ToString());
            // ���������
            RemarkContent.Text += "Ŀ¼INI�������: " + outputPath + Environment.NewLine;
            MessageBox.Show("Ŀ¼INI�������: " + outputPath);
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
                var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"));
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
            File.WriteAllText("settings.json", JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveSettings();
            base.OnFormClosing(e);
        }

        public class AssetEntry
        {
            public string Md5 { get; set; }
            public long Size { get; set; }
        }

        public class RemoteManifest
        {
            public Dictionary<string, AssetEntry> assets { get; set; }
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

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "INI files (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog.Title = "ѡ��һ��INI�ļ�";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    try
                    {
                        var iniData = ReadIniFile(filePath);

                        if (iniData != null)
                        {
                            MessageBox.Show("��ȡINI�ļ��ɹ���\n" +
                                            $"�汾��: {iniData.version}\n" +
                                            $"��URL: {iniData.packageUrl}\n" +
                                            $"�汾URL: {iniData.VersionUrl}\n" +
                                            $"�ʲ�����: {iniData.assets.Count()}");
                        }
                        else
                        {
                            MessageBox.Show("��ȡINI�ļ�ʧ�ܣ��ļ�������Ч��");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"��ȡINI�ļ������з�������: {ex.Message}");
                    }
                }
            }
        }

        private RemoteManifest ReadIniFile(string filePath)
        {
            var data = new Dictionary<string, Dictionary<string, string>>();
            var currentSection = new Dictionary<string, string>();
            var sectionName = string.Empty;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var trimmedLine = line.Trim();

                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";"))
                {
                    continue;
                }

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    sectionName = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    currentSection = new Dictionary<string, string>();
                    data[sectionName] = currentSection;
                }
                else
                {
                    var keyValue = trimmedLine.Split(new[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        currentSection[keyValue[0].Trim()] = keyValue[1].Trim();
                    }
                }
            }

            var iniData = new RemoteManifest
            {
                version = data["RemoteManifest"]["version"],
                packageUrl = data["RemoteManifest"]["packageUrl"],
                VersionUrl = data["RemoteManifest"]["VersionUrl"],
                assets = new Dictionary<string, AssetEntry>()
            };

            foreach (var section in data)
            {
                if (section.Key != "RemoteManifest")
                {
                    var asset = new AssetEntry
                    {
                        Md5 = section.Value["md5"],
                        Size = long.Parse(section.Value["size"])
                    };
                    iniData.assets[section.Key] = asset;
                }
            }

            return iniData;
        }
    }
}
