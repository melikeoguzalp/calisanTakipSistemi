using System;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class KayitOlForm : Form
    {
        public KayitOlForm()
        {
            InitializeComponent();
            TasarimiUygula();
        }

        private void TasarimiUygula()
        {

            this.Size = new Size(550, 500);
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



            // Ana eylem butonu: Çalışan Kayıt Et
            btnKayitOl.FlatStyle = FlatStyle.Flat;
            btnKayitOl.FlatAppearance.BorderSize = 0;
            btnKayitOl.BackColor = ColorTranslator.FromHtml("#0EA5A4");
            btnKayitOl.ForeColor = Color.White;
            btnKayitOl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

            // İkincil buton: Kayıtları Listele
            btnKayitlariListele.FlatStyle = FlatStyle.Flat;
            btnKayitlariListele.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#DCE1E6");
            btnKayitlariListele.BackColor = Color.White;
            btnKayitlariListele.ForeColor = ColorTranslator.FromHtml("#334155");

            // İkincil buton: Kullanıcı Bilgilerini Kopyala
            btnBilgileriKopyala.FlatStyle = FlatStyle.Flat;
            btnBilgileriKopyala.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#DCE1E6");
            btnBilgileriKopyala.BackColor = Color.White;
            btnBilgileriKopyala.ForeColor = ColorTranslator.FromHtml("#334155");
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSifreGoster.Checked)
            {
                txtSifre.PasswordChar = '\0';
                txtSifreTekrar.PasswordChar = '\0';
            }
            else
            {
                txtSifre.PasswordChar = '*';
                txtSifreTekrar.PasswordChar = '*';
            }
        }

        private void txtSaatlikUcret_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam, ondalık ayırıcı (,) ve Backspace tuşuna izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true; // yazılmasını engelle
            }

            // Zaten bir tane ondalık ayırıcı varsa, ikincisini engelle
            if (e.KeyChar == ',' && txtSaatlikUcret.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void btnBilgileriKopyala_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Kopyalamadan önce Kullanıcı Adı ve Şifre alanlarını doldurun.");
                return;
            }

            string metin = string.Format(
                "Kullanıcı Adı: {0}\nŞifre: {1}",
                txtKullaniciAdi.Text, txtSifre.Text);

            Clipboard.SetText(metin);

            MessageBox.Show("Bilgiler panoya kopyalandı.");
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            // 1) Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtAdSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text) ||
                string.IsNullOrWhiteSpace(txtSifreTekrar.Text) ||
                string.IsNullOrWhiteSpace(txtSaatlikUcret.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi girin.");
                return;
            }

            if (!decimal.TryParse(txtSaatlikUcret.Text, out decimal saatlikUcret))
            {
                MessageBox.Show("Saatlik ücret geçerli bir sayı olmalı.");
                return;
            }

            // 2) Şifreler eşleşiyor mu?
            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor.");
                return;
            }

            // 3) Şifreyi hash'le — salt olarak kullanıcı adını kullanıyoruz
            string sifreHash = SifreYardimcisi.Hashle(txtSifre.Text, txtKullaniciAdi.Text);

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string kontrolSorgu = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
                    using (SqlCommand kontrolKomutu = new SqlCommand(kontrolSorgu, baglanti))
                    {
                        kontrolKomutu.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text);
                        int sayi = (int)kontrolKomutu.ExecuteScalar();

                        if (sayi > 0)
                        {
                            MessageBox.Show("Bu kullanıcı adı zaten alınmış.");
                            return;
                        }
                    }

                    string ekleSorgu = @"INSERT INTO Kullanicilar (KullaniciAdi, SifreHash, AdSoyad, Rol, KayitTarihi, SaatlikUcret, Email)
                      VALUES (@KullaniciAdi, @SifreHash, @AdSoyad, @Rol, @KayitTarihi, @SaatlikUcret, @Email)";

                    using (SqlCommand ekleKomutu = new SqlCommand(ekleSorgu, baglanti))
                    {
                        ekleKomutu.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text);
                        ekleKomutu.Parameters.AddWithValue("@SifreHash", sifreHash);
                        ekleKomutu.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
                        ekleKomutu.Parameters.AddWithValue("@Rol", "Calisan"); // varsayılan rol
                        ekleKomutu.Parameters.AddWithValue("@KayitTarihi", DateTime.Now);
                        ekleKomutu.Parameters.AddWithValue("@SaatlikUcret", saatlikUcret);
                        ekleKomutu.Parameters.AddWithValue("@Email", txtEmail.Text);

                        ekleKomutu.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kayıt başarılı!");

                txtKullaniciAdi.Clear();
                txtAdSoyad.Clear();
                txtSifre.Clear();
                txtSifreTekrar.Clear();
                txtSaatlikUcret.Clear();
                txtEmail.Clear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata oluştu: " + hata.Message);
            }
        }

        private void btnKayitlariListele_Click(object sender, EventArgs e)
        {
            KayitlarForm kayitlarForm = new KayitlarForm();
            kayitlarForm.ShowDialog();
        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {

        }


    }
}