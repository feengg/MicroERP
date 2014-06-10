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
        #region Variablen

        private NetworkStream stream;
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
        public string result2;

        #endregion

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

                //Contacts

                #region SearchContact
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("Search"))
                {
                    
                    splitUrl.TryGetValue("name", out name);
   
                    var result = bl.searchContacts(name);

                    string msg = ToXmlString(result);

                    Console.WriteLine(msg);
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", msg);
                    sw.Flush();

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

                    ContactsList list = new ContactsList();
                    list.contact.Add(instance);

                    bl.UpdateContacts(list);

                    result2 = "Kunde erfolgreich upgedated!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
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

                    ContactsList list = new ContactsList();
                    list.contact.Add(instance);

                    bl.NewContacts(list);

                    result2 = "Kunde erfolgreich hinzugefügt!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
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

                    var result = bl.searchID(id);

                    string msg = ToXmlString(result);

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", msg);
                    sw.Flush();

                }
                #endregion

                //Firma

                #region SearchFirm
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("SearchFirm"))
                {

                    splitUrl.TryGetValue("name", out name);

                    var result = bl.searchFirm(name);

                    string msg = ToXmlString(result);

                    Console.WriteLine(msg);
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", msg);
                    sw.Flush();

                }
                #endregion

                #region NewFirm
                if (splitUrl.ContainsValue("Firma") && splitUrl.ContainsValue("New"))
                {

                    splitUrl.TryGetValue("Name", out firmname);
                    splitUrl.TryGetValue("UID", out UID);
                    splitUrl.TryGetValue("adress", out adress);
                    splitUrl.TryGetValue("deliveryaddress", out deliveryadress);
                    splitUrl.TryGetValue("billingaddress", out billingadress);

                    
                    Firma firm = new Firma();

                    firm.UID = UID;
                    firm.Name = firmname;
                    firm.Adresse = adress;
                    firm.Rechnungsadresse = billingadress;
                    firm.Lieferadresse = deliveryadress;

                    Firmlist list = new Firmlist();

                    list.firma.Add(firm);

                    bl.NewFirm(list);

                    result2 = "Firma erfolgreich hinzugefügt!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", result2);
                    sw.Flush();
                }
                #endregion

                #region Search Firm ID
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("FirmID"))
                {
                    int id;
                    string ids;
                    splitUrl.TryGetValue("id", out ids);
                    int.TryParse(ids, out id);

                    var result = bl.searchFirmID(id);

                    string msg = ToXmlString(result);

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", msg);
                    sw.Flush();
                }
                #endregion

                #region UpdateFirm
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("UpdateFirm"))
                {

                    string ids;
                    splitUrl.TryGetValue("id", out ids);
                    int.TryParse(ids, out id);

                    splitUrl.TryGetValue("name", out firmname);
                    splitUrl.TryGetValue("UID", out UID);
                    splitUrl.TryGetValue("adress", out adress);
                    splitUrl.TryGetValue("billingadress", out billingadress);
                    splitUrl.TryGetValue("deliveryadress", out deliveryadress);

                    Firma instance = new Firma();
                    instance.ID = id;
                    instance.Name = firmname;
                    instance.UID = UID;
                    instance.Adresse = adress;
                    instance.Rechnungsadresse = billingadress;
                    instance.Lieferadresse = deliveryadress;

                    Firmlist list = new Firmlist();
                    list.firma.Add(instance);

                    bl.UpdateFirm(list);

                    result2 = "Firma erfolgreich upgedated!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", result2);
                    sw.Flush();
                }
                #endregion

                //Invoice


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
