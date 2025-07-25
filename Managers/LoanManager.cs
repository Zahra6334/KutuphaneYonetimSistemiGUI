using KutuphaneYonetimSistemiGUI.Interfaces;
using KutuphaneYonetimSistemiGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KutuphaneYonetimSistemiGUI.Managers
{
    public class LoanManager : ILoanManager
    {
        private List<Book> _books;
        private List<LoanRequest> _loanRequests;

        private readonly string _booksFilePath = @"C:\Users\ZAHRA\source\repos\KutuphaneYonetimSistemiGUI\KutuphaneYonetimSistemiGUI\Data\books.json";
        private readonly string _loansFilePath = @"C:\Users\ZAHRA\source\repos\KutuphaneYonetimSistemiGUI\KutuphaneYonetimSistemiGUI\Data\loans.json";

        public LoanManager()
        {
            // Kitapları oku
            if (File.Exists(_booksFilePath))
            {
                string json = File.ReadAllText(_booksFilePath);
                _books = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
            }
            else
            {
                _books = new List<Book>();
                SaveBooks();
            }

            // Loan kayıtlarını oku
            if (File.Exists(_loansFilePath))
            {
                string json = File.ReadAllText(_loansFilePath);
                _loanRequests = JsonConvert.DeserializeObject<List<LoanRequest>>(json) ?? new List<LoanRequest>();
            }
            else
            {
                _loanRequests = new List<LoanRequest>();
                SaveLoans();
            }
        }

        public void LoanBook(string username, string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null && book.IsAvailable)
            {
                // Kitap güncelle
                book.IsAvailable = false;
                book.BorrowedBy = username;
                SaveBooks();

                // Loan kaydı oluştur
                var request = new LoanRequest
                {
                    Username = username,
                    ISBN = isbn,
                    RequestDate = DateTime.Now,
                    Status = "Onaylandı"
                };

                _loanRequests.Add(request);
                SaveLoans();
            }
            else
            {
                throw new InvalidOperationException("Kitap bulunamadı ya da zaten ödünç alınmış.");
            }
        }

        public void ReturnBook(string isbn)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null && !book.IsAvailable)
            {
                book.IsAvailable = true;
                book.BorrowedBy = null;
                SaveBooks();

                
                var lastLoan = _loanRequests.LastOrDefault(l => l.ISBN == isbn && l.Status == "Onaylandı");
                if (lastLoan != null)
                {
                    lastLoan.Status = "İade Edildi";
                    SaveLoans();
                }
            }
        }
        private void LoadLoans()
        {
            if (File.Exists(_loansFilePath))
            {
                string json = File.ReadAllText(_loansFilePath);
                _loanRequests = JsonConvert.DeserializeObject<List<LoanRequest>>(json) ?? new List<LoanRequest>();
            }
            else
            {
                _loanRequests = new List<LoanRequest>();
                SaveLoans(); 
            }
        }


        public List<Book> GetBorrowedBooks(string username)
        {
            return _books.Where(b => !b.IsAvailable && b.BorrowedBy == username).ToList();
        }

        private void SaveBooks()
        {
            string json = JsonConvert.SerializeObject(_books, Newtonsoft.Json.Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(_booksFilePath));
            File.WriteAllText(_booksFilePath, json);
        }

        private void SaveLoans()
        {
            string json = JsonConvert.SerializeObject(_loanRequests, Newtonsoft.Json.Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(_loansFilePath));
            File.WriteAllText(_loansFilePath, json);
        }
        public List<LoanRequest> GetPendingRequests()
        {
            LoadLoans();
            return _loanRequests.Where(r => r.Status == "Beklemede").ToList();
        }

        public void ApproveRequest(LoanRequest request)
        {
            LoadLoans();
            var target = _loanRequests.FirstOrDefault(r => r.Username == request.Username && r.ISBN == request.ISBN && r.Status == "Beklemede");
            if (target != null)
            {
                target.Status = "Onaylandı";
                SaveLoans();
                LoanBook(target.Username, target.ISBN); // kitap durumu güncellenir
            }
        }


        internal void BorrowBook(int userId, string isbn)
        {
            // Not: Gerçek uygulamada kullanıcı adı UserManager'dan çekilir. Şimdilik örnek olsun diye userId -> Username dönüşümü basit tutuldu
            string username = $"kullanici_{userId}";

            // Daha önce aynı kitap için bekleyen istek var mı kontrol et (opsiyonel)
            bool alreadyRequested = _loanRequests.Any(r => r.ISBN == isbn && r.Username == username && r.Status == "Beklemede");
            if (alreadyRequested)
            {
                throw new InvalidOperationException("Bu kullanıcı zaten bu kitap için bir istek göndermiş.");
            }

            LoanRequest request = new LoanRequest
            {
                Username = username,
                ISBN = isbn,
                RequestDate = DateTime.Now,
                Status = "Beklemede"
            };

            _loanRequests.Add(request);
            SaveLoans();
        }

    }
}
