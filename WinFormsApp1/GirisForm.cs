using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class GirisForm : Form
    {
        private AnaForm anaFormReferansi = null;
        private int sonGirisYapanId = 0;
        private string sonGirisYapanAdSoyad = null;

        public GirisForm()
        {
            InitializeComponent();
            TasarimiUygula();
        }

        private void TasarimiUygula()
        {
            this.Size = new Size(420, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.BackColor = ColorTranslator.FromHtml("#F4F6F8");
            this.Font = new Font("Segoe UI", 9.5f);

            foreach (Control kontrol in this.Controls)
            {
                if (kontrol is Label etiket)
                {
                    etiket.ForeColor = ColorTranslator.FromHtml("#334155");
                }
                else if (kontrol is TextBox kutu)
                {
                    kutu.BorderStyle = BorderStyle.FixedSingle;
                    kutu.BackColor = Color.White;
                }
            }

            btnGirisYap.FlatStyle = FlatStyle.Flat;
            btnGirisYap.FlatAppearance.BorderSize = 0;
            btnGirisYap.BackColor = ColorTranslator.FromHtml("#0EA5A4");
            btnGirisYap.ForeColor = Color.White;
            btnGirisYap.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGirisKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtGirisSifre.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre girin.");
                return;
            }

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = "SELECT Id, SifreHash, AdSoyad, Rol FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciAdi", txtGirisKullaniciAdi.Text);

                        using (SqlDataReader okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                string kayitliHash = okuyucu["SifreHash"].ToString();
                                string adSoyad = okuyucu["AdSoyad"].ToString();
                                string rol = okuyucu["Rol"].ToString();

                                if (SifreYardimcisi.Dogrula(txtGirisSifre.Text, kayitliHash, txtGirisKullaniciAdi.Text))
                                {
                                    int girisYapanId = (int)okuyucu["Id"];

                                    if (rol == "Yonetici")
                                    {
                                        KayitOlForm kayitOlForm = new KayitOlForm();
                                        kayitOlForm.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        sonGirisYapanId = girisYapanId;
                                        sonGirisYapanAdSoyad = adSoyad;

                                        AnaForm anaForm = new AnaForm(girisYapanId, adSoyad);
                                        anaFormReferansi = anaForm;
                                        anaForm.FormClosed += (s, args) => { anaFormReferansi = null; };

                                        anaForm.Show();
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Şifre yanlış.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bu kullanıcı adı bulunamadı.");
                            }
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata oluştu: " + hata.Message);
            }
        }

        private void notifyIconAna_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();

                bool acikMi = anaFormReferansi != null && !anaFormReferansi.IsDisposed;
                bool aktifMi = acikMi && anaFormReferansi.CalisiyorMu;
                string durumYazisi = aktifMi ? "Aktif" : "Pasif";

                ToolStripMenuItem durumOgesi = new ToolStripMenuItem("Durum: " + durumYazisi);
                durumOgesi.Click += (s, args) => AnaFormuGosterYaDaAc();

                menu.Items.Add(durumOgesi);
                menu.Show(Cursor.Position);
            }
        }

        private void AnaFormuGosterYaDaAc()
        {
            if (anaFormReferansi != null && !anaFormReferansi.IsDisposed)
            {
                // Zaten açık, sadece öne getir
                anaFormReferansi.OneGetir();
            }
            else
            {
                // Kapanmış, yeniden aç
                if (sonGirisYapanId == 0)
                {
                    MessageBox.Show("Henüz bir çalışan girişi yapılmadı.");
                    return;
                }

                AnaForm yeniForm = new AnaForm(sonGirisYapanId, sonGirisYapanAdSoyad);
                anaFormReferansi = yeniForm;
                yeniForm.FormClosed += (s, args) => { anaFormReferansi = null; };
                yeniForm.Show();
            }
        }
    }
}