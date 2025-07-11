using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Interfaces
{
    public interface IUserManager
    {
        bool Login(string username, string password);
        User GetCurrentUser();
    }
}
