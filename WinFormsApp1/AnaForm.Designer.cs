namespace WinFormsApp1
{
    partial class AnaForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            lblHosgeldin = new Label();
            label2 = new Label();
            txtKonu = new TextBox();
            label3 = new Label();
            txtIcerik = new TextBox();
            btnBaslatDurdur = new Button();
            label5 = new Label();
            lblBaslangicSaati = new Label();
            label6 = new Label();
            lblGecenSure = new Label();
            sureTimer = new System.Windows.Forms.Timer(components);
            btnManuelSure = new Button();
            pnlFooter = new Panel();
            lblToplamSure = new Label();
            lblSaatIkon = new Label();
            lblToplamSureBaslik = new Label();
            btnRaporAl = new Button();
            idleTimer = new System.Windows.Forms.Timer(components);
            cmbRaporYil = new ComboBox();
            cmbRaporAy = new ComboBox();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // lblHosgeldin
            // 
            lblHosgeldin.AutoSize = true;
            lblHosgeldin.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblHosgeldin.Location = new Point(105, 9);
            lblHosgeldin.Name = "lblHosgeldin";
            lblHosgeldin.Size = new Size(109, 17);
            lblHosgeldin.TabIndex = 0;
            lblHosgeldin.Text = "Hoş geldin, [isim]";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(133, 38);
            label2.Name = "label2";
            label2.Size = new Size(116, 20);
            label2.TabIndex = 1;
            label2.Text = "Çalışma Konusu:";
            // 
            // txtKonu
            // 
            txtKonu.Location = new Point(77, 61);
            txtKonu.Name = "txtKonu";
            txtKonu.Size = new Size(231, 27);
            txtKonu.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(161, 91);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 3;
            label3.Text = "İçerik:";
            // 
            // txtIcerik
            // 
            txtIcerik.Location = new Point(77, 117);
            txtIcerik.Multiline = true;
            txtIcerik.Name = "txtIcerik";
            txtIcerik.Size = new Size(236, 87);
            txtIcerik.TabIndex = 4;
            // 
            // btnBaslatDurdur
            // 
            btnBaslatDurdur.AutoSize = true;
            btnBaslatDurdur.Location = new Point(77, 210);
            btnBaslatDurdur.Name = "btnBaslatDurdur";
            btnBaslatDurdur.Size = new Size(236, 30);
            btnBaslatDurdur.TabIndex = 7;
            btnBaslatDurdur.Text = "Başlat";
            btnBaslatDurdur.UseVisualStyleBackColor = true;
            btnBaslatDurdur.Click += btnBaslatDurdur_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(78, 244);
            label5.Name = "label5";
            label5.Size = new Size(112, 20);
            label5.TabIndex = 8;
            label5.Text = "Başlangıç Saati:";
            // 
            // lblBaslangicSaati
            // 
            lblBaslangicSaati.AutoSize = true;
            lblBaslangicSaati.Location = new Point(80, 275);
            lblBaslangicSaati.Name = "lblBaslangicSaati";
            lblBaslangicSaati.Size = new Size(27, 20);
            lblBaslangicSaati.TabIndex = 9;
            lblBaslangicSaati.Text = "---";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(208, 244);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 10;
            label6.Text = "Geçen Süre:";
            // 
            // lblGecenSure
            // 
            lblGecenSure.AutoSize = true;
            lblGecenSure.Location = new Point(215, 275);
            lblGecenSure.Name = "lblGecenSure";
            lblGecenSure.Size = new Size(27, 20);
            lblGecenSure.TabIndex = 11;
            lblGecenSure.Text = "---";
            // 
            // sureTimer
            // 
            sureTimer.Interval = 60000;
            sureTimer.Tick += sureTimer_Tick;
            // 
            // btnManuelSure
            // 
            btnManuelSure.Location = new Point(77, 329);
            btnManuelSure.Name = "btnManuelSure";
            btnManuelSure.Size = new Size(112, 32);
            btnManuelSure.TabIndex = 14;
            btnManuelSure.Text = "Manuel Kayıt";
            btnManuelSure.UseVisualStyleBackColor = true;
            btnManuelSure.Click += btnManuelSure_Click;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.DarkCyan;
            pnlFooter.Controls.Add(lblToplamSure);
            pnlFooter.Controls.Add(lblSaatIkon);
            pnlFooter.Controls.Add(lblToplamSureBaslik);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 367);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(392, 39);
            pnlFooter.TabIndex = 15;
            // 
            // lblToplamSure
            // 
            lblToplamSure.AllowDrop = true;
            lblToplamSure.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblToplamSure.ForeColor = Color.White;
            lblToplamSure.Location = new Point(120, 17);
            lblToplamSure.Name = "lblToplamSure";
            lblToplamSure.Size = new Size(259, 28);
            lblToplamSure.TabIndex = 14;
            lblToplamSure.Text = "---";
            // 
            // lblSaatIkon
            // 
            lblSaatIkon.AutoSize = true;
            lblSaatIkon.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblSaatIkon.ForeColor = Color.White;
            lblSaatIkon.Location = new Point(3, 4);
            lblSaatIkon.Name = "lblSaatIkon";
            lblSaatIkon.Size = new Size(46, 31);
            lblSaatIkon.TabIndex = 15;
            lblSaatIkon.Text = "🕛";
            // 
            // lblToplamSureBaslik
            // 
            lblToplamSureBaslik.AutoSize = true;
            lblToplamSureBaslik.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblToplamSureBaslik.ForeColor = Color.Silver;
            lblToplamSureBaslik.Location = new Point(120, 0);
            lblToplamSureBaslik.Name = "lblToplamSureBaslik";
            lblToplamSureBaslik.Size = new Size(138, 17);
            lblToplamSureBaslik.TabIndex = 16;
            lblToplamSureBaslik.Text = "Toplam çalışılan süre";
            // 
            // btnRaporAl
            // 
            btnRaporAl.Location = new Point(200, 329);
            btnRaporAl.Name = "btnRaporAl";
            btnRaporAl.Size = new Size(113, 32);
            btnRaporAl.TabIndex = 16;
            btnRaporAl.Text = "Rapor Al";
            btnRaporAl.UseVisualStyleBackColor = true;
            btnRaporAl.Click += btnRaporAl_Click;
            // 
            // idleTimer
            // 
            idleTimer.Interval = 1000;
            idleTimer.Tick += idleTimer_Tick;
            // 
            // cmbRaporYil
            // 
            cmbRaporYil.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRaporYil.FormattingEnabled = true;
            cmbRaporYil.Location = new Point(80, 295);
            cmbRaporYil.Name = "cmbRaporYil";
            cmbRaporYil.Size = new Size(109, 28);
            cmbRaporYil.TabIndex = 17;
            cmbRaporYil.SelectedIndexChanged += cmbRaporYil_SelectedIndexChanged;
            // 
            // cmbRaporAy
            // 
            cmbRaporAy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRaporAy.FormattingEnabled = true;
            cmbRaporAy.Location = new Point(200, 295);
            cmbRaporAy.Name = "cmbRaporAy";
            cmbRaporAy.Size = new Size(113, 28);
            cmbRaporAy.TabIndex = 18;
            // 
            // AnaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(392, 406);
            Controls.Add(cmbRaporAy);
            Controls.Add(cmbRaporYil);
            Controls.Add(btnRaporAl);
            Controls.Add(pnlFooter);
            Controls.Add(btnManuelSure);
            Controls.Add(lblGecenSure);
            Controls.Add(label6);
            Controls.Add(lblBaslangicSaati);
            Controls.Add(label5);
            Controls.Add(btnBaslatDurdur);
            Controls.Add(txtIcerik);
            Controls.Add(label3);
            Controls.Add(txtKonu);
            Controls.Add(label2);
            Controls.Add(lblHosgeldin);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AnaForm";
            Text = "Çalışma Takibi";
            FormClosing += AnaForm_FormClosing;
            KeyDown += AnaForm_KeyDown;
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHosgeldin;
        private Label label2;
        private TextBox txtKonu;
        private Label label3;
        private TextBox txtIcerik;
        private Button btnBaslatDurdur;
        private Label label5;
        private Label lblBaslangicSaati;
        private Label label6;
        private Label lblGecenSure;
        private System.Windows.Forms.Timer sureTimer;
        private Button btnManuelSure;
        private Panel pnlFooter;
        private Label lblToplamSure;
        private Label lblSaatIkon;
        private Label lblToplamSureBaslik;
        private Button btnRaporAl;
        private NotifyIcon notifyIconAna;
        private System.Windows.Forms.Timer idleTimer;
        private ComboBox cmbRaporYil;
        private ComboBox cmbRaporAy;
    }
}
