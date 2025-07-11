using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Formatting = System.Xml.Formatting;

namespace KutuphaneYonetimSistemiGUI.Managers
{
    public class LoanManager : ILoanManager
    {
        private List<Book> _books;
        private string _booksFilePath = "Data/books.json";

        public LoanManager()
        {
            if (File.Exists(_booksFilePath))
            {
                string json = File.ReadAllText(_booksFilePath);
                _books = JsonConvert.DeserializeObject<List<Book>>(json);
            }
            else
            {
                _books = new List<Book>();
                SaveBooks();
            }
        }

        public void LoanBook(string username, string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false; // ödünç verildi
                book.BorrowedBy = username; // ödünç alan kullanıcı
                SaveBooks();
            }
        }

        public void ReturnBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null && !book.IsAvailable)
            {
                book.IsAvailable = true; // iade edildi
                book.BorrowedBy = null; // kullanıcı bilgisi temizlendi
                SaveBooks();
            }
        }

        public List<Book> GetBorrowedBooks(string username)
        {
            return _books.Where(b => !b.IsAvailable && b.BorrowedBy == username).ToList();
        }

        private void SaveBooks()
        {
            string json = JsonConvert.SerializeObject(_books, (Newtonsoft.Json.Formatting)Formatting.Indented);

            Directory.CreateDirectory("Data");
            File.WriteAllText(_booksFilePath, json);
        }

        internal void BorrowBook(int userId, string ıSBN)
        {
            throw new NotImplementedException();
        }
    }
}