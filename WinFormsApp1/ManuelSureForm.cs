using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ManuelSureForm : Form
    {
        public DateTime SeciliBaslangic { get; private set; }
        public DateTime SeciliBitis { get; private set; }
        public string SeciliKonu { get; private set; }
        public string SeciliIcerik { get; private set; }
        public string SeciliNeden { get; private set; }

        private int kullaniciId;

        public ManuelSureForm(int girisYapanId)
        {
            InitializeComponent();
            kullaniciId = girisYapanId;
            ToplamCalismaSuresiniGoster();
        }

        // Yarım kalan kaydı tamamlamak için özel mod
        public void TamamlamaModunaGecir(DateTime sabitBaslangic, string oncekiKonu, string oncekiIcerik)
        {
            dtpBaslangic.Value = sabitBaslangic;
            dtpBaslangic.Enabled = false;

            dtpBitis.Value = DateTime.Now;

            txtManuelKonu.Text = oncekiKonu;
            txtManuelIcerik.Text = oncekiIcerik;

            this.Text = "Yarım Kalan Oturumu Tamamla";
        }

        private void ToplamCalismaSuresiniGoster()
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = @"SELECT SUM(SureDakika) FROM CalismaKayitlari 
                                      WHERE KullaniciId = @KullaniciId";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);

                        object sonuc = komut.ExecuteScalar();
                        int toplamDakika = (sonuc == null || sonuc == DBNull.Value) ? 0 : Convert.ToInt32(sonuc);

                        int saat = toplamDakika / 60;
                        int dakika = toplamDakika % 60;
                        lblManuelToplamSure.Text = "Toplam çalışma süreniz: " + saat + " saat " + dakika + " dakika";
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Toplam süre alınırken hata oluştu: " + hata.Message);
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtManuelKonu.Text))
            {
                MessageBox.Show("Lütfen çalışma konusunu girin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtManuelIcerik.Text))
            {
                MessageBox.Show("Lütfen içerik alanını doldurun.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNeden.Text))
            {
                MessageBox.Show("Lütfen neden manuel girdiğinizi açıklayın.");
                return;
            }

            DateTime baslangic = dtpBaslangic.Value;
            DateTime bitis = dtpBitis.Value;

            if (bitis <= baslangic)
            {
                MessageBox.Show("Bitiş saati, başlangıç saatinden sonra olmalı.");
                return;
            }

            SeciliBaslangic = baslangic;
            SeciliBitis = bitis;
            SeciliKonu = txtManuelKonu.Text;
            SeciliIcerik = txtManuelIcerik.Text;
            SeciliNeden = txtNeden.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}