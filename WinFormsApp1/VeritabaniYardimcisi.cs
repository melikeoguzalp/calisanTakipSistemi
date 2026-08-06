using System;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    internal class VeritabaniYardimcisi
    {
        // Veritabanına nasıl bağlanacağımızı anlatan bilgi (connection string)
        private static string baglantiCumlesi =
            @"Server=(localdb)\MSSQLLocalDB;Database=CalismaTakipDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // Her seferinde yeni bir bağlantı nesnesi almak için kullanacağımız metot
        public static SqlConnection BaglantiAl()
        {
            return new SqlConnection(baglantiCumlesi);
        }

        // Bağlantının çalışıp çalışmadığını test etmek için basit bir metot
        public static bool BaglantiyiTestEt()
        {
            try
            {
                using (SqlConnection baglanti = BaglantiAl())
                {
                    baglanti.Open(); // Veritabanına bağlanmayı dene
                    return true;     // Başarılıysa true dön
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bağlantı hatası: " + hata.Message);
                return false;
            }
        }
    }
}