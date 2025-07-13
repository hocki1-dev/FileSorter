namespace File_Sorter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            folderBrowserDialog1 = new FolderBrowserDialog();
            colorDialog1 = new ColorDialog();
            B_Sort = new Button();
            CB_SortInside = new CheckBox();
            B_About = new Label();
            sortingPathTextBox = new TextBox();
            L_Settings = new Label();
            L_ChooseFolder = new Label();
            SuspendLayout();
            // 
            // B_Sort
            // 
            B_Sort.BackColor = Color.FromArgb(231, 156, 73);
            B_Sort.BackgroundImageLayout = ImageLayout.Zoom;
            //B_Sort.FlatAppearance.BorderSize = 0;
            B_Sort.FlatStyle = FlatStyle.Flat;
            B_Sort.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            B_Sort.ForeColor = Color.Black;
            B_Sort.Location = new Point(94, 110);
            B_Sort.Name = "B_Sort";
            B_Sort.Size = new Size(78, 33);
            B_Sort.TabIndex = 5;
            B_Sort.Text = "Sort";
            B_Sort.UseVisualStyleBackColor = false;
            B_Sort.Click += B_Sort_Click;
            // 
            // CB_SortInside
            // 
            CB_SortInside.AccessibleDescription = "";
            CB_SortInside.AutoSize = true;
            CB_SortInside.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            CB_SortInside.ForeColor = Color.Black;
            CB_SortInside.Location = new Point(17, 70);
            CB_SortInside.Name = "CB_SortInside";
            CB_SortInside.Size = new Size(155, 24);
            CB_SortInside.TabIndex = 6;
            CB_SortInside.Tag = "";
            CB_SortInside.Text = "Sort folders inside?";
            CB_SortInside.UseVisualStyleBackColor = true;
            CB_SortInside.CheckedChanged += CB_SortInside_CheckedChanged;
            // 
            // B_About
            // 
            B_About.AutoSize = true;
            B_About.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            B_About.ForeColor = Color.FromArgb(58, 88, 177);
            B_About.Location = new Point(186, 159);
            B_About.Name = "B_About";
            B_About.Size = new Size(51, 20);
            B_About.TabIndex = 8;
            B_About.Text = "About";
            B_About.Click += B_About_Click;
            // 
            // sortingPathTextBox
            // 
            sortingPathTextBox.Location = new Point(15, 41);
            sortingPathTextBox.Name = "sortingPathTextBox";
            sortingPathTextBox.PlaceholderText = "Double click to open folder";
            sortingPathTextBox.Size = new Size(233, 23);
            sortingPathTextBox.TabIndex = 4;
            sortingPathTextBox.DoubleClick += textBox1_DoubleClick;
            // 
            // L_Settings
            // 
            L_Settings.BackColor = Color.Transparent;
            L_Settings.FlatStyle = FlatStyle.Popup;
            L_Settings.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            L_Settings.ForeColor = Color.Black;
            L_Settings.Image = (Image)resources.GetObject("L_Settings.Image");
            L_Settings.Location = new Point(12, 153);
            L_Settings.Name = "L_Settings";
            L_Settings.Padding = new Padding(0, 3, 0, 3);
            L_Settings.Size = new Size(30, 26);
            L_Settings.TabIndex = 9;
            L_Settings.Click += L_Settings_Click;
            // 
            // L_ChooseFolder
            // 
            L_ChooseFolder.AutoSize = true;
            L_ChooseFolder.Font = new Font("Segoe UI Emoji", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            L_ChooseFolder.ForeColor = Color.Black;
            L_ChooseFolder.Location = new Point(9, 10);
            L_ChooseFolder.Name = "L_ChooseFolder";
            L_ChooseFolder.Size = new Size(176, 28);
            L_ChooseFolder.TabIndex = 7;
            L_ChooseFolder.Text = "Folder for sorting:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 192);
            Controls.Add(L_ChooseFolder);
            Controls.Add(L_Settings);
            Controls.Add(CB_SortInside);
            Controls.Add(sortingPathTextBox);
            Controls.Add(B_Sort);
            Controls.Add(B_About);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "File Sorter";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private ColorDialog colorDialog1;
        private Button B_Sort;
        private CheckBox CB_SortInside;
        private Label B_About;
        private TextBox sortingPathTextBox;
        private Label L_Settings;
        private Label L_ChooseFolder;
    }
}