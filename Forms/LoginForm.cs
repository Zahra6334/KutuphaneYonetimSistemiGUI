using KutuphaneYonetimSistemiGUI.Managers;
using KutuphaneYonetimSistemiGUI.sqlbaglantest;
using System;
using System.Linq;
using System.Windows.Forms;
namespace KutuphaneYonetimSistemiGUI.Forms
{
  public partial class LoginForm : Form
    {
        private readonly UserManager _userManager;

        // Form kontrolleri
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Button button1;
        private Label lblPassword;

        public LoginForm()
        {
            InitializeComponent();
            _userManager = new UserManager();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(160, 47);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(160, 97);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 22);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(160, 150);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 29);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Giriş Yap";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(50, 50);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(82, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Kullanıcı Adı:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(50, 100);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(37, 16);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Şifre:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "db bagla";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(380, 230);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginForm";
            this.Text = "Kütüphane Yönetim Sistemi - Giriş";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (_userManager.Login(username, password))
            {
                var user = _userManager.GetCurrentUser();


                if (user != null)
                {
                    if (user.IsAdmin)
                    {
                        AdminForm adminForm = new AdminForm();
                        adminForm.Show();
                    }
                    else
                    {
                        UserForm userForm = new UserForm(user.Id,user.Username);
                        userForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.TestConnection();
        }
    }
}
