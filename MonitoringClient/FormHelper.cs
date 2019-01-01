using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MonitoringClient
{
    class FormHelper
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public enum SkinColors : int
        {
            Primary = 0xFAFAFA, //Основной цвет окна: GRAY_50
            Primary2 = 0xe0e0e0, //Второй основной цвет окна: GRAY_300
            PrimaryText = 0x212121, //Текст вторичной вкладки: GRAY_900
            Secondary = 0x607d8b, //Цвет вторичной вкладки: BLUEGRAY_500
            Secondary2 = 0x78909c, //Второй цвет вкладки: BLUEGRAY_400 
            SecondaryText = 0xECEFF1, //Текст вторичной вкладки: BLUEGRAY_50
            Danger = 0xEF5350, //Цвет опасности: RED_400
            Alert = 0xffa726, //Цвет предупреждения: ORANGE_400
            Info = 0x24a69a//Цвет информирования: Teal_400
        }

        public static void DrawCloseButton(Graphics g, Rectangle r, SkinColors color)
        {
            using (var formButtonsPen = GetPen(color, 2))
            {
                g.DrawLine(
                        formButtonsPen,
                        (int)(r.Width * 0.33),
                        (int)(r.Height * 0.33),
                        (int)(r.Width * 0.66),
                        (int)(r.Height * 0.66)
                   );
                g.DrawLine(
                        formButtonsPen,
                        (int)(r.Width * 0.33),
                        (int)(r.Height * 0.66),
                        (int)(r.Width * 0.66),
                        (int)(r.Height * 0.33)
                   );
            }
        }

        public static void DrawMinimizeButton(Graphics g, Rectangle r, SkinColors color)
        {

            using (var formButtonsPen = GetPen(color, 2))
                g.DrawLine(
                    formButtonsPen,
                    (int)(r.Width * 0.33),
                    (int)(r.Height * 0.66),
                    (int)(r.Width * 0.66),
                    (int)(r.Height * 0.66)
               );
        }

        public static Color GetColor(SkinColors color)
        {
            return Color.FromArgb(
                (((int)color) & 0xff0000) >> 16,
                (((int)color) & 0xff00) >> 8,
                 ((int)color) & 0xff);
        }
        public static Pen GetPen(SkinColors color, int Width)
        {
            return new Pen(GetColor(color), Width);

        }
    }
}
