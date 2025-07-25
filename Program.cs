using KutuphaneYonetimSistemiGUI.Forms;
using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneYonetimSistemiGUI
{
      static class Program
    {
      
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
        }
    }
