using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoPatcheWpf.VM
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _RemarkContent;

        [ObservableProperty]
        private string _OutDirPath;

        [ObservableProperty]
        private string _IpAddrContent;

        [ObservableProperty]
        private string _DirPathConten;

        [ObservableProperty]
        private string _VersionNO;

        [ObservableProperty]
        private ObservableCollection<AssetEntry> _Assets;

        public MainWindowViewModel()
        {
            Assets = new ObservableCollection<AssetEntry>();
            LoadSettingsFromIni();
        }
        private void SaveSettingsToIni()
        {
            var iniContent = new StringBuilder();
            iniContent.AppendLine("[Settings]");
            iniContent.AppendLine($"OutDirPath={OutDirPath}");
            File.WriteAllText("config.ini", iniContent.ToString());
        }

        private void LoadSettingsFromIni()
        {
            if (File.Exists("config.ini"))
            {
                var iniContent = File.ReadAllText("config.ini");
                var lines = iniContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var line in lines)
                {
                    if (line.StartsWith("OutDirPath="))
                    {
                        OutDirPath = line.Substring("OutDirPath=".Length).Trim();
                    }
                }
            }
        }

        [RelayCommand]
        private void SelectDirectory()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OutDirPath = dialog.SelectedPath;
                SaveSettingsToIni();
                MessageBox.Show($"Selected Directory: {OutDirPath}");
            }
        }

        [RelayCommand]
        private void GenerateDirectoryIni()
        { // 添加选择目录的逻辑
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            string SaveDirPath = string.Empty;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveDirPath = dialog.SelectedPath;
                MessageBox.Show($"Selected Directory: {DirPathConten}");
            }
            else
            {
                MessageBox.Show("未选择任何目录");
                return;
            }
            if (string.IsNullOrEmpty(DirPathConten))
            {
                MessageBox.Show("未选择任何目录");
                return;
            }

            if (!ValidateOutputDirectory(OutDirPath))
            {
                MessageBox.Show("输出目录无效，请输入有效的输出目录路径");
                return;
            }

            if (!ValidateVersion(VersionNO))
            {
                MessageBox.Show("版本号无效，请输入有效的版本号");
                return;
            }

            if (string.IsNullOrEmpty(DirPathConten))
            {
                MessageBox.Show("目录路径无效，请输入有效的目录路径");
                return;
            }

            RemarkContent = string.Empty;
            string outputPath = DirPathConten; // 替换为你的输出路径
            string outputDirectory = SaveDirPath;
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            outputPath = Path.Combine(outputDirectory, outputPath);
            var assets = new Dictionary<string, AssetEntry>();
            assets.Clear();

            foreach (var asset in Assets)
            {
                assets[asset.FileName] = new AssetEntry { Md5 = asset.Md5, Size = asset.Size };
            }

            var iniContent = new StringBuilder();
            iniContent.AppendLine("[RemoteManifest]");
            iniContent.AppendLine($"version={VersionNO}");
            iniContent.AppendLine($"packageUrl={IpAddrContent}");
            iniContent.AppendLine($"VersionUrl={IpAddrContent}/{DirPathConten}");
            iniContent.AppendLine("assets=");
            foreach (var asset in assets)
            {
                iniContent.AppendLine($"[{asset.Key}]");
                iniContent.AppendLine($"md5={asset.Value.Md5}");
                iniContent.AppendLine($"size={asset.Value.Size}");
            }

            File.WriteAllText(outputPath, iniContent.ToString());
            RemarkContent += "目录INI生成完毕: " + outputPath + Environment.NewLine;
            MessageBox.Show("目录INI生成完毕: " + outputPath);
        }

        [RelayCommand]
        private void ImportConfigFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "INI files (*.ini)|*.ini|All files (*.*)|*.*",
                Title = "选择一个INI文件"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    var iniContent = File.ReadAllText(filePath);
                    var lines = iniContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    var assets = new Dictionary<string, AssetEntry>();
                    string currentSection = string.Empty;

                    foreach (var line in lines)
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            currentSection = line.Trim('[', ']');
                        }
                        else if (!string.IsNullOrWhiteSpace(line) && currentSection == "RemoteManifest")
                        {
                            var keyValue = line.Split('=');
                            if (keyValue.Length == 2)
                            {
                                switch (keyValue[0].Trim())
                                {
                                    case "version":
                                        VersionNO = keyValue[1].Trim();
                                        break;
                                    case "packageUrl":
                                        IpAddrContent = keyValue[1].Trim();
                                        break;
                                    case "VersionUrl":
                                        DirPathConten = keyValue[1].Trim().Replace(IpAddrContent,"");
                                        if(DirPathConten.StartsWith("/"))
                                        {
                                            DirPathConten = DirPathConten.Substring(1);
                                        }
                                        break;
                                }
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(line) && currentSection != "RemoteManifest")
                        {
                            var keyValue = line.Split('=');
                            if (keyValue.Length == 2)
                            {
                                if (assets.ContainsKey(currentSection))
                                {
                                    if (keyValue[0].Trim() == "md5")
                                    {
                                        assets[currentSection].Md5 = keyValue[1].Trim();
                                    }
                                    else if (keyValue[0].Trim() == "size")
                                    {
                                        assets[currentSection].Size = long.Parse(keyValue[1].Trim());
                                    }
                                }
                                else
                                {
                                    assets[currentSection] = new AssetEntry { FileName = currentSection };
                                    if (keyValue[0].Trim() == "md5")
                                    {
                                        assets[currentSection].Md5 = keyValue[1].Trim();
                                    }
                                    else if (keyValue[0].Trim() == "size")
                                    {
                                        assets[currentSection].Size = long.Parse(keyValue[1].Trim());
                                    }
                                }
                            }
                        }
                    }

                    Assets.Clear();
                    foreach (var asset in assets.Values)
                    {
                        Assets.Add(asset);
                    }

                    MessageBox.Show("配置文件导入成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导入配置文件时发生错误: {ex.Message}");
                }
            }
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
        public void AddFilesFromDirectory(string directory)
        {
            var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                AddFileToDataGridView(file);
            }
        }

        public void AddFileToDataGridView(string file)
        {
            if (OutDirPath == string.Empty)
            {
                MessageBox.Show("请先选择目录");
                return;
            }
            FileInfo fileInfo = new FileInfo(file);
            string md5 = CalculateMD5(file);
            if(Assets.Contains(new AssetEntry { FileName = fileInfo.FullName.Replace(OutDirPath, "") }))
            {
                MessageBox.Show("文件已存在");
                return;
            }
            else
            {
                var assetEntry = new AssetEntry
                {
                    FileName = fileInfo.FullName.Replace(OutDirPath, ""),
                    Md5 = md5,
                    Size = fileInfo.Length
                };
                Assets.Add(assetEntry);
            }
           
        }
        private bool ValidateOutputDirectory(string path)
        {
            return Directory.Exists(Path.GetDirectoryName(path));
        }

        private bool ValidateVersion(string version)
        {
            string pattern = @"^\d+\.\d+\.\d+\.\d+$";
            return Regex.IsMatch(version, pattern);
        }

        public class AssetEntry
        {
            public string FileName { get; set; }
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
    }
}
