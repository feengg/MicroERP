using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public class Invoice
    {
        public int ID_Invoice { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Invoicenumber { get; set; }
        public String  Comment { get; set; }
        public String Message { get; set; }
        
    }


}
