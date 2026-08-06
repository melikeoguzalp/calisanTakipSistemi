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
            label7 = new Label();
            lblToplamSure = new Label();
            sureTimer = new System.Windows.Forms.Timer(components);
            btnManuelSure = new Button();
            SuspendLayout();
            // 
            // lblHosgeldin
            // 
            lblHosgeldin.AutoSize = true;
            lblHosgeldin.Location = new Point(173, 5);
            lblHosgeldin.Name = "lblHosgeldin";
            lblHosgeldin.Size = new Size(125, 20);
            lblHosgeldin.TabIndex = 0;
            lblHosgeldin.Text = "Hoş geldin, [isim]";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(134, 36);
            label2.Name = "label2";
            label2.Size = new Size(116, 20);
            label2.TabIndex = 1;
            label2.Text = "Çalışma Konusu:";
            // 
            // txtKonu
            // 
            txtKonu.Location = new Point(136, 66);
            txtKonu.Name = "txtKonu";
            txtKonu.Size = new Size(231, 27);
            txtKonu.TabIndex = 2;
         
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(136, 117);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 3;
            label3.Text = "İçerik:";
            // 
            // txtIcerik
            // 
            txtIcerik.Location = new Point(136, 140);
            txtIcerik.Multiline = true;
            txtIcerik.Name = "txtIcerik";
            txtIcerik.Size = new Size(231, 100);
            txtIcerik.TabIndex = 4;
            // 
            // btnBaslatDurdur
            // 
            btnBaslatDurdur.AutoSize = true;
            btnBaslatDurdur.Location = new Point(134, 273);
            btnBaslatDurdur.Name = "btnBaslatDurdur";
            btnBaslatDurdur.Size = new Size(236, 51);
            btnBaslatDurdur.TabIndex = 7;
            btnBaslatDurdur.Text = "Başlat";
            btnBaslatDurdur.UseVisualStyleBackColor = true;
            btnBaslatDurdur.Click += btnBaslatDurdur_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(137, 338);
            label5.Name = "label5";
            label5.Size = new Size(112, 20);
            label5.TabIndex = 8;
            label5.Text = "Başlangıç Saati:";
            // 
            // lblBaslangicSaati
            // 
            lblBaslangicSaati.AutoSize = true;
            lblBaslangicSaati.Location = new Point(139, 369);
            lblBaslangicSaati.Name = "lblBaslangicSaati";
            lblBaslangicSaati.Size = new Size(27, 20);
            lblBaslangicSaati.TabIndex = 9;
            lblBaslangicSaati.Text = "---";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(267, 338);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 10;
            label6.Text = "Geçen Süre:";
            // 
            // lblGecenSure
            // 
            lblGecenSure.AutoSize = true;
            lblGecenSure.Location = new Point(274, 369);
            lblGecenSure.Name = "lblGecenSure";
            lblGecenSure.Size = new Size(27, 20);
            lblGecenSure.TabIndex = 11;
            lblGecenSure.Text = "---";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(176, 419);
            label7.Name = "label7";
            label7.Size = new Size(154, 20);
            label7.TabIndex = 12;
            label7.Text = "Toplam Çalışılan Süre:";
            // 
            // lblToplamSure
            // 
            lblToplamSure.AllowDrop = true;
            lblToplamSure.Location = new Point(181, 453);
            lblToplamSure.Name = "lblToplamSure";
            lblToplamSure.Size = new Size(186, 35);
            lblToplamSure.TabIndex = 13;
            lblToplamSure.Text = "---";
            // 
            // sureTimer
            // 
            sureTimer.Interval = 60000;
            sureTimer.Tick += sureTimer_Tick;
            // 
            // btnManuelSure
            // 
            btnManuelSure.Location = new Point(159, 503);
            btnManuelSure.Name = "btnManuelSure";
            btnManuelSure.Size = new Size(194, 53);
            btnManuelSure.TabIndex = 14;
            btnManuelSure.Text = "Manuel Çalışma Saati Gir";
            btnManuelSure.UseVisualStyleBackColor = true;
            btnManuelSure.Click += btnManuelSure_Click;
            // 
            // AnaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 599);
            Controls.Add(btnManuelSure);
            Controls.Add(lblToplamSure);
            Controls.Add(label7);
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
            Name = "AnaForm";
            Text = "Çalışma Takibi";
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
        private Label label7;
        private Label lblToplamSure;
        private System.Windows.Forms.Timer sureTimer;
        private Button btnManuelSure;
    }
}
