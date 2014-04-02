using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public class Contact
    {
        public int ID { get; set; }
        public String Titel { get; set; }
        public String Vorname { get; set; }
        public String Nachname { get; set; }
        public String Suffix { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public String Adresse { get; set; }
        public String Lieferadresse { get; set; }
        public String Rechnungsadresse { get; set; }
       // public int ID_Person { get; set; }
    }


}
