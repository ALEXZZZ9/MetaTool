namespace AX9.MetaToolGUI
{
    partial class MainForm
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
            this.MMS_Menu = new System.Windows.Forms.MenuStrip();
            this.MMS_Menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MMS_Menu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MB_NpdmToDesc = new System.Windows.Forms.Button();
            this.MMS_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMS_Menu
            // 
            this.MMS_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMS_Menu_File});
            this.MMS_Menu.Location = new System.Drawing.Point(0, 0);
            this.MMS_Menu.Name = "MMS_Menu";
            this.MMS_Menu.Size = new System.Drawing.Size(184, 24);
            this.MMS_Menu.TabIndex = 4;
            this.MMS_Menu.Text = "menuStrip1";
            this.MMS_Menu.Visible = false;
            // 
            // MMS_Menu_File
            // 
            this.MMS_Menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMS_Menu_File_Open});
            this.MMS_Menu_File.Name = "MMS_Menu_File";
            this.MMS_Menu_File.Size = new System.Drawing.Size(37, 20);
            this.MMS_Menu_File.Text = "File";
            // 
            // MMS_Menu_File_Open
            // 
            this.MMS_Menu_File_Open.Name = "MMS_Menu_File_Open";
            this.MMS_Menu_File_Open.Size = new System.Drawing.Size(174, 22);
            this.MMS_Menu_File_Open.Text = "Open NPDM/DESC";
            this.MMS_Menu_File_Open.Click += new System.EventHandler(this.MMS_Menu_File_Open_Click);
            // 
            // MB_NpdmToDesc
            // 
            this.MB_NpdmToDesc.Location = new System.Drawing.Point(42, 30);
            this.MB_NpdmToDesc.Name = "MB_NpdmToDesc";
            this.MB_NpdmToDesc.Size = new System.Drawing.Size(100, 50);
            this.MB_NpdmToDesc.TabIndex = 5;
            this.MB_NpdmToDesc.Text = ".NPDM -> .DESC";
            this.MB_NpdmToDesc.UseVisualStyleBackColor = true;
            this.MB_NpdmToDesc.Click += new System.EventHandler(this.MB_NpdmToDesc_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 111);
            this.Controls.Add(this.MB_NpdmToDesc);
            this.Controls.Add(this.MMS_Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.MMS_Menu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MetaTool By ALEXZZZ9";
            this.MMS_Menu.ResumeLayout(false);
            this.MMS_Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MMS_Menu;
        private System.Windows.Forms.ToolStripMenuItem MMS_Menu_File;
        private System.Windows.Forms.ToolStripMenuItem MMS_Menu_File_Open;
        private System.Windows.Forms.Button MB_NpdmToDesc;
    }
}

