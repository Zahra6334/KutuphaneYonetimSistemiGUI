using KutuphaneYonetimSistemiGUI.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneYonetimSistemiGUI.Forms
{
    public partial class RegisterForm : Form
    {

        private readonly UserManager _userManager;
        public RegisterForm(UserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void kaydol_btn_Click(object sender, EventArgs e)
        {
            string kullaniciadi=KullaniciAdi_txt.Text.Trim();
            string sifre=Sifre_txt.Text.Trim();
            if (_userManager.Register(kullaniciadi, sifre))
            {
                uyarı_lbl.Text = "Kayıt başarılı";
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm=new LoginForm();
            loginForm.Show();
        }
    }
}
