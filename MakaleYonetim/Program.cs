using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakaleYonetim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Data.ServerName = "DESKTOP-93G7NTJ";
            Data.UserID = "DESKTOP-93G7NTJ\\section-1";
            Data.Database = "MakaleDB";
            Data.WindowsAuthentication = true;
            Application.Run(new Form1());
        }
    }
}
