
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonitoringClient
{
    public partial class FormCaptcha : Form, IMessageFilter
    {

        public const int
            WM_NCLBUTTONDOWN = 0xA1,
            HT_CAPTION = 0x2,
            WM_LBUTTONDOWN = 0x0201;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        int
            TimeToClose,
            Timeout;

        public FormCaptcha(int Length = 3, byte TimeToClose = 10)
        {
            this.TimeToClose = TimeToClose * 1000;
            Timeout = this.TimeToClose;
            Application.AddMessageFilter(this);
            InitializeComponent();
            Region = Region.FromHrgn(FormHelper.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 3, 3));
            BackColor = FormHelper.GetColor(FormHelper.SkinColors.Primary);
            textBox1.BackColor = BackColor;

            SetBorderColor(FormHelper.SkinColors.Alert);
            TitleLabel.Text = Text;
            Random rnd = new Random();
            for (int i = 0; i < Length; i++)
                OriginalText.Text += chars[rnd.Next(chars.Length)];
            textBox1.Focus();
        }

        private void FormCaptcha_Paint(object sender, PaintEventArgs e)
        {
            using (var pn = new System.Drawing.Pen(TitleLabel.BackColor, 2))
                e.Graphics.DrawRectangle(
                    pn, 1, 1, Width - 2, Height - 2
                    );
        }

        private void CloseButton_Paint(object sender, PaintEventArgs e)
        {
            FormHelper.DrawCloseButton(
                e.Graphics, ((Button)sender).ClientRectangle, FormHelper.SkinColors.PrimaryText
                );
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Если длина введенная больше эталонной - ставим цвет ошибки
            if (textBox1.TextLength > OriginalText.Text.Length)
            {
                SetBorderColor(FormHelper.SkinColors.Danger);
                return;
            }
            //Если текст равен, то пропускаем
            else if (textBox1.Text == OriginalText.Text)
            {
                SetBorderColor(FormHelper.SkinColors.Info);
                Update();
                DialogResult = DialogResult.OK;
                System.Threading.Thread.Sleep(500);
                Close();
                return;
            }
            else
                for (int i = 0; i < textBox1.TextLength; i++)
                    if (textBox1.Text[i] != OriginalText.Text[i])
                    {
                        SetBorderColor(FormHelper.SkinColors.Danger);
                        return;
                    }
            SetBorderColor(FormHelper.SkinColors.Alert);
            return;
        }

        private void SetBorderColor(FormHelper.SkinColors color)
        {

            TitleLabel.BackColor = FormHelper.GetColor(color);
            CloseButton.BackColor = TitleLabel.BackColor;
            TimerLabel.BackColor = TitleLabel.BackColor;
            using (Graphics g = CreateGraphics()) FormCaptcha_Paint(this, new PaintEventArgs(g, ClientRectangle));
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN && TitleLabel == Control.FromHandle(m.HWnd))
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return true;
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeToClose -= 500;
            if (TimeToClose <= 0)
                Close();

            float proc = textBox1.Width * ((float)TimeToClose / Timeout);

            TimerLabel.Width = (int)proc;
            TimerLabel.Left = (Width - TimerLabel.Width) / 2;


        }
    }
}
