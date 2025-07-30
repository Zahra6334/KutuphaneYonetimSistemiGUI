using KutuphaneYonetimSistemiGUI.Managers;
using KutuphaneYonetimSistemiGUI.SQLManeger;
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
        private readonly SqlUserManager _sqlUserManager;
        public RegisterForm(UserManager userManager,SqlUserManager sqluserManager)
        {
            _userManager = userManager;
            _sqlUserManager = sqluserManager;

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
               

            }
            if (_sqlUserManager.Register(kullaniciadi, sifre))
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

        private void KullaniciAdi_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
