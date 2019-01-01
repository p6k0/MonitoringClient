using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MonitoringClient
{
    static class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            logger.Info("Запуск приложения");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCaptcha());
            logger.Info("Завершение приложения");
        }
    }
}
