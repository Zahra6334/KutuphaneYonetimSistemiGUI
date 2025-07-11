using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // EKSİK OLAN BU!

namespace KutuphaneYonetimSistemiGUI.Managers
{
    public class UserManager : IUserManager
    {
        private List<User> _users;
        private User _currentUser;
        private readonly string _filePath = "Data/users.json";

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

        public bool Login(string username, string password)
        {
            foreach (var user in _users)
            {
                if (user.Username == username && user.Password == password)
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
        public List<User> GetAllUsers() // 🔥 BU METODU EKLEDİK
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
