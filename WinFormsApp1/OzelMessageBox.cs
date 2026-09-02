using System;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsApp1
{
    internal static class OzelMessageBox
    {
        // Basit bilgi mesajı — MessageBox.Show(mesaj) yerine geçecek
        public static void Bilgi(string mesaj)
        {
            using (Form popup = TemelPopupOlustur(mesaj, out FlowLayoutPanel butonPaneli))
            {
                Button tamamButon = YeniButon("Tamam", true);
                tamamButon.Click += (s, e) => { popup.DialogResult = DialogResult.OK; popup.Close(); };
                butonPaneli.Controls.Add(tamamButon);

                popup.ShowDialog();
            }
        }

        // Evet/Hayır onay penceresi — MessageBoxButtons.YesNo yerine geçecek
        public static bool Onayla(string mesaj)
        {
            using (Form popup = TemelPopupOlustur(mesaj, out FlowLayoutPanel butonPaneli))
            {
                Button hayirButon = YeniButon("Hayır", false);
                Button evetButon = YeniButon("Evet", true);

                hayirButon.Click += (s, e) => { popup.DialogResult = DialogResult.No; popup.Close(); };
                evetButon.Click += (s, e) => { popup.DialogResult = DialogResult.Yes; popup.Close(); };

                butonPaneli.Controls.Add(hayirButon);
                butonPaneli.Controls.Add(evetButon);

                return popup.ShowDialog() == DialogResult.Yes;
            }
        }

        private static Form TemelPopupOlustur(string mesaj, out FlowLayoutPanel butonPaneli)
        {
            Form popup = new Form();
            popup.Size = new Size(380, 190);
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.FormBorderStyle = FormBorderStyle.FixedDialog;
            popup.MaximizeBox = false;
            popup.MinimizeBox = false;
            popup.TopMost = true;
            popup.BackColor = ColorTranslator.FromHtml("#F4F6F8");
            popup.Font = new Font("Segoe UI", 9.5f);
            popup.Text = "";

            Label mesajLabel = new Label();
            mesajLabel.Text = mesaj;
            mesajLabel.ForeColor = ColorTranslator.FromHtml("#334155");
            mesajLabel.Location = new Point(25, 25);
            mesajLabel.Size = new Size(330, 80);
            mesajLabel.AutoSize = false;

            butonPaneli = new FlowLayoutPanel();
            butonPaneli.FlowDirection = FlowDirection.RightToLeft;
            butonPaneli.Location = new Point(25, 115);
            butonPaneli.Size = new Size(330, 40);
            butonPaneli.WrapContents = false;

            popup.Controls.Add(mesajLabel);
            popup.Controls.Add(butonPaneli);

            return popup;
        }

        private static Button YeniButon(string yazi, bool vurgulu)
        {
            Button buton = new Button();
            buton.Text = yazi;
            buton.Size = new Size(90, 32);
            buton.Margin = new Padding(6, 0, 0, 0);
            buton.FlatStyle = FlatStyle.Flat;
            buton.FlatAppearance.BorderSize = vurgulu ? 0 : 1;

            if (vurgulu)
            {
                buton.BackColor = ColorTranslator.FromHtml("#0EA5A4");
                buton.ForeColor = Color.White;
                buton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
            else
            {
                buton.BackColor = Color.White;
                buton.ForeColor = ColorTranslator.FromHtml("#334155");
                buton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#DCE1E6");
            }

            return buton;
        }
    }
}