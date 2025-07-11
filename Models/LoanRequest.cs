using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Models
{
    public class LoanRequest
    {
        public string Username { get; set; }      // Kim istedi
        public string ISBN { get; set; }          // Hangi kitabı
        public DateTime RequestDate { get; set; } // Ne zaman istedi
        public string Status { get; set; } = "Beklemede"; // Beklemede / Onaylandı / Reddedildi
    }
}
