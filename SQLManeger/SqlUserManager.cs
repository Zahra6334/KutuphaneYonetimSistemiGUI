using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using KutuphaneYonetimSistemiGUI.sqlbaglantest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.SQLManeger
{
    public class SqlUserManager: IUserManager
    {
        public bool Login(string username, string password)
        {
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.HashPassword(password);
            db.HashPassword(username);
            return true;
        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public bool Register(string username, string password, bool isAdmin = false)
        {
            DatabaseDataProvider db = new DatabaseDataProvider();
            // Yeni kullanıcı oluşturuluyor
            var newUser = new User
            {
               
                Username = username,
                Password = db.HashPassword(password),
                IsAdmin = isAdmin
            };


            db.AddUser(newUser);
            return true;
        }

       
    }
}
