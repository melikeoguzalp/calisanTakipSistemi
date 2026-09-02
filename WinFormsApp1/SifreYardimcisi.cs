using System;
using System.Security.Cryptography;
using System.Text;

namespace WinFormsApp1
{
    internal class SifreYardimcisi
    {
        // Şifreyi hash'e çevirir (geri döndürülemez, tek yönlü)
        // 'salt' olarak kullanıcı adını gönderiyoruz — böylece aynı şifreyi seçen
        // iki farklı kullanıcı, farklı kullanıcı adlarına sahip oldukları için farklı hash üretir.
        public static string Hashle(string sifre, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string yeni = sifre + salt;
                byte[] veri = Encoding.UTF8.GetBytes(yeni);
                byte[] hashVeri = sha256.ComputeHash(veri);

                StringBuilder sonuc = new StringBuilder();
                foreach (byte b in hashVeri)
                {
                    sonuc.Append(b.ToString("x2"));
                }
                return sonuc.ToString();
            }
        }

        // Girilen şifre ile veritabanındaki hash'i karşılaştırır
        public static bool Dogrula(string girilenSifre, string kayitliHash, string kullaniciAdi)
        {
            string girilenHash = Hashle(girilenSifre, kullaniciAdi);
            return girilenHash == kayitliHash;
        }
    }
}