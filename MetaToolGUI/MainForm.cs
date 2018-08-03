using System;
using System.IO;
using System.Windows.Forms;
using AX9.MetaTool;
using AX9.MetaTool.Models;

namespace AX9.MetaToolGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private async void MB_NpdmToDesc_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oFD = new OpenFileDialog
            {
                Filter = "NPDM files|*.npdm",
                RestoreDirectory = true
            })
            {
                if (oFD.ShowDialog() == DialogResult.OK)
                {
                    FileInfo inputFile = new FileInfo(oFD.FileName);

                    if (inputFile.Extension == ".npdm")
                    {
                        DescModel desc = await DescModel.FromNpdm(oFD.FileName);

                        using (SaveFileDialog sFD = new SaveFileDialog
                        {
                            Filter = "DESC files|*.desc|All Files|*.*",
                            RestoreDirectory = true,
                            FileName = Path.ChangeExtension(inputFile.FullName, ".desc")
                        })
                        {
                            if (sFD.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllText(sFD.FileName, desc.XMLSerialize());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The file format is not yet supported");
                    }
                }
            }
        }

        private void MB_EditDesc_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oFD = new OpenFileDialog()
            {
                Filter = "DESC files|*.desc",
                RestoreDirectory = true
            })
            {
                if (oFD.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DescModel desc = DescModel.FromXml(oFD.FileName);

                        new DescEditor(desc, oFD.FileName).Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void MB_CreateDesc_Click(object sender, EventArgs e)
        {
            DescModel desc = new DescModel(true);

            new DescEditor(desc).Show();
            this.Hide();
        }
    }
}
