using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Interfaces
{
    public interface ILoanManager
    {
        void LoanBook(string isbn, string username);
        void ReturnBook(string isbn);
        List<Book> GetBorrowedBooks(string username);
    }
}
