namespace WinFormsApp1
{
    partial class GirisForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisForm));
            label1 = new Label();
            txtGirisKullaniciAdi = new TextBox();
            label2 = new Label();
            txtGirisSifre = new TextBox();
            btnGirisYap = new Button();
            pbLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(58, 196);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 0;
            label1.Text = "Kullanıcı Adı:";
            // 
            // txtGirisKullaniciAdi
            // 
            txtGirisKullaniciAdi.Location = new Point(186, 193);
            txtGirisKullaniciAdi.Name = "txtGirisKullaniciAdi";
            txtGirisKullaniciAdi.Size = new Size(125, 27);
            txtGirisKullaniciAdi.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(58, 258);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 2;
            label2.Text = "Şifre:";
            // 
            // txtGirisSifre
            // 
            txtGirisSifre.Location = new Point(186, 258);
            txtGirisSifre.Name = "txtGirisSifre";
            txtGirisSifre.PasswordChar = '*';
            txtGirisSifre.Size = new Size(125, 27);
            txtGirisSifre.TabIndex = 3;
            // 
            // btnGirisYap
            // 
            btnGirisYap.Location = new Point(137, 342);
            btnGirisYap.Name = "btnGirisYap";
            btnGirisYap.Size = new Size(138, 29);
            btnGirisYap.TabIndex = 4;
            btnGirisYap.Text = "Giriş Yap";
            btnGirisYap.UseVisualStyleBackColor = true;
            btnGirisYap.Click += btnGirisYap_Click;
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(69, 49);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(258, 95);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 5;
            pbLogo.TabStop = false;
            // 
            // GirisForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 450);
            Controls.Add(pbLogo);
            Controls.Add(btnGirisYap);
            Controls.Add(txtGirisSifre);
            Controls.Add(label2);
            Controls.Add(txtGirisKullaniciAdi);
            Controls.Add(label1);
            Name = "GirisForm";
            Text = "GirisForm";
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtGirisKullaniciAdi;
        private Label label2;
        private TextBox txtGirisSifre;
        private Button btnGirisYap;
        private PictureBox pbLogo;
    }
}