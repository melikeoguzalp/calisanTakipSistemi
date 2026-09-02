namespace WinFormsApp1
{
    partial class KayitlarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KayitlarForm));
            dgvKayitlar = new DataGridView();
            label1 = new Label();
            btnAra = new Button();
            cmbCalisanlar = new ComboBox();
            label2 = new Label();
            nudHareketsizlikDakika = new NumericUpDown();
            btnBuCalisanaKaydet = new Button();
            btnTumuneUygula = new Button();
            label3 = new Label();
            btnRaporAl = new Button();
            label4 = new Label();
            label5 = new Label();
            txtEkEmail = new TextBox();
            btnGonder = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKayitlar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHareketsizlikDakika).BeginInit();
            SuspendLayout();
            // 
            // dgvKayitlar
            // 
            dgvKayitlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKayitlar.Location = new Point(2, 279);
            dgvKayitlar.Name = "dgvKayitlar";
            dgvKayitlar.RowHeadersWidth = 51;
            dgvKayitlar.Size = new Size(760, 185);
            dgvKayitlar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(165, 53);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 1;
            label1.Text = "Çalışan Seç:";
            // 
            // btnAra
            // 
            btnAra.Location = new Point(480, 44);
            btnAra.Name = "btnAra";
            btnAra.Size = new Size(94, 29);
            btnAra.TabIndex = 2;
            btnAra.Text = "Ara";
            btnAra.UseVisualStyleBackColor = true;
            btnAra.Click += btnAra_Click;
            // 
            // cmbCalisanlar
            // 
            cmbCalisanlar.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCalisanlar.FormattingEnabled = true;
            cmbCalisanlar.Location = new Point(308, 44);
            cmbCalisanlar.Name = "cmbCalisanlar";
            cmbCalisanlar.Size = new Size(151, 28);
            cmbCalisanlar.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(165, 201);
            label2.Name = "label2";
            label2.Size = new Size(173, 20);
            label2.TabIndex = 4;
            label2.Text = "Hareketsizlik süresi ayarı:";
            // 
            // nudHareketsizlikDakika
            // 
            nudHareketsizlikDakika.Location = new Point(228, 228);
            nudHareketsizlikDakika.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudHareketsizlikDakika.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudHareketsizlikDakika.Name = "nudHareketsizlikDakika";
            nudHareketsizlikDakika.Size = new Size(57, 27);
            nudHareketsizlikDakika.TabIndex = 5;
            nudHareketsizlikDakika.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnBuCalisanaKaydet
            // 
            btnBuCalisanaKaydet.Location = new Point(312, 224);
            btnBuCalisanaKaydet.Name = "btnBuCalisanaKaydet";
            btnBuCalisanaKaydet.Size = new Size(172, 34);
            btnBuCalisanaKaydet.TabIndex = 6;
            btnBuCalisanaKaydet.Text = "Bu Çalışana Kaydet";
            btnBuCalisanaKaydet.UseVisualStyleBackColor = true;
            btnBuCalisanaKaydet.Click += btnBuCalisanaKaydet_Click;
            // 
            // btnTumuneUygula
            // 
            btnTumuneUygula.Location = new Point(504, 224);
            btnTumuneUygula.Name = "btnTumuneUygula";
            btnTumuneUygula.Size = new Size(142, 31);
            btnTumuneUygula.TabIndex = 7;
            btnTumuneUygula.Text = "Tümüne Uygula";
            btnTumuneUygula.UseVisualStyleBackColor = true;
            btnTumuneUygula.Click += btnTumuneUygula_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(165, 232);
            label3.Name = "label3";
            label3.Size = new Size(57, 20);
            label3.TabIndex = 8;
            label3.Text = "Dakika:";
            // 
            // btnRaporAl
            // 
            btnRaporAl.Location = new Point(591, 43);
            btnRaporAl.Name = "btnRaporAl";
            btnRaporAl.Size = new Size(94, 29);
            btnRaporAl.TabIndex = 9;
            btnRaporAl.Text = "Rapor Al";
            btnRaporAl.UseVisualStyleBackColor = true;
            btnRaporAl.Click += btnRaporAl_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(165, 92);
            label4.Name = "label4";
            label4.Size = new Size(109, 20);
            label4.TabIndex = 10;
            label4.Text = "Email İşlemleri:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(163, 123);
            label5.Name = "label5";
            label5.Size = new Size(226, 20);
            label5.TabIndex = 12;
            label5.Text = "Alıcı E-posta(lar) (virgülle ayırın):";
            // 
            // txtEkEmail
            // 
            txtEkEmail.Location = new Point(165, 157);
            txtEkEmail.Name = "txtEkEmail";
            txtEkEmail.Size = new Size(205, 27);
            txtEkEmail.TabIndex = 13;
            // 
            // btnGonder
            // 
            btnGonder.Location = new Point(376, 155);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(72, 29);
            btnGonder.TabIndex = 14;
            btnGonder.Text = "Gönder";
            btnGonder.UseVisualStyleBackColor = true;
            btnGonder.Click += btnGonder_Click;
            // 
            // KayitlarForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 465);
            Controls.Add(btnGonder);
            Controls.Add(txtEkEmail);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(btnRaporAl);
            Controls.Add(label3);
            Controls.Add(btnTumuneUygula);
            Controls.Add(btnBuCalisanaKaydet);
            Controls.Add(nudHareketsizlikDakika);
            Controls.Add(label2);
            Controls.Add(cmbCalisanlar);
            Controls.Add(btnAra);
            Controls.Add(label1);
            Controls.Add(dgvKayitlar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "KayitlarForm";
            Text = "Çalışma Kayıtları ";
            ((System.ComponentModel.ISupportInitialize)dgvKayitlar).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHareketsizlikDakika).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvKayitlar;
        private Label label1;
        private Button btnAra;
        private ComboBox cmbCalisanlar;
        private Label label2;
        private NumericUpDown nudHareketsizlikDakika;
        private Button btnBuCalisanaKaydet;
        private Button btnTumuneUygula;
        private Label label3;
        private Button btnRaporAl;
        private Label label4;
        private Label label5;
        private TextBox txtEkEmail;
        private Button btnGonder;
    }
}