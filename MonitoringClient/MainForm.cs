
using System;
using System.Windows.Forms;

namespace MonitoringClient
{
    public partial class MainForm : Form
    {
        #region панель лога
        #endregion
        public MainForm(bool WithTaskbar = false)
        {
            InitializeComponent();
            //Задаем ширину во всю клиентскую область

            Bounds = WithTaskbar ? Screen.PrimaryScreen.WorkingArea : Screen.PrimaryScreen.Bounds;
            BackColor = FormHelper.GetColor(FormHelper.SkinColors.Primary);
            ForeColor = FormHelper.GetColor(FormHelper.SkinColors.PrimaryText);
            LogPanel.BackColor = FormHelper.GetColor(FormHelper.SkinColors.Secondary);
            LogPanel.Top = 20;
            MonitorPanel.Top = 20;
            richTextBox1.BackColor = FormHelper.GetColor(FormHelper.SkinColors.Secondary2);
            LogPanel.Height = Height - 20;
            MonitorPanel.Height = LogPanel.Height;
            TrayIcon_SetState();

        }
        #region трей
        /// <summary>
        /// Признак, показывающий была ли показана подсказка о том, что приложение сворачивается в трей
        /// </summary>
        bool TrayWizardShowed = false;

        private void TrayIcon_SetState(bool Problem = false)
        {
            notifyIcon1.Icon = Problem ? Properties.Resources.Tray_Problem : Properties.Resources.Tray_OK;
            notifyIcon1.Text = Problem ? "Имеются проблемы!" : "Все исправно";
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            notifyIcon1.Visible = false;
        }
        #endregion


        private void LogButton_Click(object sender, EventArgs e)
        {
            if (LogPanel.Visible)
                LogButtton.BackColor = MonitorPanel.BackColor;
            else
                LogButtton.BackColor = LogPanel.BackColor;
            LogPanel.Visible = !LogPanel.Visible;
            MonitorPanel.Visible = !MonitorPanel.Visible;
        }
        #region кнопка закрыть
        private void CloseButton_Click(object sender, EventArgs e)
        {
            using (FormCaptcha f =
                #if DEBUG
                new FormCaptcha(3, 10)
                #else
                new FormCaptcha(7, 10)
                #endif
                )
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Close();
                }
            }

        }

        private void CloseButton_Paint(object sender, PaintEventArgs e)
        {
            FormHelper.DrawCloseButton(
                e.Graphics, ((Button)sender).ClientRectangle, FormHelper.SkinColors.PrimaryText
                );
        }
        #endregion
        #region кнопка свернуть
        private void MinimizeButton_Paint(object sender, PaintEventArgs e)
        {
            FormHelper.DrawMinimizeButton(
                e.Graphics, ((Button)sender).ClientRectangle, FormHelper.SkinColors.PrimaryText
                );
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            Visible = false;
            notifyIcon1.Visible = true;
            if (!TrayWizardShowed)
            {
                notifyIcon1.ShowBalloonTip(5, "Подсказка", "Мониторинг турникетного комплекса сворачивается в область уведомлений 🡇", ToolTipIcon.Info);
                TrayWizardShowed = true;
            }
        }
        #endregion

    }
}
