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
    internal class SqlLoanManager : ILoanManager
    {
        public List<Book> GetBorrowedBooks(string username)
        {
            throw new NotImplementedException();
        }

        public void LoanBook(string isbn, string username)
        {
            if (string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("ISBN ve kullanıcı adı boş olamaz.");

            DatabaseDataProvider db = new DatabaseDataProvider();

            // Kullanıcının ID'sini çekiyoruz
            int userId = db.GetUserIdByUsername(username);

            // Kitabı bu kullanıcıya ödünç veriyoruz
            db.LoanBook(isbn, userId);
        }



        public void ReturnBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN boş olamaz.");

            DatabaseDataProvider db = new DatabaseDataProvider();
            db.ReturnBook(isbn);
        }
    }
}
