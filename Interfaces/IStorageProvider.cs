using KutuphaneYonetimSistemiGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Interfaces
{
    public interface IStorageProvider
    {
        List<Book> LoadBooks();
        void SaveBooks(List<Book> books);

        List<User> LoadUsers();
        void SaveUsers(List<User> users);

        List<LoanRequest> LoadLoans();
        void SaveLoans(List<LoanRequest> loans);
    }
}
