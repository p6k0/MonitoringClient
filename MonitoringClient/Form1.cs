using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonitoringClient
{
    public partial class Form1 : Form
    {
        private Panel LogPanel;
        private TextBox LogBox;
        public Form1()
        {
            InitializeComponent();
            //Задаем ширину во всю клиентскую область
            Bounds = Screen.PrimaryScreen.WorkingArea;

            using (GraphicsPath gp = new GraphicsPath())
            {
                int HalfH = LogButtton.Height / 3;
                gp.AddLine(0, HalfH, HalfH, 0);
                gp.AddLine(HalfH, 0, LogButtton.Width - HalfH, 0);
                gp.AddLine(LogButtton.Width - HalfH, 0, LogButtton.Width, HalfH);
                gp.AddLine(LogButtton.Width, HalfH, LogButtton.Width, LogButtton.Height);
                gp.AddLine(LogButtton.Width, LogButtton.Height, 0, LogButtton.Height);
                LogButtton.Region = new Region(gp);
            }


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

                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            LogPanel.Controls.Add(LogBox);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (LogPanel.Visible)
                LogButtton.BackColor = MonitorPanel.BackColor;
            else
                LogButtton.BackColor = LogPanel.BackColor;
            LogPanel.Visible = !LogPanel.Visible;
            MonitorPanel.Visible = !MonitorPanel.Visible;
        }
    }
}
