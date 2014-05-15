using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using WebLibrary;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;


namespace Interface
{
    public class Facade : IPlugin
    {
        private NetworkStream stream;
       // private String url;
        //private string[] splitUrl;
        private StreamWriter sw;
        private Dictionary<string, string> splitUrl;
        BusinessLayer bl = new BusinessLayer();

        int id;
        string title;
        string firmname;
        string UID;
        string firstname;
        string lastname;
        string suffix;
        //DateTime birthday;
        string adress;
        string billingadress;
        string deliveryadress;
        public string name;
        public List<Contact> result1;
        public string result2;

        

        public void start()
        {
            Console.WriteLine("Facade loaded");
            
        }

        public void handleRequest(Url url, NetworkStream clientStream)
        {
            stream = clientStream;
            Url newUrl = new Url();
            newUrl = (Url)url;

            StreamWriter sw = new StreamWriter(stream);

            string plug = "Facade";
            newUrl.setPluginName(plug);
            string pluginName = newUrl.getPluginName();
            splitUrl = newUrl.getSplitUrl();

            if (pluginName == "Facade")
            {
                Console.WriteLine("{0}: handleRequest", pluginName);

                #region SearchContact
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("Search"))
                {
                    
                    splitUrl.TryGetValue("name", out name);
   
                    var result = bl.searchContacts(name);

                    foreach (Contact con in result)
                    {
                        //Console.WriteLine("{0} {1}", con.ID, con.Vorname);
                        XElement contacts = new XElement("Contacts", new XElement("Contact", new XElement("ID", con.ID), new XElement("Titel", con.Titel), new XElement("Firstname", con.Vorname), new XElement("Lastname", con.Nachname), new XElement("Suffix", con.Suffix), new XElement("Birthday", con.Geburtsdatum), new XElement("Adress", con.Adresse), new XElement("Deliveryaddress", con.Lieferadresse), new XElement("Billingaddress", con.Rechnungsadresse)));
                        string msg = ToXmlString(contacts);
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("{0}", msg);
                        sw.Flush();
                    
                    }
                }
                #endregion

                #region UpdateContact
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("Update"))
                {
                   
                    string ids;
                    splitUrl.TryGetValue("id", out ids);
                    int.TryParse(ids, out id);
                    
                    splitUrl.TryGetValue("title", out title);
                    splitUrl.TryGetValue("firstname", out firstname);
                    splitUrl.TryGetValue("lastname", out lastname);
                    splitUrl.TryGetValue("suffix", out suffix);
                    //string birthday1;
                    //splitUrl.TryGetValue("birthday", out birthday1);

                    //DateTime birthday = new DateTime();
                    //birthday = DateTime.Parse(birthday1, System.Globalization.CultureInfo.InvariantCulture);

                    splitUrl.TryGetValue("adress", out adress);
                    splitUrl.TryGetValue("billingadress", out billingadress);
                    splitUrl.TryGetValue("deliveryadress", out deliveryadress);

                    Contact instance = new Contact();
                    instance.ID = id;
                    instance.Titel = title;
                    instance.Vorname = firstname;
                    instance.Nachname = lastname;
                    instance.Suffix = suffix;
                    //instance.Geburtsdatum = birthday;
                    instance.Adresse = adress;
                    instance.Rechnungsadresse = billingadress;
                    instance.Lieferadresse = deliveryadress;

                    List<Contact> list = new List<Contact>();
                    list.Add(instance);

                    bl.UpdateContacts(instance);

                    result2 = "Kunde erfolgreich upgedated!";
                    
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("{0}", result2);
                        sw.Flush();
                }
                #endregion

                #region NewContacts
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("New"))
                {

                   
                    splitUrl.TryGetValue("title", out title);
                    splitUrl.TryGetValue("firstname", out firstname);
                    splitUrl.TryGetValue("lastname", out lastname);
                    splitUrl.TryGetValue("suffix", out suffix);
                    string birthday1;
                    splitUrl.TryGetValue("birthday", out birthday1);

                    DateTime birthday = new DateTime();
                    birthday = DateTime.Parse(birthday1, System.Globalization.CultureInfo.InvariantCulture);

                    splitUrl.TryGetValue("adress", out adress);
                    splitUrl.TryGetValue("billingadress", out billingadress);
                    splitUrl.TryGetValue("deliveryadress", out deliveryadress);

                    Contact instance = new Contact();
                    instance.ID = id;
                    instance.Titel = title;
                    instance.Vorname = firstname;
                    instance.Nachname = lastname;
                    instance.Suffix = suffix;
                    instance.Geburtsdatum = birthday;
                    instance.Adresse = adress;
                    instance.Rechnungsadresse = billingadress;
                    instance.Lieferadresse = deliveryadress;

                    List<Contact> list = new List<Contact>();
                    list.Add(instance);

                    bl.NewContacts(instance);

                    result2 = "Kunde erfolgreich hinzugefügt!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("{0}", result2);
                    sw.Flush();
                }
                #endregion

                #region NewContacts
                if (splitUrl.ContainsValue("Firma") && splitUrl.ContainsValue("New"))
                {

                    splitUrl.TryGetValue("firmname", out firmname);
                    splitUrl.TryGetValue("UID", out UID);
                    splitUrl.TryGetValue("adress", out adress);
                    splitUrl.TryGetValue("billingadress", out billingadress);
                    splitUrl.TryGetValue("deliveryadress", out deliveryadress);

                    Contact instance = new Contact();
                    instance.Name = firmname;
                    instance.UID = UID;
                    instance.Adresse = adress;
                    instance.Rechnungsadresse = billingadress;
                    instance.Lieferadresse = deliveryadress;

                    List<Contact> list = new List<Contact>();
                    list.Add(instance);

                    bl.NewFirm(instance);

                    result2 = "Firma erfolgreich hinzugefügt!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("{0}", result2);
                    sw.Flush();
                }
                #endregion


                #region SearchID
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("ID"))
                {
                    int id;
                    string ids;
                    splitUrl.TryGetValue("id", out ids);
                    int.TryParse(ids, out id);

                     result1 = bl.searchID(id);
                   
                    foreach (Contact con in result1)
                    {
                        //Console.WriteLine("{0} {1}", con.ID, con.Vorname);
                        XElement contacts = new XElement("Contacts", new XElement("Contact", new XElement("ID", con.ID), new XElement("Titel", con.Titel), new XElement("Firstname", con.Vorname), new XElement("Lastname", con.Nachname), new XElement("Suffix", con.Suffix), new XElement("Birthday", con.Geburtsdatum), new XElement("Adresse", con.Adresse), new XElement("deliveryaddress", con.Lieferadresse), new XElement("billingaddress", con.Rechnungsadresse)));
                        string msg = ToXmlString(contacts);
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("{0}", msg);
                        sw.Flush();

                    }
                }
                #endregion
            }
        }

        public static string ToXmlString(object obj)
        { 
            if (obj == null) { throw new ArgumentNullException("obj"); } 

            XmlSerializer xml = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            xml.Serialize(new StringWriter(sb), obj);
            return sb.ToString();
        }
       
    }

}
