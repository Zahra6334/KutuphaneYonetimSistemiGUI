
using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using KutuphaneYonetimSistemiGUI.sqlbaglantest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;


namespace KutuphaneYonetimSistemiGUI.Managers
{
    public class BookManager : IBookManager
    {
        private readonly string _filePath;
        private List<Book> _books;

        public BookManager()
        {
            // Uygulama dizininde Data klasörü içinde sakla
            var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataDirectory = Path.Combine(projectDirectory, "Data");
            Directory.CreateDirectory(dataDirectory);
            _filePath = @"C:\Users\ZAHRA\source\repos\KutuphaneYonetimSistemiGUI\KutuphaneYonetimSistemiGUI\Data\books.json";


            Console.WriteLine("filepath", _filePath);

            LoadBooks();
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("ISBN boş olamaz");

            if (_books.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException("Bu ISBN ile zaten bir kitap mevcut");

            _books.Add(book);
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.AddBook(book);
            SaveBooks();
        }


        public void DeleteBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN boş olamaz");

            var bookToRemove = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToRemove == null)
                throw new KeyNotFoundException("Belirtilen ISBN ile kitap bulunamadı");

            _books.Remove(bookToRemove);
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.DeleteBookByIsbn(isbn);
            SaveBooks();
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.ISBN))
                throw new ArgumentException("ISBN boş olamaz");

            var index = _books.FindIndex(b => b.ISBN == book.ISBN);
            if (index == -1)
                throw new KeyNotFoundException("Güncellenecek kitap bulunamadı");

            _books[index] = book;
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.UpdateBookByIsbn(book);
            SaveBooks();
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books); // Dışarıya kopyası  döndür
        }

        public Book GetBookByISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN boş olamaz");

            return _books.FirstOrDefault(b => b.ISBN == isbn); // Deep copy döndür
        }

        private void LoadBooks()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    _books = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
                }
                else
                {
                    _books = new List<Book>();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kitaplar yüklenirken hata oluştu: {ex.Message}");
                _books = new List<Book>();
            }
        }

        private void SaveBooks()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_books, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Kitaplar kaydedilirken hata oluştu: {ex.Message}");
                throw; // Hatanın yukarıya iletilmesi
            }
        }

        List<Book> IBookManager.ListAllBooks()
        {
            throw new NotImplementedException();
        }

        List<Book> IBookManager.ListAvailableBooks()
        {
            throw new NotImplementedException();
        }
    }

}
