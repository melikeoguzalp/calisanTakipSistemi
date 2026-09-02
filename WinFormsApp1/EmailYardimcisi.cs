using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WinFormsApp1
{
    internal static class EmailYardimcisi
    {
        // Gönderici hesap bilgileri
        private static readonly string GondericiEmail = "imgencoglu@gmail.com";
        private static readonly string UygulamaSifresi = "ryrc relh ovpl ljgq";

        public static void RaporGonder(string aliciEmailleri, string konu, string govdeMetni, string ekDosyaYolu)
        {
            var mesaj = new MimeMessage();
            mesaj.From.Add(new MailboxAddress("Çalışma Takip Sistemi", GondericiEmail));

            // Virgülle ayrılmış birden fazla alıcıyı ekle
            string[] adresler = aliciEmailleri.Split(',');
            foreach (string adres in adresler)
            {
                string temiz = adres.Trim();
                if (!string.IsNullOrWhiteSpace(temiz))
                {
                    mesaj.To.Add(MailboxAddress.Parse(temiz));
                }
            }

            mesaj.Subject = konu;

            var govde = new BodyBuilder();

            // DEĞİŞİKLİK BURADA: TextBody yerine HtmlBody kullanıyoruz.
            // Artık govdeMetni parametresine "<b>Merhaba</b>" gibi HTML etiketleri gönderebilirsiniz.
            govde.HtmlBody = govdeMetni;

            if (!string.IsNullOrWhiteSpace(ekDosyaYolu))
            {
                govde.Attachments.Add(ekDosyaYolu);
            }

            // HATA DÜZELTMESİ: 'new' kelimesi kaldırıldı. ToMessageBody() zaten yeni bir nesne döner.
            mesaj.Body = govde.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(GondericiEmail, UygulamaSifresi);
                client.Send(mesaj);
                client.Disconnect(true);
            }
        }
    }
}