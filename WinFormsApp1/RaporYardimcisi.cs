using System;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.WinForms;

namespace WinFormsApp1
{
    public static class RaporYardimcisi
    {
        // Raporu oluşturur VE tarayıcıda açar (AnaForm, KayitlarForm'daki "Rapor Al" için)
        public static void RaporOlustur(int kullaniciId, string calisanAdi, int secilenYil, int secilenAy)
        {
            try
            {
                string dosyaYolu = RaporDosyasiOlustur(kullaniciId, calisanAdi, secilenYil, secilenAy);
                Process.Start(new ProcessStartInfo(dosyaYolu) { UseShellExecute = true });
            }
            catch (Exception hata)
            {
                MessageBox.Show("Rapor oluşturulurken hata oluştu: " + hata.Message);
            }
        }

        // Raporu HTML olarak oluşturur, tarayıcıda AÇMAZ, sadece dosya yolunu döndürür
        public static string RaporDosyasiOlustur(int kullaniciId, string calisanAdi, int secilenYil, int secilenAy, bool isReturnFolderPath = true)
        {
            var gunlukVeri = new System.Collections.Generic.List<(string TarihGosterim, int ToplamDakika)>();
            var detayListesi = new System.Collections.Generic.List<(string Tarih, string Konu, string Icerik, int Sure, bool ManuelMi, string ManuelNeden)>();
            int toplamDakika = 0;

            using (SqlConnection baglanti = VeritabaniYardimcisi.BaglantiAl())
            {
                baglanti.Open();

                string gunlukSorgu = @"
                    SELECT DAY(BaslangicZamani) AS Gun, SUM(SureDakika) AS ToplamDakika
                    FROM CalismaKayitlari
                    WHERE KullaniciId = @KullaniciId AND BitisZamani IS NOT NULL
                      AND YEAR(BaslangicZamani) = @Yil AND MONTH(BaslangicZamani) = @Ay
                    GROUP BY DAY(BaslangicZamani)";

                var gunlukSozluk = new System.Collections.Generic.Dictionary<int, int>();

                using (SqlCommand komut = new SqlCommand(gunlukSorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                    komut.Parameters.AddWithValue("@Yil", secilenYil);
                    komut.Parameters.AddWithValue("@Ay", secilenAy);

                    using (SqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            int gun = (int)okuyucu["Gun"];
                            int dakika = okuyucu["ToplamDakika"] == DBNull.Value ? 0 : Convert.ToInt32(okuyucu["ToplamDakika"]);
                            gunlukSozluk[gun] = dakika;
                            toplamDakika += dakika;
                        }
                    }
                }

                int ayinGunSayisi = DateTime.DaysInMonth(secilenYil, secilenAy);
                for (int gun = 1; gun <= ayinGunSayisi; gun++)
                {
                    int dakika = gunlukSozluk.ContainsKey(gun) ? gunlukSozluk[gun] : 0;
                    gunlukVeri.Add((gun.ToString("00") + "." + secilenAy.ToString("00"), dakika));
                }

                string detaySorgu = @"
                    SELECT BaslangicZamani, Konu, Icerik, SureDakika, ManuelMi, ManuelNeden
                    FROM CalismaKayitlari
                    WHERE KullaniciId = @KullaniciId AND BitisZamani IS NOT NULL
                      AND YEAR(BaslangicZamani) = @Yil AND MONTH(BaslangicZamani) = @Ay
                    ORDER BY BaslangicZamani DESC";

                using (SqlCommand komut = new SqlCommand(detaySorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                    komut.Parameters.AddWithValue("@Yil", secilenYil);
                    komut.Parameters.AddWithValue("@Ay", secilenAy);

                    using (SqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            DateTime tarih = (DateTime)okuyucu["BaslangicZamani"];
                            string konu = okuyucu["Konu"].ToString();
                            string icerik = okuyucu["Icerik"].ToString();
                            int sure = (int)okuyucu["SureDakika"];
                            bool manuelMi = (bool)okuyucu["ManuelMi"];
                            string manuelNeden = okuyucu["ManuelNeden"] == DBNull.Value ? "" : okuyucu["ManuelNeden"].ToString();

                            detayListesi.Add((tarih.ToString("dd.MM.yyyy HH:mm"), konu, icerik, sure, manuelMi, manuelNeden));
                        }
                    }
                }
            }

            string[] aylarBaslik = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                                      "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            string donemAdi = aylarBaslik[secilenAy - 1] + " " + secilenYil;

            string htmlIcerik = RaporHtmlOlustur(gunlukVeri, detayListesi, toplamDakika, calisanAdi, donemAdi);

            string dosyaYolu = Path.Combine(Path.GetTempPath(), "calisma_raporu_" + kullaniciId + ".html");
            File.WriteAllText(dosyaYolu, htmlIcerik, Encoding.UTF8);

            if (isReturnFolderPath)
                return dosyaYolu;
            else
                return htmlIcerik.ToString();
        }

        // YENİ: Raporu önce HTML olarak oluşturur, sonra gizli bir tarayıcı ile gerçek PDF'e çevirir
        public static async Task<string> RaporPdfOlustur(int kullaniciId, string calisanAdi, int secilenYil, int secilenAy)
        {
            string htmlYolu = RaporDosyasiOlustur(kullaniciId, calisanAdi, secilenYil, secilenAy);
            string pdfYolu = Path.Combine(Path.GetTempPath(), "calisma_raporu_" + kullaniciId + ".pdf");

            using (Form gizliForm = new Form())
            {
                gizliForm.StartPosition = FormStartPosition.Manual;
                gizliForm.Location = new Point(-3000, -3000); // ekranın çok dışına, görünmez şekilde
                gizliForm.Size = new Size(1000, 800);
                gizliForm.ShowInTaskbar = false;
                gizliForm.FormBorderStyle = FormBorderStyle.None;

                using (var webView = new WebView2())
                {
                    webView.Dock = DockStyle.Fill;
                    gizliForm.Controls.Add(webView);
                    gizliForm.Show();

                    await webView.EnsureCoreWebView2Async();

                    var navigasyonBitti = new TaskCompletionSource<bool>();
                    webView.CoreWebView2.NavigationCompleted += (s, e) => navigasyonBitti.TrySetResult(e.IsSuccess);

                    webView.CoreWebView2.Navigate(new Uri(htmlYolu).AbsoluteUri);
                    await navigasyonBitti.Task;

                    // Grafiğin (Chart.js) tamamen çizilmesi için kısa bir bekleme
                    await Task.Delay(1500);

                    await webView.CoreWebView2.PrintToPdfAsync(pdfYolu);
                }

                gizliForm.Close();
            }

            return pdfYolu;
        }

        public static string RaporHtmlOlustur(
            System.Collections.Generic.List<(string TarihGosterim, int ToplamDakika)> gunlukVeri,
            System.Collections.Generic.List<(string Tarih, string Konu, string Icerik, int Sure, bool ManuelMi, string ManuelNeden)> detayListesi,
            int toplamDakika, string calisanAdi, string donemAdi)
        {
            int saat = toplamDakika / 60;
            int dakika = toplamDakika % 60;

            string etiketler = string.Join(",", gunlukVeri.ConvertAll(g => "\"" + g.TarihGosterim + "\""));
            string veriler = string.Join(",", gunlukVeri.ConvertAll(g => g.ToplamDakika.ToString()));

            StringBuilder satirlar = new StringBuilder();
            foreach (var kayit in detayListesi)
            {
                string tur = kayit.ManuelMi ? "Manuel" : "Otomatik";
                string konuTemiz = System.Net.WebUtility.HtmlEncode(kayit.Konu);
                string icerikTemiz = System.Net.WebUtility.HtmlEncode(kayit.Icerik);

                string aciklamaGosterim;
                if (kayit.ManuelMi && !string.IsNullOrWhiteSpace(kayit.ManuelNeden))
                {
                    string nedenTemiz = System.Net.WebUtility.HtmlEncode(kayit.ManuelNeden);
                    aciklamaGosterim = "Manuel Nedeni: " + nedenTemiz + " - " + icerikTemiz;
                }
                else
                {
                    aciklamaGosterim = icerikTemiz;
                }

                satirlar.Append("<tr><td>" + kayit.Tarih + "</td><td>" + konuTemiz + "</td><td>" + aciklamaGosterim + "</td><td>" + kayit.Sure + "</td><td>" + tur + "</td></tr>");
            }

            string html = @"
<!DOCTYPE html>
<html lang='tr'>
<head>
<meta charset='UTF-8'>
<title>Çalışma Raporu</title>
<script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
<style>
body { font-family: Segoe UI, sans-serif; background: #F4F6F8; padding: 30px; color: #334155; }
h1 { color: #1E293B; margin: 0; }
.ust-satir { display: flex; justify-content: space-between; align-items: center; }
.donem { font-size: 15px; color: #64748B; margin-bottom: 10px; }
.toplam { font-size: 20px; font-weight: bold; color: #0F6E56; margin: 20px 0; }
table { width: 100%; border-collapse: collapse; background: white; margin-top: 20px; }
th, td { padding: 10px; border-bottom: 1px solid #DCE1E6; text-align: left; }
th { background: #334155; color: white; }
.chart-container { background: white; padding: 20px; border-radius: 8px; }
</style>
</head>
<body>
<div class='ust-satir'>
<h1>" + calisanAdi + @" - Çalışma Raporu</h1>
</div>

<div id='raporIcerik'>
<div class='donem'>Dönem: " + donemAdi + @"</div>
<div class='chart-container'>
<canvas id='raporGrafik' height='100'></canvas>
</div>
<div class='toplam'>Toplam Süre: " + saat + @" saat " + dakika + @" dakika</div>
<table>
<tr><th>Tarih</th><th>Konu</th><th>Açıklama</th><th>Süre (dk)</th><th>Tür</th></tr>
" + satirlar.ToString() + @"
</table>
</div>

<script>
new Chart(document.getElementById('raporGrafik'), {
    type: 'bar',
    data: {
        labels: [" + etiketler + @"],
        datasets: [
            { label: 'Toplam', data: [" + veriler + @"], backgroundColor: '#DC2626' }
        ]
    },
    options: {
        responsive: true,
        plugins: {
            legend: { display: false },
            tooltip: {
                enabled: true,
                callbacks: {
                    label: function(context) {
                        var toplamDakika = context.parsed.y;
                        var saat = Math.floor(toplamDakika / 60);
                        var dakika = toplamDakika % 60;
                        return saat + ':' + (dakika < 10 ? '0' + dakika : dakika);
                    }
                }
            }
        },
        scales: {
            y: {
                title: { display: true, text: 'Saat' },
                beginAtZero: true,
                ticks: {
                    stepSize: 120,
                    callback: function(deger) {
                        return (deger / 60) + ' sa';
                    }
                }
            }
        }
    }
});
</script>
</body>
</html>";

            return html;
        }
    }
}