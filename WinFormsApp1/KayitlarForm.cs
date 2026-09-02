using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class KayitlarForm : Form
    {
        private class CalisanOge
        {
            public int Id { get; set; }
            public string AdSoyad { get; set; }
            public override string ToString() => AdSoyad;
        }

        public KayitlarForm()
        {
            InitializeComponent();
            this.Size = new Size(760, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnAra;
            SutunlariOlustur();
            CalisanlariYukle();

            dgvKayitlar.BorderStyle = BorderStyle.None;

        }

        private void SutunlariOlustur()
        {
            dgvKayitlar.AutoGenerateColumns = false;
            dgvKayitlar.Columns.Clear();

            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Id", Visible = false });
            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAdSoyad", HeaderText = "Ad Soyad", Width = 150 });
            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colKonu", HeaderText = "Konu", Width = 150 });
            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBaslangicTarihi", HeaderText = "Başlangıç Tarihi", Width = 130 });
            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBitisTarihi", HeaderText = "Bitiş Tarihi", Width = 130 });
            dgvKayitlar.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSure", HeaderText = "Süre", Width = 70 });
            dgvKayitlar.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colManuel", HeaderText = "Manuel", Width = 60, ReadOnly = true });
        }

        private void CalisanlariYukle()
        {
            cmbCalisanlar.Items.Clear();

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = "SELECT Id, AdSoyad FROM Kullanicilar WHERE Rol = 'Calisan' ORDER BY AdSoyad";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    using (SqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            cmbCalisanlar.Items.Add(new CalisanOge
                            {
                                Id = (int)okuyucu["Id"],
                                AdSoyad = okuyucu["AdSoyad"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Çalışanlar yüklenirken hata oluştu: " + hata.Message);
            }
        }

        private void btnRaporAl_Click(object sender, EventArgs e)
        {
            if (!(cmbCalisanlar.SelectedItem is CalisanOge secilenCalisan))
            {
                MessageBox.Show("Lütfen önce listeden bir çalışan seçin.");
                return;
            }

            ContextMenuStrip menu = new ContextMenuStrip();

            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                        "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            int guncelYil = DateTime.Now.Year;
            int guncelAy = DateTime.Now.Month;

            for (int yil = guncelYil; yil >= guncelYil - 1; yil--)
            {
                ToolStripMenuItem yilOgesi = new ToolStripMenuItem(yil.ToString());

                for (int i = 0; i < aylar.Length; i++)
                {
                    int secilenAy = i + 1;

                    // Bulunduğumuz yılın, henüz gelmemiş aylarını menüye hiç eklemiyoruz
                    if (yil == guncelYil && secilenAy > guncelAy)
                    {
                        continue;
                    }

                    int secilenYil = yil;

                    ToolStripMenuItem ayOgesi = new ToolStripMenuItem(aylar[i]);
                    ayOgesi.Click += (s, args) => RaporYardimcisi.RaporOlustur(secilenCalisan.Id, secilenCalisan.AdSoyad, secilenYil, secilenAy);

                    yilOgesi.DropDownItems.Add(ayOgesi);
                }

                menu.Items.Add(yilOgesi);
            }

            menu.Show(btnRaporAl, new Point(0, btnRaporAl.Height));
        }

        private void KayitlariYukle(int kullaniciId)
        {
            dgvKayitlar.Rows.Clear();

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();

                    string sorgu = @"SELECT ck.Id, k.AdSoyad, ck.Konu, ck.BaslangicZamani, ck.BitisZamani, ck.SureDakika, ck.ManuelMi
                                      FROM CalismaKayitlari ck
                                      JOIN Kullanicilar k ON ck.KullaniciId = k.Id
                                      WHERE ck.KullaniciId = @KullaniciId
                                      ORDER BY ck.BaslangicZamani DESC";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);

                        using (SqlDataReader okuyucu = komut.ExecuteReader())
                        {
                            while (okuyucu.Read())
                            {
                                int id = (int)okuyucu["Id"];
                                string adSoyad = okuyucu["AdSoyad"].ToString();
                                string konu = okuyucu["Konu"].ToString();
                                DateTime baslangic = (DateTime)okuyucu["BaslangicZamani"];
                                bool manuelMi = (bool)okuyucu["ManuelMi"];

                                string baslangicGosterim = baslangic.ToString("dd.MM.yyyy HH:mm");

                                string bitisGosterim;
                                string sureGosterim;

                                if (okuyucu["BitisZamani"] == DBNull.Value)
                                {
                                    bitisGosterim = "Devam ediyor";
                                    sureGosterim = "-";
                                }
                                else
                                {
                                    DateTime bitis = (DateTime)okuyucu["BitisZamani"];
                                    bitisGosterim = bitis.ToString("dd.MM.yyyy HH:mm");

                                    int sureDakika = okuyucu["SureDakika"] == DBNull.Value ? 0 : (int)okuyucu["SureDakika"];
                                    sureGosterim = string.Format("{0:D2}:{1:D2}", sureDakika / 60, sureDakika % 60);
                                }

                                dgvKayitlar.Rows.Add(id, adSoyad, konu, baslangicGosterim, bitisGosterim, sureGosterim, manuelMi);
                            }
                        }
                    }

                    if (dgvKayitlar.Rows.Count == 0)
                    {
                        MessageBox.Show("Bu çalışana ait kayıt bulunamadı.");
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kayıtlar yüklenirken hata oluştu: " + hata.Message);
            }
        }

        // Seçili çalışanın mevcut hareketsizlik ayarını okuyup kutuya yazar
        private void HareketsizlikAyariniYukle(int kullaniciId)
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = "SELECT HareketsizlikLimitiDakika FROM HareketsizlikAyarlari WHERE KullaniciId = @KullaniciId";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        object sonuc = komut.ExecuteScalar();

                        nudHareketsizlikDakika.Value = (sonuc == null || sonuc == DBNull.Value) ? 2 : Convert.ToInt32(sonuc);
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ayar yüklenirken hata oluştu: " + hata.Message);
            }
        }

        // Seçili çalışanın email'ini veritabanından çekip etikete yazar
        private void CalisanEmailiniYukle(int kullaniciId)
        {
            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = "SELECT Email FROM Kullanicilar WHERE Id = @Id";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@Id", kullaniciId);
                        object sonuc = komut.ExecuteScalar();

                        if (sonuc == null || sonuc == DBNull.Value || string.IsNullOrWhiteSpace(sonuc.ToString()))
                        {
                            txtEkEmail.Clear();
                        }
                        else
                        {
                            txtEkEmail.Text = sonuc.ToString();
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Email bilgisi alınırken hata oluştu: " + hata.Message);
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (!(cmbCalisanlar.SelectedItem is CalisanOge secilenCalisan))
            {
                MessageBox.Show("Lütfen önce listeden bir çalışan seçin.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEkEmail.Text))
            {
                MessageBox.Show("Lütfen gönderilecek email adresini girin.");
                return;
            }

            string tumAlicilar = txtEkEmail.Text;

            ContextMenuStrip menu = new ContextMenuStrip();

            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                        "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            int guncelYil = DateTime.Now.Year;
            int guncelAy = DateTime.Now.Month;

            for (int yil = guncelYil; yil >= guncelYil - 1; yil--)
            {
                ToolStripMenuItem yilOgesi = new ToolStripMenuItem(yil.ToString());

                for (int i = 0; i < aylar.Length; i++)
                {
                    int secilenAy = i + 1;

                    if (yil == guncelYil && secilenAy > guncelAy)
                    {
                        continue;
                    }

                    int secilenYil = yil;

                    ToolStripMenuItem ayOgesi = new ToolStripMenuItem(aylar[i]);
                    ayOgesi.Click += (s, args) => RaporEmailleGonder(secilenCalisan.Id, secilenCalisan.AdSoyad, secilenYil, secilenAy, tumAlicilar);

                    yilOgesi.DropDownItems.Add(ayOgesi);
                }

                menu.Items.Add(yilOgesi);
            }

            menu.Show(btnGonder, new Point(0, btnGonder.Height));
        }

        private async void RaporEmailleGonder(int kullaniciId, string calisanAdi, int yil, int ay, string aliciEmailleri)
        {
            try
            {
                
                string[] aylarBaslik = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                                  "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
                string donemAdi = aylarBaslik[ay - 1] + " " + yil;

                string pdfYolu = await RaporYardimcisi.RaporPdfOlustur(kullaniciId, calisanAdi, yil, ay);

                string htmlIcerik =RaporYardimcisi.RaporDosyasiOlustur(kullaniciId, calisanAdi, yil, ay,false);


                string konu = calisanAdi + " - " + donemAdi + " Çalışma Raporu";
                string govdeMetni = "Merhaba,\n\n" + calisanAdi + " için " + donemAdi + " dönemine ait çalışma raporu ektedir.\n\nİyi çalışmalar.";

                govdeMetni += $"<br>{htmlIcerik}";

                EmailYardimcisi.RaporGonder(aliciEmailleri, konu, govdeMetni, pdfYolu);

                MessageBox.Show("Rapor başarıyla gönderildi: " + aliciEmailleri);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Email gönderilirken hata oluştu: " + hata.Message);
            }
        }

        private void btnBuCalisanaKaydet_Click(object sender, EventArgs e)
        {
            if (!(cmbCalisanlar.SelectedItem is CalisanOge secilenCalisan))
            {
                MessageBox.Show("Lütfen listeden bir çalışan seçin.");
                return;
            }

            HareketsizlikAyariniKaydet(secilenCalisan.Id, (int)nudHareketsizlikDakika.Value);
            MessageBox.Show(secilenCalisan.AdSoyad + " için hareketsizlik süresi " + nudHareketsizlikDakika.Value + " dakika olarak kaydedildi.");
        }

        private void btnTumuneUygula_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show(
    "Bu değer (" + nudHareketsizlikDakika.Value + " dakika) TÜM çalışanlara uygulanacak. Emin misiniz?",
    "Onay", MessageBoxButtons.YesNo);

            if (onay != DialogResult.Yes) return;

            try
            {
                using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
                {
                    baglanti.Open();
                    string sorgu = "SELECT Id FROM Kullanicilar WHERE Rol = 'Calisan'";
                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    using (SqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        var tumIdler = new System.Collections.Generic.List<int>();
                        while (okuyucu.Read())
                        {
                            tumIdler.Add((int)okuyucu["Id"]);
                        }

                        okuyucu.Close();

                        foreach (int id in tumIdler)
                        {
                            HareketsizlikAyariniKaydet(id, (int)nudHareketsizlikDakika.Value);
                        }
                    }
                }

                MessageBox.Show("Hareketsizlik süresi tüm çalışanlara uygulandı.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Toplu güncelleme sırasında hata oluştu: " + hata.Message);
            }
        }

        // Var olan ayarı günceller, yoksa yeni satır ekler (UPSERT mantığı)
        private void HareketsizlikAyariniKaydet(int kullaniciId, int dakika)
        {
            using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
            {
                baglanti.Open();

                string kontrolSorgu = "SELECT COUNT(*) FROM HareketsizlikAyarlari WHERE KullaniciId = @KullaniciId";
                using (SqlCommand kontrolKomutu = new SqlCommand(kontrolSorgu, baglanti))
                {
                    kontrolKomutu.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                    int sayi = (int)kontrolKomutu.ExecuteScalar();

                    string sorgu;
                    if (sayi > 0)
                    {
                        sorgu = "UPDATE HareketsizlikAyarlari SET HareketsizlikLimitiDakika = @Dakika WHERE KullaniciId = @KullaniciId";
                    }
                    else
                    {
                        sorgu = "INSERT INTO HareketsizlikAyarlari (KullaniciId, HareketsizlikLimitiDakika) VALUES (@KullaniciId, @Dakika)";
                    }

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        komut.Parameters.AddWithValue("@Dakika", dakika);
                        komut.ExecuteNonQuery();
                    }
                }
            }
        }

        private void dgvKayitlar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvKayitlar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (cmbCalisanlar.SelectedItem is CalisanOge secilenCalisan)
            {
                KayitlariYukle(secilenCalisan.Id);
                HareketsizlikAyariniYukle(secilenCalisan.Id);
                CalisanEmailiniYukle(secilenCalisan.Id);
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir çalışan seçin.");
            }
        }
    }
}