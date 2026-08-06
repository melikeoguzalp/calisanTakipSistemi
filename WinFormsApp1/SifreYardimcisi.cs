using System;
using System.Security.Cryptography;
using System.Text;

namespace WinFormsApp1
{
    internal class SifreYardimcisi
    {
        // Şifreyi hash'e çevirir (geri döndürülemez, tek yönlü)
        public static string Hashle(string sifre)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] veri = Encoding.UTF8.GetBytes(sifre);
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
        public static bool Dogrula(string girilenSifre, string kayitliHash)
        {
            string girilenHash = Hashle(girilenSifre);
            return girilenHash == kayitliHash;
        }
    }
}