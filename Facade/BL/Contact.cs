using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Interface
{

    [XmlRoot("Contacts")] 
    public class ContactsList { 

        [XmlElement("Contact")] 
        public List<Contact> contact { get; set; }

        public ContactsList()
        {
            this.contact = new List<Contact>();
        }
    }
    public class Contact
    {

        [XmlElement("ID")] 
        public int ID { get; set; } 
        [XmlElement("Titel")] 
        public string Titel { get; set; } 
        [XmlElement("Firstname")] 
        public string Vorname { get; set; } 
        [XmlElement("Lastname")] 
        public string Nachname { get; set; } 
        [XmlElement("Suffix")]
        public string Suffix { get; set; }
        [XmlElement("Birthday")]
        public DateTime Geburtsdatum { get; set; } 
        [XmlElement("Adress")] 
        public string Adresse { get; set; } 
        [XmlElement("deliveryaddress")] 
        public string Lieferadresse { get; set; }
        [XmlElement("billingaddress")] 
        public string Rechnungsadresse { get; set; } }

    }



