using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Interfaces
{
    public interface IBookManager
    {
        void AddBook(Book book);
        void DeleteBook(string isbn);
        void UpdateBook(Book updatedBook);
        List<Book> ListAllBooks();
        List<Book> ListAvailableBooks();
    }
}
