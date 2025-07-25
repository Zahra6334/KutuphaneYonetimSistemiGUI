using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using KutuphaneYonetimSistemiGUI.sqlbaglantest;

namespace KutuphaneYonetimSistemiGUI.Managers
{
    public class UserManager : IUserManager
    {
        private List<User> _users;
        private User _currentUser;
        private readonly string _filePath = @"C:\Users\ZAHRA\source\repos\KutuphaneYonetimSistemiGUI\KutuphaneYonetimSistemiGUI\Data\users.json";

        public UserManager()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _users = JsonConvert.DeserializeObject<List<User>>(json);

            }
            else
            {
                _users = new List<User>
                {
                    new User {Id=1, Username = "admin", Password = "admin123", IsAdmin = true }
                };
                SaveUsers();
            }
        }
        public bool Register(string username, string password, bool isAdmin = false)
        {
            // Aynı kullanıcı adıyla bir kullanıcı varsa kayıt başarısız olur
            if (_users.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            DatabaseDataProvider db = new DatabaseDataProvider();
            // Yeni kullanıcı oluşturuluyor
            var newUser = new User
            {
                Id = _users.Count + 1,
                Username = username,
                Password = db.HashPassword(password),
                IsAdmin = isAdmin
            };

            
            db.AddUser(newUser);

            _users.Add(newUser);
            SaveUsers(); // Dosyaya kaydet
            return true;
        }

        public bool Login(string username, string password)
        {
            DatabaseDataProvider db = new DatabaseDataProvider();   
            foreach (var user in _users)
            {
                if (user.Username == username && user.Password == db.HashPassword(password))
                {
                    _currentUser = user;
                    return true;
                }
            }
            return false;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }
        public List<User> GetAllUsers() 
        {
            return _users;
        }
        private void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
            Directory.CreateDirectory("Data");
            File.WriteAllText(_filePath, json);
            

        }
    }
}
