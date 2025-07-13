using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace File_Sorter
{
    public partial class Form1 : Form
    {
        private readonly HashSet<string> blacklist = new() { ".blend1", ".tmp", ".bak" };
        private readonly HashSet<string> systemFormats = new() { ".lnk", ".url", ".pif", ".scf", ".shs", ".shb", ".xnk", ".exe", ".ini" };
        private IEnumerable<string> files = new List<string>();
        private int all_folders = 0;
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sorting_Log.txt");

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private Config LoadJsonConfig()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(exePath, "config.json");

            if (!File.Exists(configPath))
            {
                MessageBox.Show("File config.json not found near to .exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                string json = File.ReadAllText(configPath);
                return System.Text.Json.JsonSerializer.Deserialize<Config>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Got error while reading config.json:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private bool SortFilesFromJson(string file, string ext, Config config)
        {
            string detectedCategory = DetectRealCategory(file, ext);
            if (detectedCategory != null)
            {
                MoveFileToFolder(file, detectedCategory);
                return true;
            }

            foreach (var category in config.FileCategories)
            {
                if (category.Extensions.Contains(ext))
                {
                    MoveFileToFolder(file, category.TargetFolder);
                    return true;
                }
            }

            return false;
        }

        private string DetectByMagicBytes(string path)
        {
            byte[] buffer = new byte[8];
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                fs.Read(buffer, 0, buffer.Length);

                if (buffer[0] == 0x89 && buffer[1] == 0x50) return "Images"; // PNG
                if (buffer[0] == 0x50 && buffer[1] == 0x4B) return "Archives"; // ZIP
                if (buffer[0] == 'M' && buffer[1] == 'Z') return "Apps"; // EXE/DLL
                if (buffer[0] == 0x7F && buffer[1] == 'E' && buffer[2] == 'L') return "Apps"; // ELF
                if (buffer[0] == 0x1F && buffer[1] == 0x8B) return "Archives"; // Gzip
                if (buffer[0] == 0xFF && buffer[1] == 0xD8) return "Images"; // JPEG
                if (buffer[0] == 0x25 && buffer[1] == 0x50) return "Documents"; // PDF
                if (buffer[0] == 0xD0 && buffer[1] == 0xCF) return "Documents"; // DOC/XLS
                if (buffer[0] == 0x38 && buffer[1] == 0x42) return "Images"; // PSD
                if (buffer[0] == 0x49 && buffer[1] == 0x49) return "Images"; // TIFF
                if (buffer[0] == 0x42 && buffer[1] == 0x4D) return "Images"; // BMP
                if (buffer[0] == 0x00 && buffer[1] == 0x01 && buffer[2] == 0x00) return "Images"; // ICO
                if (buffer[0] == 0x4F && buffer[1] == 0x67 && buffer[2] == 0x67) return "Audio"; // OGG
                if (buffer[0] == 0x52 && buffer[1] == 0x49 && buffer[2] == 0x46 && buffer[3] == 0x46) return "Audio"; // WAV/AVI

                return null;
            }
            catch { return null; }
        }

        private string DetectByContentText(string path)
        {
            try
            {
                string text = File.ReadLines(path).FirstOrDefault()?.Trim();
                if (string.IsNullOrEmpty(text)) return null;

                if (text.StartsWith("{") || text.StartsWith("[")) return "Scripts";
                if (text.StartsWith("<?xml")) return "Documents";
                if (text.Contains("=") && text.StartsWith("[")) return "Scripts";
                if (text.Contains("import") || text.Contains("function") || text.Contains("def ")) return "Scripts";

                return null;
            }
            catch { return null; }
        }

        private string DetectRealCategory(string path, string ext)
        {
            ext = ext.ToLower();
            var ambiguousExts = new HashSet<string> { ".ts", ".obj", ".bin", ".uasset", "" };

            if (ambiguousExts.Contains(ext))
            {
                string contentGuess = DetectByContentText(path);
                if (!string.IsNullOrEmpty(contentGuess))
                {
                    Log($"[ContentDetection] {Path.GetFileName(path)} ({ext}) - {contentGuess}");
                    return contentGuess;
                }

                string magicGuess = DetectByMagicBytes(path);
                if (!string.IsNullOrEmpty(magicGuess))
                {
                    Log($"[MagicBytesDetection] {Path.GetFileName(path)} ({ext}) - {magicGuess}");
                    return magicGuess;
                }

                if (ext == ".uasset")
                {
                    Log($"[UAssetFallback] {Path.GetFileName(path)} - GameAssets");
                    return "GameAssets";
                }

                if (string.IsNullOrEmpty(ext))
                {
                    Log($"[UnknownExtension] {Path.GetFileName(path)} - Other");
                    return "Other";
                }
            }

            return null;
        }

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n");
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = L_ChooseFolder;
            ToolTip t = new();
            t.SetToolTip(CB_SortInside, "Sorting files inside folders inside directory");
            t.SetToolTip(L_Settings, "Open settings file");
            RoundButton(B_Sort);
        }

        private void RoundButton(Button button, int radius = 15)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.White;

            GraphicsPath outerPath = new();
            outerPath.AddArc(-1, -1, radius + 4, radius + 4, 180, 90);
            outerPath.AddArc(button.Width - 6 - radius, -1, radius + 4, radius + 4, 270, 90);
            outerPath.AddArc(button.Width - 6 - radius, button.Height - 6 - radius, radius + 4, radius + 4, 0, 90);
            outerPath.AddArc(-1, button.Height - 6 - radius, radius + 4, radius + 4, 90, 90);
            outerPath.CloseAllFigures();

            GraphicsPath innerPath = new();
            innerPath.AddArc(1, 1, radius - 2, radius - 2, 180, 90);
            innerPath.AddArc(button.Width - radius - 2, 1, radius - 2, radius - 2, 270, 90);
            innerPath.AddArc(button.Width - radius - 2, button.Height - 2 - radius, radius - 2, radius - 2, 0, 90);
            innerPath.AddArc(1, button.Height - 2 - radius, radius - 2, radius - 2, 90, 90);
            innerPath.CloseAllFigures();

            button.Region = new Region(outerPath);

            button.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                using var blackBrush = new SolidBrush(Color.Black);
                e.Graphics.FillPath(blackBrush, outerPath);

                using var orangeBrush = new SolidBrush(Color.Orange);
                e.Graphics.FillPath(orangeBrush, innerPath);

                TextRenderer.DrawText(e.Graphics, button.Text, button.Font, button.ClientRectangle, button.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void CB_SortInside_CheckedChanged(object sender, EventArgs e)
        {
            all_folders = CB_SortInside.Checked ? 1 : 0;
        }

        private void B_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Sorter v1.3.4\n\nAuthor: HOCKI1", "About");
        }

        private void L_Settings_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "./config.json");
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sortingPathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void B_Sort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sortingPathTextBox.Text))
            {
                MessageBox.Show("Insert sorting folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var config = LoadJsonConfig();
            if (config == null) return;

            files = Directory.EnumerateFiles(
                sortingPathTextBox.Text,
                "*.*",
                all_folders == 1 ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (blacklist.Contains(ext))
                {
                    File.Delete(file);
                    continue;
                }

                if (!SortFilesFromJson(file, ext, config) && !systemFormats.Contains(ext))
                {
                    MoveFileToFolder(file, "Other");
                }

                Application.DoEvents();
            }

            MessageBox.Show("Sorting finished!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MoveFileToFolder(string file, string folderName)
        {
            var targetDir = Path.Combine(sortingPathTextBox.Text, folderName);
            Directory.CreateDirectory(targetDir);

            var fileName = Path.GetFileName(file);
            var targetPath = Path.Combine(targetDir, fileName);

            
            if (File.Exists(targetPath))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);
                int counter = 1;

                do
                {
                    string newFileName = $"{name} ({counter}){ext}";
                    targetPath = Path.Combine(targetDir, newFileName);
                    counter++;
                } while (File.Exists(targetPath));

                Log($"Renamed copy: {Path.GetFileName(file)} - {Path.GetFileName(targetPath)}");
            }

            File.Move(file, targetPath);
            Log($"Moved: {Path.GetFileName(file)} - {folderName}");
        }

    }

    public class FileCategory
    {
        public string TargetFolder { get; set; }
        public List<string> Extensions { get; set; }
    }

    public class Config
    {
        public List<FileCategory> FileCategories { get; set; }
    }
}
