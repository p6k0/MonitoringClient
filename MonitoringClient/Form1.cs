using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonitoringClient
{
    public partial class Form1 : Form
    {
        #region панель лога
        private Panel LogPanel;
        private TextBox LogBox;
        #endregion
        public Form1()
        {
            Program.logger.Info("Отрисовка пользовательского интерфейса");
            InitializeComponent();
            //Задаем ширину во всю клиентскую область
            Bounds = Screen.PrimaryScreen.WorkingArea;

            using (GraphicsPath gp = new GraphicsPath())
            {
                int HalfH = LogButtton.Height / 2;
                Rectangle arc = new Rectangle(0, 0, HalfH, HalfH);
                gp.AddArc(arc, 180, 90);
                arc.X = LogButtton.Width - arc.Width - 1;
                gp.AddArc(arc, 270, 90);
                gp.AddLine(LogButtton.Width, LogButtton.Height, 0, LogButtton.Height);
                LogButtton.Region = new Region(gp);
            }
            TrayIcon_SetState();

            #region создаем панель лога
            LogPanel = new Panel()
            {
                Width = Width,
                Height = Height - 20,
                Left = 0,
                Top = 20,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Visible = false,
                BackColor = Color.FromArgb(0x60, 0x7d, 0x8b)
            };
            Controls.Add(LogPanel);
            LogBox = new TextBox()
            {
                Multiline = true,
                Width = LogPanel.Width - 10,
                Height = LogPanel.Height - 10,
                Left = 5,
                Top = 5,
                BorderStyle = BorderStyle.None,
                BackColor = LogPanel.BackColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204)
            };
            LogPanel.Controls.Add(LogBox);
            #endregion
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
            Close();
        }

        private void CloseButton_Paint(object sender, PaintEventArgs e)
        {
            int bWidth = ((Button)sender).Width;
            int bHeight = ((Button)sender).Height;
            using (var formButtonsPen = new Pen(Color.Black, 2))
            {

                e.Graphics.DrawLine(
                        formButtonsPen,
                        (int)(bWidth * 0.33),
                        (int)(bHeight * 0.33),
                        (int)(bWidth * 0.66),
                        (int)(bHeight * 0.66)
                   );
                e.Graphics.DrawLine(
                        formButtonsPen,
                        (int)(bWidth * 0.33),
                        (int)(bHeight * 0.66),
                        (int)(bWidth * 0.66),
                        (int)(bHeight * 0.33)
                   );
            }
        }
        #endregion
        #region кнопка свернуть
        private void MinimizeButton_Paint(object sender, PaintEventArgs e)
        {
            int bWidth = ((Button)sender).Width;
            int bHeight = ((Button)sender).Height;
            using (var formButtonsPen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(
                        formButtonsPen,
                        (int)(bWidth * 0.33),
                        (int)(bHeight * 0.66),
                        (int)(bWidth * 0.66),
                        (int)(bHeight * 0.66)
                   );
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            Visible = false;
            notifyIcon1.Visible = true;
            if (!TrayWizardShowed)
            {
                notifyIcon1.ShowBalloonTip(5, "Подсказка", "Мониторинг турникетного комплекса находится тут", ToolTipIcon.Info);
                TrayWizardShowed = true;
            }
        }
        #endregion

    }
}
