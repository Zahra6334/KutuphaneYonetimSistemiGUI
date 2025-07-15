using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemiGUI.Models
{
    public class LoanRequest //Ödünç İstek) classı
    {
        public string Username { get; set; }      
        public string ISBN { get; set; }          
        public DateTime RequestDate { get; set; } 
        public string Status { get; set; } = "Beklemede"; 
    }
}
