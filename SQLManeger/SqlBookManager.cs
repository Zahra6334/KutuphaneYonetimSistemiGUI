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
    internal class SqlBookManager : IBookManager
    {
        public void AddBook(Book book)
        {
           
            
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.AddBook(book);

        }

        public void DeleteBook(string isbn)
        {

            DatabaseDataProvider db = new DatabaseDataProvider();
            db.DeleteBookByIsbn(isbn);
           
        }

        public List<Book> ListAllBooks()
        {
            throw new NotImplementedException();
        }

        public List<Book> ListAvailableBooks()
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book updatedBook)
        {
            DatabaseDataProvider db = new DatabaseDataProvider();
            db.UpdateBookByIsbn(updatedBook);
        }
    }
}
