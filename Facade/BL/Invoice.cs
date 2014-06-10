using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int ID { get; set; }

        [XmlElement("Date")]
        public DateTime Datum { get; set; }

        [XmlElement("PaymentDate")]
        public DateTime Faelligkeit { get; set; }

        [XmlElement("Number")]
        public string Nummer { get; set; }

        [XmlElement("IDContact")]
        public int IDKontakt { get; set; }

        [XmlElement("Billingadress")]
        public string Rechnungsadresse { get; set; }

        [XmlElement("Comment")]
        public string Kommentar { get; set; }

        [XmlElement("Message")]
        public string Nachricht { get; set; }

        [XmlElement("Article1")]
        public string Artikel1 { get; set; }

        [XmlElement("Amount1")]
        public int Menge1 { get; set; }

        [XmlElement("Ust1")]
        public int Ust1 { get; set; }

        [XmlElement("Article2")]
        public string Artikel2 { get; set; }

        [XmlElement("Amount2")]
        public int Menge2 { get; set; }

        [XmlElement("Ust2")]
        public int Ust2 { get; set; }

        [XmlElement("Article3")]
        public string Artikel3 { get; set; }

        [XmlElement("Amount3")]
        public int Menge3 { get; set; }

        [XmlElement("Ust3")]
        public int Ust3 { get; set; }
    }
}