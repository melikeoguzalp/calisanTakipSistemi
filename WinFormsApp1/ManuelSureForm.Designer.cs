namespace WinFormsApp1
{
    partial class ManuelSureForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            dtpBaslangic = new DateTimePicker();
            label2 = new Label();
            dtpBitis = new DateTimePicker();
            btnOnayla = new Button();
            btnIptal = new Button();
            label3 = new Label();
            txtManuelKonu = new TextBox();
            label4 = new Label();
            txtManuelIcerik = new TextBox();
            label5 = new Label();
            txtNeden = new TextBox();
            toolTipNeden = new ToolTip(components);
            lblManuelToplamSure = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(207, 23);
            label1.Name = "label1";
            label1.Size = new Size(112, 20);
            label1.TabIndex = 0;
            label1.Text = "Başlangıç Saati:";
            // 
            // dtpBaslangic
            // 
            dtpBaslangic.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpBaslangic.Format = DateTimePickerFormat.Custom;
            dtpBaslangic.Location = new Point(392, 23);
            dtpBaslangic.Name = "dtpBaslangic";
            dtpBaslangic.ShowUpDown = true;
            dtpBaslangic.Size = new Size(250, 27);
            dtpBaslangic.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(207, 85);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 2;
            label2.Text = "Bitiş Saati:";
            // 
            // dtpBitis
            // 
            dtpBitis.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpBitis.Format = DateTimePickerFormat.Custom;
            dtpBitis.Location = new Point(392, 85);
            dtpBitis.Name = "dtpBitis";
            dtpBitis.ShowUpDown = true;
            dtpBitis.Size = new Size(250, 27);
            dtpBitis.TabIndex = 3;
            // 
            // btnOnayla
            // 
            btnOnayla.Location = new Point(293, 430);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(94, 29);
            btnOnayla.TabIndex = 4;
            btnOnayla.Text = "Onayla";
            btnOnayla.UseVisualStyleBackColor = true;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // btnIptal
            // 
            btnIptal.DialogResult = DialogResult.Cancel;
            btnIptal.Location = new Point(479, 430);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(94, 29);
            btnIptal.TabIndex = 5;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(207, 131);
            label3.Name = "label3";
            label3.Size = new Size(116, 20);
            label3.TabIndex = 6;
            label3.Text = "Çalışma Konusu:";
            // 
            // txtManuelKonu
            // 
            txtManuelKonu.Location = new Point(392, 128);
            txtManuelKonu.Name = "txtManuelKonu";
            txtManuelKonu.Size = new Size(244, 27);
            txtManuelKonu.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(207, 256);
            label4.Name = "label4";
            label4.Size = new Size(47, 20);
            label4.TabIndex = 8;
            label4.Text = "İçerik:";
            // 
            // txtManuelIcerik
            // 
            txtManuelIcerik.Location = new Point(392, 256);
            txtManuelIcerik.Multiline = true;
            txtManuelIcerik.Name = "txtManuelIcerik";
            txtManuelIcerik.Size = new Size(244, 70);
            txtManuelIcerik.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(207, 366);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 10;
            label5.Text = "Reason:";
            // 
            // txtNeden
            // 
            txtNeden.Location = new Point(392, 366);
            txtNeden.Name = "txtNeden";
            txtNeden.Size = new Size(244, 27);
            txtNeden.TabIndex = 11;
            toolTipNeden.SetToolTip(txtNeden, "Neden manuel girdiğinizi açıklayın (örn: sistemde unutma, teknik arıza vb.)");
            // 
            // toolTipNeden
            // 
            toolTipNeden.IsBalloon = true;
            // 
            // lblManuelToplamSure
            // 
            lblManuelToplamSure.AutoSize = true;
            lblManuelToplamSure.Location = new Point(207, 205);
            lblManuelToplamSure.Name = "lblManuelToplamSure";
            lblManuelToplamSure.Size = new Size(0, 20);
            lblManuelToplamSure.TabIndex = 12;
            // 
            // ManuelSureForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 487);
            Controls.Add(lblManuelToplamSure);
            Controls.Add(txtNeden);
            Controls.Add(label5);
            Controls.Add(txtManuelIcerik);
            Controls.Add(label4);
            Controls.Add(txtManuelKonu);
            Controls.Add(label3);
            Controls.Add(btnIptal);
            Controls.Add(btnOnayla);
            Controls.Add(dtpBitis);
            Controls.Add(label2);
            Controls.Add(dtpBaslangic);
            Controls.Add(label1);
            Name = "ManuelSureForm";
            Text = "ManuelSureForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker dtpBaslangic;
        private Label label2;
        private DateTimePicker dtpBitis;
        private Button btnOnayla;
        private Button btnIptal;
        private Label label3;
        private TextBox txtManuelKonu;
        private Label label4;
        private TextBox txtManuelIcerik;
        private Label label5;
        private TextBox txtNeden;
        private ToolTip toolTipNeden;
        private Label lblManuelToplamSure;
    }
}