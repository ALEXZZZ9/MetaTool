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
            this.SuspendLayout();
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MetaTool By ALEXZZZ9";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button MB_NpdmToDesc;
    }
}

