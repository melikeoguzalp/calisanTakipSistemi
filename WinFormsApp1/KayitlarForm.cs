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
            this.Size = new Size(760, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnAra;
            SutunlariOlustur();
            CalisanlariYukle();
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

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (cmbCalisanlar.SelectedItem is CalisanOge secilenCalisan)
            {
                KayitlariYukle(secilenCalisan.Id);
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir çalışan seçin.");
            }
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

        private void dgvKayitlar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvKayitlar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
        }
    }
}