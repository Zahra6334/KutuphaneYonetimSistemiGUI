using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace KutuphaneYonetimSistemiGUI.Data
{
    public class FileStorageProvider : IStorageProvider
    {
        private readonly string booksFilePath = "books.json";
        private readonly string usersFilePath = "users.json";
        private readonly string loansFilePath = "loans.json";

        // Kitapları yükler
        public List<Book> LoadBooks()
        {
            if (!File.Exists(booksFilePath))
                return new List<Book>();

            string json = File.ReadAllText(booksFilePath);
            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        // Kitapları kaydeder
        public void SaveBooks(List<Book> books)
        {
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(booksFilePath, json);
        }

        // Kullanıcıları yükler
        public List<User> LoadUsers()
        {
            if (!File.Exists(usersFilePath))
                return new List<User>();

            string json = File.ReadAllText(usersFilePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        // Kullanıcıları kaydeder
        public void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }

        // Ödünç alma kayıtlarını yükler
        public List<LoanRequest> LoadLoans()
        {
            if (!File.Exists(loansFilePath))
                return new List<LoanRequest>();

            string json = File.ReadAllText(loansFilePath);
            return JsonConvert.DeserializeObject<List<LoanRequest>>(json);
        }

        // Ödünç alma kayıtlarını kaydeder
        public void SaveLoans(List<LoanRequest> loans)
        {
            string json = JsonConvert.SerializeObject(loans, Formatting.Indented);
            File.WriteAllText(loansFilePath, json);
        }
    }
}