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
            this.MB_NpdmToDesc = new System.Windows.Forms.Button();
            this.MB_EditDesc = new System.Windows.Forms.Button();
            this.MB_CreateDesc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MB_NpdmToDesc
            // 
            this.MB_NpdmToDesc.Location = new System.Drawing.Point(4, 6);
            this.MB_NpdmToDesc.Name = "MB_NpdmToDesc";
            this.MB_NpdmToDesc.Size = new System.Drawing.Size(260, 45);
            this.MB_NpdmToDesc.TabIndex = 5;
            this.MB_NpdmToDesc.Text = "Convert NPDM  To DESC";
            this.MB_NpdmToDesc.UseVisualStyleBackColor = true;
            this.MB_NpdmToDesc.Click += new System.EventHandler(this.MB_NpdmToDesc_Click);
            // 
            // MB_EditDesc
            // 
            this.MB_EditDesc.Location = new System.Drawing.Point(4, 58);
            this.MB_EditDesc.Name = "MB_EditDesc";
            this.MB_EditDesc.Size = new System.Drawing.Size(260, 45);
            this.MB_EditDesc.TabIndex = 6;
            this.MB_EditDesc.Text = "Edit DESC";
            this.MB_EditDesc.UseVisualStyleBackColor = true;
            this.MB_EditDesc.Click += new System.EventHandler(this.MB_EditDesc_Click);
            // 
            // MB_CreateDesc
            // 
            this.MB_CreateDesc.Location = new System.Drawing.Point(4, 110);
            this.MB_CreateDesc.Name = "MB_CreateDesc";
            this.MB_CreateDesc.Size = new System.Drawing.Size(260, 45);
            this.MB_CreateDesc.TabIndex = 7;
            this.MB_CreateDesc.Text = "Create DESC";
            this.MB_CreateDesc.UseVisualStyleBackColor = true;
            this.MB_CreateDesc.Click += new System.EventHandler(this.MB_CreateDesc_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 161);
            this.Controls.Add(this.MB_CreateDesc);
            this.Controls.Add(this.MB_EditDesc);
            this.Controls.Add(this.MB_NpdmToDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MetaTool By ALEXZZZ9";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button MB_NpdmToDesc;
        private System.Windows.Forms.Button MB_EditDesc;
        private System.Windows.Forms.Button MB_CreateDesc;
    }
}

