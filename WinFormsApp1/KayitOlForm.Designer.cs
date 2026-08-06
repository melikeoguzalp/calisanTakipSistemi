namespace WinFormsApp1
{
    partial class KayitOlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnKayitOl = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtAdSoyad = new TextBox();
            txtKullaniciAdi = new TextBox();
            txtSifre = new TextBox();
            txtSifreTekrar = new TextBox();
            txtSaatlikUcret = new TextBox();
            label5 = new Label();
            btnKayitlariListele = new Button();
            chkSifreGoster = new CheckBox();
            btnBilgileriKopyala = new Button();
            SuspendLayout();
            // 
            // btnKayitOl
            // 
            btnKayitOl.Location = new Point(56, 353);
            btnKayitOl.Name = "btnKayitOl";
            btnKayitOl.Size = new Size(172, 72);
            btnKayitOl.TabIndex = 4;
            btnKayitOl.Text = "Çalışan Kayıt Et";
            btnKayitOl.UseVisualStyleBackColor = true;
            btnKayitOl.Click += btnKayitOl_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 179);
            label4.Name = "label4";
            label4.Size = new Size(86, 20);
            label4.TabIndex = 3;
            label4.Text = "Şifre Tekrar:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 130);
            label3.Name = "label3";
            label3.Size = new Size(42, 20);
            label3.TabIndex = 2;
            label3.Text = "Şifre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 31);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 1;
            label2.Text = "Ad Soyad:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 78);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 0;
            label1.Text = "Kullanıcı Adı:";
            // 
            // txtAdSoyad
            // 
            txtAdSoyad.Location = new Point(122, 24);
            txtAdSoyad.Name = "txtAdSoyad";
            txtAdSoyad.Size = new Size(125, 27);
            txtAdSoyad.TabIndex = 5;
            txtAdSoyad.TextChanged += txtAdSoyad_TextChanged;
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new Point(122, 71);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new Size(125, 27);
            txtKullaniciAdi.TabIndex = 6;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(122, 123);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(125, 27);
            txtSifre.TabIndex = 7;
            // 
            // txtSifreTekrar
            // 
            txtSifreTekrar.Location = new Point(122, 176);
            txtSifreTekrar.Name = "txtSifreTekrar";
            txtSifreTekrar.PasswordChar = '*';
            txtSifreTekrar.Size = new Size(125, 27);
            txtSifreTekrar.TabIndex = 8;
            // 
            // txtSaatlikUcret
            // 
            txtSaatlikUcret.Location = new Point(122, 252);
            txtSaatlikUcret.Name = "txtSaatlikUcret";
            txtSaatlikUcret.Size = new Size(125, 27);
            txtSaatlikUcret.TabIndex = 10;
            txtSaatlikUcret.KeyPress += txtSaatlikUcret_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 259);
            label5.Name = "label5";
            label5.Size = new Size(95, 20);
            label5.TabIndex = 9;
            label5.Text = "Saatlik Ücret:";
            // 
            // btnKayitlariListele
            // 
            btnKayitlariListele.Location = new Point(306, 13);
            btnKayitlariListele.Name = "btnKayitlariListele";
            btnKayitlariListele.Size = new Size(94, 57);
            btnKayitlariListele.TabIndex = 11;
            btnKayitlariListele.Text = "Kayıtları Listele";
            btnKayitlariListele.UseVisualStyleBackColor = true;
            btnKayitlariListele.Click += btnKayitlariListele_Click;
            // 
            // chkSifreGoster
            // 
            chkSifreGoster.AutoSize = true;
            chkSifreGoster.Font = new Font("Segoe UI", 6F);
            chkSifreGoster.Location = new Point(165, 219);
            chkSifreGoster.Name = "chkSifreGoster";
            chkSifreGoster.Size = new Size(82, 17);
            chkSifreGoster.TabIndex = 12;
            chkSifreGoster.Text = "Şifreyi göster";
            chkSifreGoster.UseVisualStyleBackColor = true;
            chkSifreGoster.Click += chkSifreGoster_CheckedChanged;
            // 
            // btnBilgileriKopyala
            // 
            btnBilgileriKopyala.Location = new Point(306, 95);
            btnBilgileriKopyala.Name = "btnBilgileriKopyala";
            btnBilgileriKopyala.Size = new Size(94, 82);
            btnBilgileriKopyala.TabIndex = 15;
            btnBilgileriKopyala.Text = "Kullanıcı Bilgilerini Kopyala";
            btnBilgileriKopyala.UseVisualStyleBackColor = true;
            btnBilgileriKopyala.Click += btnBilgileriKopyala_Click;
            // 
            // KayitOlForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBilgileriKopyala);
            Controls.Add(chkSifreGoster);
            Controls.Add(btnKayitlariListele);
            Controls.Add(txtSaatlikUcret);
            Controls.Add(label5);
            Controls.Add(txtSifreTekrar);
            Controls.Add(txtSifre);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(txtAdSoyad);
            Controls.Add(btnKayitOl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "KayitOlForm";
            Text = "Çalışan Kayıt Et";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnKayitOl;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtAdSoyad;
        private TextBox txtKullaniciAdi;
        private TextBox txtSifre;
        private TextBox txtSifreTekrar;
        private TextBox txtSaatlikUcret;
        private Label label5;
        private Button btnKayitlariListele;
        private CheckBox chkSifreGoster;
        private Button btnBilgileriKopyala;
    }
}