using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SimpleLogger;

namespace MonitoringClient
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SimpleLog.LogLevel = SimpleLog.Severity.Info;
            SimpleLog.SetLogDir("Log", true);
            SimpleLog.Info("Запуск приложения");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            SimpleLog.Info("Завершение приложения");
        }
    }
}
