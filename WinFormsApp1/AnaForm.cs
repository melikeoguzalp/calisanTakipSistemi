using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsApp1
{
    public partial class AnaForm : Form
    {
        private int kullaniciId;
        private string adSoyad;

        private DateTime ilkBaslangicZamani;
        private DateTime segmentBaslangicZamani;
        private int toplamAktifSaniye = 0;
        private bool calisiyorMu = false;
        private bool oturumBasladiMi = false;

        private int? acikKayitId = null;

        public AnaForm(int girisYapanId, string girisYapanAdSoyad)
        {
            InitializeComponent();
            kullaniciId = girisYapanId;
            adSoyad = girisYapanAdSoyad;
            lblHosgeldin.Text = "Hoş geldin, " + adSoyad;

            sureTimer.Interval = 1000;

            btnBaslatDurdur.UseVisualStyleBackColor = false;
            btnBaslatDurdur.BackColor = Color.MediumSeaGreen;
            btnBaslatDurdur.ForeColor = Color.White;

            TasarimiUygula();
            ToplamCalismaSuresiniGoster();
            YarimKalanKaydiKontrolEt();
        }

        private void TasarimiUygula()
        {
            this.Size = new Size(450, 650);

            this.StartPosition = FormStartPosition.Manual;

            var calismaAlani = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(
                calismaAlani.Width - this.Width - 20,
                calismaAlani.Height - this.Height - 20);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

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
                else if (kontrol is Button buton && buton != btnBaslatDurdur)
                {
                    buton.FlatStyle = FlatStyle.Flat;
                    buton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#DCE1E6");
                    buton.BackColor = Color.White;
                    buton.ForeColor = ColorTranslator.FromHtml("#334155");
                }
            }

            lblHosgeldin.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblHosgeldin.ForeColor = ColorTranslator.FromHtml("#1E293B");

            lblToplamSure.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            lblToplamSure.ForeColor = ColorTranslator.FromHtml("#0F6E56");
        }

        // Kullanıcının TÜM konularda, TÜM zamanlardaki toplam çalışma süresi
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
                        lblToplamSure.Text = saat + " saat " + dakika + " dakika";
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Toplam süre alınırken hata oluştu: " + hata.Message);
            }
        }

        // Manuel girişte kümülatif hesap için: bu konudaki mevcut toplamı verir
        private int KonuToplamDakikaGetir(string konu)
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = @"SELECT SUM(SureDakika) FROM CalismaKayitlari 
                                      WHERE KullaniciId = @KullaniciId AND Konu = @Konu";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        komut.Parameters.AddWithValue("@Konu", konu);

                        object sonuc = komut.ExecuteScalar();
                        return (sonuc == null || sonuc == DBNull.Value) ? 0 : Convert.ToInt32(sonuc);
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Toplam süre alınırken hata oluştu: " + hata.Message);
                return 0;
            }
        }

        // ---- ÇÖKME GÜVENLİĞİ: Yarım kalan (Bitiş=NULL) kayıt var mı kontrol eder ----
        private void YarimKalanKaydiKontrolEt()
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = @"SELECT TOP 1 Id, Konu, Icerik, BaslangicZamani 
                                      FROM CalismaKayitlari 
                                      WHERE KullaniciId = @KullaniciId AND BitisZamani IS NULL
                                      ORDER BY BaslangicZamani DESC";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);

                        int yarimKalanId = -1;
                        string oncekiKonu = null;
                        string oncekiIcerik = null;
                        DateTime oncekiBaslangic = DateTime.MinValue;
                        bool bulundu = false;

                        using (SqlDataReader okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                yarimKalanId = (int)okuyucu["Id"];
                                oncekiKonu = okuyucu["Konu"].ToString();
                                oncekiIcerik = okuyucu["Icerik"].ToString();
                                oncekiBaslangic = (DateTime)okuyucu["BaslangicZamani"];
                                bulundu = true;
                            }
                        }

                        if (bulundu)
                        {
                            MessageBox.Show("Önceki oturumunuz düzgün kapatılmamış görünüyor. Lütfen bitiş bilgilerini tamamlayın.");

                            using (ManuelSureForm tamamlamaForm = new ManuelSureForm(kullaniciId))
                            {
                                tamamlamaForm.TamamlamaModunaGecir(oncekiBaslangic, oncekiKonu, oncekiIcerik);

                                if (tamamlamaForm.ShowDialog() == DialogResult.OK)
                                {
                                    YarimKalanKaydiTamamla(yarimKalanId, oncekiBaslangic, tamamlamaForm.SeciliBitis,
                                        tamamlamaForm.SeciliKonu, tamamlamaForm.SeciliIcerik, tamamlamaForm.SeciliNeden);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Yarım kalan kayıt kontrol edilirken hata oluştu: " + hata.Message);
            }
        }

        private void YarimKalanKaydiTamamla(int id, DateTime baslangic, DateTime bitis, string konu, string icerik, string neden)
        {
            try
            {
                int sureDakika = (int)(bitis - baslangic).TotalMinutes;

                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = @"UPDATE CalismaKayitlari
                        SET BitisZamani = @BitisZamani, SureDakika = @SureDakika,
                            Konu = @Konu, Icerik = @Icerik, ManuelMi = 1, ManuelNeden = @ManuelNeden
                        WHERE Id = @Id";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@BitisZamani", bitis);
                        komut.Parameters.AddWithValue("@SureDakika", sureDakika);
                        komut.Parameters.AddWithValue("@Konu", konu);
                        komut.Parameters.AddWithValue("@Icerik", icerik);
                        komut.Parameters.AddWithValue("@ManuelNeden", neden);
                        komut.Parameters.AddWithValue("@Id", id);

                        komut.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Yarım kalan oturum başarıyla tamamlandı.");
                ToplamCalismaSuresiniGoster();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayıt tamamlanırken hata oluştu: " + hata.Message);
            }
        }

        // ---- ÇÖKME GÜVENLİĞİ: Başlat anında açık kayıt oluştur/güncelle ----
        private void AcikKayitBaslat()
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    if (acikKayitId == null)
                    {
                        string sorgu = @"INSERT INTO CalismaKayitlari 
                            (KullaniciId, Konu, Icerik, BaslangicZamani, BitisZamani, SureDakika, ManuelMi)
                            VALUES (@KullaniciId, @Konu, @Icerik, @BaslangicZamani, NULL, NULL, 0);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                            komut.Parameters.AddWithValue("@Konu", txtKonu.Text);
                            komut.Parameters.AddWithValue("@Icerik", txtIcerik.Text);
                            komut.Parameters.AddWithValue("@BaslangicZamani", segmentBaslangicZamani);

                            acikKayitId = (int)komut.ExecuteScalar();
                        }
                    }
                    else
                    {
                        string sorgu = @"UPDATE CalismaKayitlari
                            SET BaslangicZamani = @BaslangicZamani, BitisZamani = NULL, SureDakika = NULL,
                                Konu = @Konu, Icerik = @Icerik
                            WHERE Id = @Id";

                        using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@BaslangicZamani", segmentBaslangicZamani);
                            komut.Parameters.AddWithValue("@Konu", txtKonu.Text);
                            komut.Parameters.AddWithValue("@Icerik", txtIcerik.Text);
                            komut.Parameters.AddWithValue("@Id", acikKayitId.Value);

                            komut.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Açık kayıt oluşturulurken hata oluştu: " + hata.Message);
            }
        }

        private void AcikKayitKapat()
        {
            if (acikKayitId == null) return;

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = @"UPDATE CalismaKayitlari
                        SET BitisZamani = @BitisZamani, SureDakika = @SureDakika
                        WHERE Id = @Id";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@BitisZamani", DateTime.Now);
                        komut.Parameters.AddWithValue("@SureDakika", (int)(DateTime.Now - segmentBaslangicZamani).TotalMinutes);
                        komut.Parameters.AddWithValue("@Id", acikKayitId.Value);

                        komut.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Açık kayıt kapatılırken hata oluştu: " + hata.Message);
            }
        }

        private void btnBaslatDurdur_Click(object sender, EventArgs e)
        {
            if (!calisiyorMu)
            {
                if (!oturumBasladiMi)
                {
                    if (string.IsNullOrWhiteSpace(txtKonu.Text))
                    {
                        MessageBox.Show("Lütfen çalışma konusunu girin.");
                        return;
                    }

                    toplamAktifSaniye = 0;
                    ilkBaslangicZamani = DateTime.Now;
                    lblBaslangicSaati.Text = ilkBaslangicZamani.ToString("HH:mm");
                    lblGecenSure.Text = "0 dakika";
                    oturumBasladiMi = true;
                    txtKonu.Enabled = false;
                }

                segmentBaslangicZamani = DateTime.Now;

                AcikKayitBaslat();

                sureTimer.Start();
                calisiyorMu = true;
                btnBaslatDurdur.Text = "Durdur";
                btnBaslatDurdur.BackColor = Color.IndianRed;
            }
            else
            {
                sureTimer.Stop();
                calisiyorMu = false;
                btnBaslatDurdur.Text = "Başlat";
                btnBaslatDurdur.BackColor = Color.MediumSeaGreen;

                TimeSpan buSegment = DateTime.Now - segmentBaslangicZamani;
                toplamAktifSaniye += (int)buSegment.TotalSeconds;

                lblGecenSure.Text = (toplamAktifSaniye / 60) + " dakika";

                AcikKayitKapat();

                KaydiVeritabaninaKaydet(DateTime.Now, toplamAktifSaniye / 60, txtKonu.Text, txtIcerik.Text, false);

                ToplamCalismaSuresiniGoster();

                MessageBox.Show("Kayıt kaydedildi.");
            }
        }

        private void sureTimer_Tick(object sender, EventArgs e)
        {
            int canliSaniye = toplamAktifSaniye + (int)(DateTime.Now - segmentBaslangicZamani).TotalSeconds;
            lblGecenSure.Text = (canliSaniye / 60) + " dakika";
        }

        private void KaydiVeritabaninaKaydet(DateTime bitisZamani, int sureDakika, string konu, string icerik, bool manuelMi, string manuelNeden = null)
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = @"INSERT INTO CalismaKayitlari 
                (KullaniciId, Konu, Icerik, BaslangicZamani, BitisZamani, SureDakika, ManuelMi, ManuelNeden)
                VALUES (@KullaniciId, @Konu, @Icerik, @BaslangicZamani, @BitisZamani, @SureDakika, @ManuelMi, @ManuelNeden)";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        komut.Parameters.AddWithValue("@Konu", konu);
                        komut.Parameters.AddWithValue("@Icerik", icerik);
                        komut.Parameters.AddWithValue("@BaslangicZamani", ilkBaslangicZamani);
                        komut.Parameters.AddWithValue("@BitisZamani", bitisZamani);
                        komut.Parameters.AddWithValue("@SureDakika", sureDakika);
                        komut.Parameters.AddWithValue("@ManuelMi", manuelMi);
                        komut.Parameters.AddWithValue("@ManuelNeden", (object)manuelNeden ?? DBNull.Value);

                        komut.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + hata.Message);
            }
        }

        private void btnManuelSure_Click(object sender, EventArgs e)
        {
            if (calisiyorMu)
            {
                MessageBox.Show("Süre zaten çalışıyor, önce durdurun.");
                return;
            }

            using (ManuelSureForm manuelForm = new ManuelSureForm(kullaniciId))
            {
                if (manuelForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime baslangic = manuelForm.SeciliBaslangic;
                    DateTime bitis = manuelForm.SeciliBitis;
                    int yeniSureDakika = (int)(bitis - baslangic).TotalMinutes;

                    int oncekiToplamDakika = KonuToplamDakikaGetir(manuelForm.SeciliKonu);
                    int kumulatifToplam = oncekiToplamDakika + yeniSureDakika;

                    ilkBaslangicZamani = baslangic;

                    KaydiVeritabaninaKaydet(bitis, kumulatifToplam, manuelForm.SeciliKonu, manuelForm.SeciliIcerik, true, manuelForm.SeciliNeden);

                    ToplamCalismaSuresiniGoster();

                    MessageBox.Show("Kayıt kaydedildi.");
                }
            }
        }
    }
}