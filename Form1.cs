using System;
using System.IO;
using System.Linq;

namespace File_Sorter
{
    public partial class Form1 : Form
    {
        public string blacklist_formats = ".blend1"; //блэклист форматы
        public string system_formats = ".lnk .url .pif .scf .shs .shb .xnk .exe .ini"; //системные форматы
        public string newFolder = "";
        List<string> settings = new List<string>();
        StreamReader settingsFile = new StreamReader("settings.ini"); //считывание конфиг файла

        public class ConfigString
        {
            public string folder_name;
            public string file_extentions;

        }

        List<ConfigString> configStrings = new List<ConfigString>(); // строки файла конфигурации(ключ - значение)

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
                //выбор всех файлов в директориях
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

                //запись настроек в лист
                string temp = "";
                while (!settingsFile.EndOfStream)
                {
                    temp = settingsFile.ReadLine().ToString();
                    settings.Add(temp);
                }
                //////


                string[] strings = settings.ToArray();
                string temp2 = "";

                //раскидывание файлов по папкам
                foreach (string file in files)
                {
                    //получение формата файла
                    fileformat = Path.GetExtension(file).ToLower();

                    //проверка каждой строки в настройках
                    foreach (string setting in settings)
                    {
                        //расширения файлов
                        temp2 = setting.Substring(setting.IndexOf("=") + 1);
                        //названия папок
                        newFolder = setting.Substring(0, setting.IndexOf('=') - 1);
                        newFolder = "\\" + newFolder + "\\";

                        //проверка формата файла в расширениях файлов
                        if (temp2.Contains(fileformat))
                        {
                            //если директория существует, но файла не существует, то переместить 
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
                            //если директории не существует, то создать
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
                            //если директория существует и файл существует, то ничего не делать
                            else if (Directory.Exists(sortingPathTextBox.Text + newFolder) && File.Exists(sortingPathTextBox.Text + newFolder + Path.GetFileName(file)))
                            {

                            }
                        }
                    }
                }

                //сортировка блэклиста
                foreach (string file in files)
                {
                    //получение формата файла
                    fileformat = Path.GetExtension(file).ToLower();

                    //если файл существует
                    if (File.Exists(sortingPathTextBox.Text + "\\" + Path.GetFileName(file)))
                    {
                        //если файл в блэклисте
                        if (blacklist_formats.Contains(fileformat))
                        {
                            File.Delete(file);
                        }
                    }
                }

                //сортировка папки other
                foreach (string file in files)
                {
                    //получение формата файла
                    fileformat = Path.GetExtension(file).ToLower();

                    //если файл существует и не в системных, то закинуть в Other
                    if (File.Exists(sortingPathTextBox.Text + "\\" + Path.GetFileName(file)) && !system_formats.Contains(fileformat))
                    {
                        string otherFolder = "\\Other\\";
                        //если директория существует, но файла не существует, то переместить 
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
                        //если директории не существует, то создать
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
                        //если директория существует и файл существует, то ничего не делать
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




        //Сортировать внутренности папок?
        private void CB_SortInside_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_SortInside.Checked)
            {
                all_folders = 1; //да
            }
            else
            {
                all_folders = 0; //нет
            }
        }

        //кнопка About
        private void B_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Sorter v1.3.1\n\nAuthor: HOCKI1", "About");
        }

        //загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = L_ChooseFolder;
            ToolTip t = new ToolTip();
            t.SetToolTip(CB_SortInside, "Sorting files inside folders inside directory");
            t.SetToolTip(L_Settings, "Open settings file");
        }

        //открытие конфига программы
        private void L_Settings_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process txt = new System.Diagnostics.Process();
            txt.StartInfo.FileName = "notepad.exe";
            txt.StartInfo.Arguments = "./settings.ini";
            txt.Start();
        }
    }
}