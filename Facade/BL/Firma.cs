using System.Collections.Generic;
using System.Xml.Serialization;

namespace Interface
{
    /* Firma */

    [XmlRoot("Firms")]
    public class Firmlist
    {
        [XmlElement("Firm")]
        public List<Firma> firma { get; set; }

        public Firmlist()
        {
            this.firma = new List<Firma>();
        }
    }

    public class Firma
    {
        [XmlElement("ID")]
        public int ID { get; set; }

        [XmlElement("UID")]
        public string UID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Adress")]
        public string Adresse { get; set; }

        [XmlElement("deliveryaddress")]
        public string Lieferadresse { get; set; }

        [XmlElement("billingaddress")]
        public string Rechnungsadresse { get; set; }
    }
}