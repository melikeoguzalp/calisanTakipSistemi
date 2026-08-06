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
            dgvKayitlar = new DataGridView();
            label1 = new Label();
            btnAra = new Button();
            cmbCalisanlar = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvKayitlar).BeginInit();
            SuspendLayout();
            // 
            // dgvKayitlar
            // 
            dgvKayitlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKayitlar.Location = new Point(0, 127);
            dgvKayitlar.Name = "dgvKayitlar";
            dgvKayitlar.RowHeadersWidth = 51;
            dgvKayitlar.Size = new Size(760, 288);
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
            // KayitlarForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 415);
            Controls.Add(cmbCalisanlar);
            Controls.Add(btnAra);
            Controls.Add(label1);
            Controls.Add(dgvKayitlar);
            Name = "KayitlarForm";
            Text = "Çalışma Kayıtları ";
            ((System.ComponentModel.ISupportInitialize)dgvKayitlar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvKayitlar;
        private Label label1;
        private Button btnAra;
        private ComboBox cmbCalisanlar;
    }
}