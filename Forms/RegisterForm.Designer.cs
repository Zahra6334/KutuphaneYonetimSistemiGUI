namespace KutuphaneYonetimSistemiGUI.Forms
{
    partial class RegisterForm
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
            this.KullaniciAdi_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Sifrelabel = new System.Windows.Forms.Label();
            this.Sifre_txt = new System.Windows.Forms.TextBox();
            this.kaydol_btn = new System.Windows.Forms.Button();
            this.uyarı_lbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KullaniciAdi_txt
            // 
            this.KullaniciAdi_txt.Location = new System.Drawing.Point(245, 62);
            this.KullaniciAdi_txt.Name = "KullaniciAdi_txt";
            this.KullaniciAdi_txt.Size = new System.Drawing.Size(248, 22);
            this.KullaniciAdi_txt.TabIndex = 0;
            this.KullaniciAdi_txt.TextChanged += new System.EventHandler(this.KullaniciAdi_txt_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanıcı Adı";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Sifrelabel
            // 
            this.Sifrelabel.AutoSize = true;
            this.Sifrelabel.Location = new System.Drawing.Point(242, 106);
            this.Sifrelabel.Name = "Sifrelabel";
            this.Sifrelabel.Size = new System.Drawing.Size(34, 16);
            this.Sifrelabel.TabIndex = 3;
            this.Sifrelabel.Text = "Sifre";
            // 
            // Sifre_txt
            // 
            this.Sifre_txt.Location = new System.Drawing.Point(245, 125);
            this.Sifre_txt.Name = "Sifre_txt";
            this.Sifre_txt.Size = new System.Drawing.Size(248, 22);
            this.Sifre_txt.TabIndex = 2;
            // 
            // kaydol_btn
            // 
            this.kaydol_btn.Location = new System.Drawing.Point(245, 186);
            this.kaydol_btn.Name = "kaydol_btn";
            this.kaydol_btn.Size = new System.Drawing.Size(75, 23);
            this.kaydol_btn.TabIndex = 4;
            this.kaydol_btn.Text = "KAYDOL";
            this.kaydol_btn.UseVisualStyleBackColor = true;
            this.kaydol_btn.Click += new System.EventHandler(this.kaydol_btn_Click);
            // 
            // uyarı_lbl
            // 
            this.uyarı_lbl.AutoSize = true;
            this.uyarı_lbl.Location = new System.Drawing.Point(325, 228);
            this.uyarı_lbl.Name = "uyarı_lbl";
            this.uyarı_lbl.Size = new System.Drawing.Size(0, 16);
            this.uyarı_lbl.TabIndex = 5;
            this.uyarı_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uyarı_lbl.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "GİRİS YAP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uyarı_lbl);
            this.Controls.Add(this.kaydol_btn);
            this.Controls.Add(this.Sifrelabel);
            this.Controls.Add(this.Sifre_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KullaniciAdi_txt);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KullaniciAdi_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Sifrelabel;
        private System.Windows.Forms.TextBox Sifre_txt;
        private System.Windows.Forms.Button kaydol_btn;
        private System.Windows.Forms.Label uyarı_lbl;
        private System.Windows.Forms.Button button1;
    }
}