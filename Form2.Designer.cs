namespace File_Sorter
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DG_Cfg = new DataGridView();
            folder_name = new DataGridViewTextBoxColumn();
            file_ext = new DataGridViewTextBoxColumn();
            B_CancelCfg = new Button();
            B_SaveCfg = new Button();
            ((System.ComponentModel.ISupportInitialize)DG_Cfg).BeginInit();
            SuspendLayout();
            // 
            // DG_Cfg
            // 
            DG_Cfg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DG_Cfg.BackgroundColor = SystemColors.Control;
            DG_Cfg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DG_Cfg.Columns.AddRange(new DataGridViewColumn[] { folder_name, file_ext });
            DG_Cfg.Location = new Point(12, 12);
            DG_Cfg.Margin = new Padding(3, 3, 3, 40);
            DG_Cfg.Name = "DG_Cfg";
            DG_Cfg.RowTemplate.Height = 25;
            DG_Cfg.Size = new Size(460, 300);
            DG_Cfg.TabIndex = 0;
            // 
            // folder_name
            // 
            folder_name.HeaderText = "Folder";
            folder_name.Name = "folder_name";
            folder_name.Resizable = DataGridViewTriState.True;
            // 
            // file_ext
            // 
            file_ext.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            file_ext.HeaderText = "File Extentions";
            file_ext.Name = "file_ext";
            // 
            // B_CancelCfg
            // 
            B_CancelCfg.Location = new Point(397, 326);
            B_CancelCfg.Name = "B_CancelCfg";
            B_CancelCfg.Size = new Size(75, 23);
            B_CancelCfg.TabIndex = 1;
            B_CancelCfg.Text = "Cancel";
            B_CancelCfg.UseVisualStyleBackColor = true;
            // 
            // B_SaveCfg
            // 
            B_SaveCfg.Location = new Point(316, 326);
            B_SaveCfg.Name = "B_SaveCfg";
            B_SaveCfg.Size = new Size(75, 23);
            B_SaveCfg.TabIndex = 2;
            B_SaveCfg.Text = "Save";
            B_SaveCfg.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 361);
            Controls.Add(B_SaveCfg);
            Controls.Add(B_CancelCfg);
            Controls.Add(DG_Cfg);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)DG_Cfg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DG_Cfg;
        private DataGridViewTextBoxColumn folder_name;
        private DataGridViewTextBoxColumn file_ext;
        private Button B_CancelCfg;
        private Button B_SaveCfg;
    }
}