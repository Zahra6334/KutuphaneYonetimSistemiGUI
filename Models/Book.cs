using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Models
{
    public class Book
    {
        public string ISBN { get; set; } // Kitaba özel numara
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        // true: Kütüphanede, false: Ödünçte
        public string BorrowedBy { get; set; }  // Kitabı kim ödünç aldı?
    }
}
