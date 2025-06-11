using System;
using System.IO;
using System.Linq;

namespace File_Sorter
{
    public partial class Form1 : Form
    {
        public string blacklist_formats = ".blend1"; //�������� �������
        public string system_formats = ".lnk .url .pif .scf .shs .shb .xnk .exe .ini"; //��������� �������
        public string newFolder = "";
        List<string> settings = new List<string>();
        StreamReader settingsFile = new StreamReader("settings.ini"); //���������� ������ �����

        public class ConfigString
        {
            public string folder_name;
            public string file_extentions;

        }

        List<ConfigString> configStrings = new List<ConfigString>(); // ������ ����� ������������(���� - ��������)

        public int all_folders = 0;

        IEnumerable<string> files = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            sortingPathTextBox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void B_Sort_Click(object sender, EventArgs e)
        {
            string fileformat = "";

            if (sortingPathTextBox.Text != "")
            {
                //����� ���� ������ � �����������
                if (all_folders == 0)
                {
                    files = Directory.EnumerateFiles(sortingPathTextBox.Text, "*.*", SearchOption.TopDirectoryOnly);

                }
                else if (all_folders == 1)
                {
                    files = Directory.EnumerateFiles(sortingPathTextBox.Text, "*.*", SearchOption.AllDirectories);
                }
                else
                {

                }

                //������ �������� � ����
                string temp = "";
                while (!settingsFile.EndOfStream)
                {
                    temp = settingsFile.ReadLine().ToString();
                    settings.Add(temp);
                }
                //////


                string[] strings = settings.ToArray();
                string temp2 = "";

                //������������ ������ �� ������
                foreach (string file in files)
                {
                    //��������� ������� �����
                    fileformat = Path.GetExtension(file).ToLower();

                    //�������� ������ ������ � ����������
                    foreach (string setting in settings)
                    {
                        //���������� ������
                        temp2 = setting.Substring(setting.IndexOf("=") + 1);
                        //�������� �����
                        newFolder = setting.Substring(0, setting.IndexOf('=') - 1);
                        newFolder = "\\" + newFolder + "\\";

                        //�������� ������� ����� � ����������� ������
                        if (temp2.Contains(fileformat))
                        {
                            //���� ���������� ����������, �� ����� �� ����������, �� ����������� 
                            if (Directory.Exists(sortingPathTextBox.Text + newFolder) && !File.Exists(sortingPathTextBox.Text + newFolder + Path.GetFileName(file)))
                            {
                                try
                                {
                                    File.Move(file, sortingPathTextBox.Text + newFolder + Path.GetFileName(file));
                                }
                                catch
                                {

                                }
                            }
                            //���� ���������� �� ����������, �� �������
                            else if (!Directory.Exists(sortingPathTextBox.Text + newFolder))
                            {
                                try
                                {
                                    Directory.CreateDirectory(sortingPathTextBox.Text + newFolder);
                                    File.Move(file, sortingPathTextBox.Text + newFolder + Path.GetFileName(file));
                                }
                                catch
                                {

                                }
                            }
                            //���� ���������� ���������� � ���� ����������, �� ������ �� ������
                            else if (Directory.Exists(sortingPathTextBox.Text + newFolder) && File.Exists(sortingPathTextBox.Text + newFolder + Path.GetFileName(file)))
                            {

                            }
                        }
                    }
                }

                //���������� ���������
                foreach (string file in files)
                {
                    //��������� ������� �����
                    fileformat = Path.GetExtension(file).ToLower();

                    //���� ���� ����������
                    if (File.Exists(sortingPathTextBox.Text + "\\" + Path.GetFileName(file)))
                    {
                        //���� ���� � ���������
                        if (blacklist_formats.Contains(fileformat))
                        {
                            File.Delete(file);
                        }
                    }
                }

                //���������� ����� other
                foreach (string file in files)
                {
                    //��������� ������� �����
                    fileformat = Path.GetExtension(file).ToLower();

                    //���� ���� ���������� � �� � ���������, �� �������� � Other
                    if (File.Exists(sortingPathTextBox.Text + "\\" + Path.GetFileName(file)) && !system_formats.Contains(fileformat))
                    {
                        string otherFolder = "\\Other\\";
                        //���� ���������� ����������, �� ����� �� ����������, �� ����������� 
                        if (Directory.Exists(sortingPathTextBox.Text + otherFolder) && !File.Exists(sortingPathTextBox.Text + otherFolder + Path.GetFileName(file)))
                        {
                            try
                            {
                                File.Move(file, sortingPathTextBox.Text + otherFolder + Path.GetFileName(file));
                            }
                            catch
                            {

                            }
                        }
                        //���� ���������� �� ����������, �� �������
                        else if (!Directory.Exists(sortingPathTextBox.Text + otherFolder))
                        {
                            try
                            {
                                Directory.CreateDirectory(sortingPathTextBox.Text + otherFolder);
                                File.Move(file, sortingPathTextBox.Text + otherFolder + Path.GetFileName(file));
                            }
                            catch
                            {

                            }
                        }
                        //���� ���������� ���������� � ���� ����������, �� ������ �� ������
                        else if (Directory.Exists(sortingPathTextBox.Text + otherFolder) && File.Exists(sortingPathTextBox.Text + otherFolder + Path.GetFileName(file)))
                        {

                        }
                    }
                }

                MessageBox.Show("Sorting finished!", "Finished", MessageBoxButtons.OK);

            }

            else
            {
                MessageBox.Show("Insert sorting folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //����������� ������������ �����?
        private void CB_SortInside_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_SortInside.Checked)
            {
                all_folders = 1; //��
            }
            else
            {
                all_folders = 0; //���
            }
        }

        //������ About
        private void B_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Sorter v1.3.1\n\nAuthor: HOCKI1", "About");
        }

        //�������� �����
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = L_ChooseFolder;
            ToolTip t = new ToolTip();
            t.SetToolTip(CB_SortInside, "Sorting files inside folders inside directory");
            t.SetToolTip(L_Settings, "Open settings file");
        }

        //�������� ������� ���������
        private void L_Settings_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process txt = new System.Diagnostics.Process();
            txt.StartInfo.FileName = "notepad.exe";
            txt.StartInfo.Arguments = "./settings.ini";
            txt.Start();
        }
    }
}