using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProektModelirane
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //TODO изтрий като свърша с проверките
        public static void log(String str)
        {
            Console.WriteLine(str);
        }
    }
}
