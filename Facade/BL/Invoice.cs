using System.Collections.Generic;
using System.Xml.Serialization;

namespace Interface
{
    /* Invoice */

    [XmlRoot("Invoices")]
    public class InvoiceList
    {
        [XmlElement("Invoice")]
        public List<Invoice> invoice { get; set; }

        public InvoiceList()
        {
            this.invoice = new List<Invoice>();
        }
    }

    public class Invoice
    {
        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("Date")]
        public string Datum { get; set; }

        [XmlElement("PaymentDate")]
        public string Faelligkeit { get; set; }

        [XmlElement("Number")]
        public string Nummer { get; set; }

        [XmlElement("Comment")]
        public string Kommentar { get; set; }

        [XmlElement("Message")]
        public string Nachricht { get; set; }

        [XmlElement("Amount")]
        public string Menge { get; set; }

        [XmlElement("UnitPrice ")]
        public string Stueckpreis { get; set; }

        [XmlElement("Ust")]
        public string Ust { get; set; }
    }
}