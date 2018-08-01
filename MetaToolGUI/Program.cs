using System;
using System.Windows.Forms;

namespace AX9.MetaToolGUI
{
    public static class Program
    {
        public static MainForm MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm();

            Application.Run(MainForm);
        }
    }
}
