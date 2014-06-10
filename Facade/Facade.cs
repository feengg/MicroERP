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
        //Rechung
        DateTime paymentDate;
        int idContact;
        string number;
        string comment;
        string message;
        string article1;
        int amount1;
        int Ust1;
        int stk1;
        string article2;
        int amount2;
        int Ust2;
        int stk2;
        string article3;
        int amount3;
        int Ust3;
        int stk3;

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
                    
                    splitUrl.TryGetValue("Name", out name);
   
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
                    splitUrl.TryGetValue("Id", out ids);
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
                    splitUrl.TryGetValue("billingaddress", out billingadress);
                    splitUrl.TryGetValue("deliveryaddress", out deliveryadress);

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
                    splitUrl.TryGetValue("Title", out title);
                    splitUrl.TryGetValue("firstname", out firstname);
                    splitUrl.TryGetValue("lastname", out lastname);
                    splitUrl.TryGetValue("suffix", out suffix);
                    string birthday1;
                    splitUrl.TryGetValue("birthday", out birthday1);

                    DateTime birthday = new DateTime();
                    birthday = DateTime.Parse(birthday1, System.Globalization.CultureInfo.InvariantCulture);

                    splitUrl.TryGetValue("address", out adress);
                    splitUrl.TryGetValue("deliveryaddress", out billingadress);
                    splitUrl.TryGetValue("billingaddress", out deliveryadress);

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
                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("Id"))
                {
                    int id;
                    string ids;
                    splitUrl.TryGetValue("Id", out ids);
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
                if (splitUrl.ContainsValue("Firm") && splitUrl.ContainsValue("Search"))
                {

                    splitUrl.TryGetValue("Name", out name);

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
                if (splitUrl.ContainsValue("Firm") && splitUrl.ContainsValue("New"))
                {

                    splitUrl.TryGetValue("Name", out firmname);
                    splitUrl.TryGetValue("Uid", out UID);
                    splitUrl.TryGetValue("address", out adress);
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
                if (splitUrl.ContainsValue("Firm") && splitUrl.ContainsValue("Id"))
                {
                    int id;
                    string ids;
                    splitUrl.TryGetValue("Id", out ids);
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
                if (splitUrl.ContainsValue("Firm") && splitUrl.ContainsValue("Update"))
                {

                    string ids;
                    splitUrl.TryGetValue("Id", out ids);
                    int.TryParse(ids, out id);

                    splitUrl.TryGetValue("name", out firmname);
                    splitUrl.TryGetValue("Uid", out UID);
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

                #region Search Invoice ID
                if (splitUrl.ContainsValue("Invoice") && splitUrl.ContainsValue("ID"))
                {
                    int id;
                    string ids;
                    splitUrl.TryGetValue("id", out ids);
                    int.TryParse(ids, out id);

                    var result = bl.searchIDInvoice(id);

                    string msg = ToXmlString(result);

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", msg);
                    sw.Flush();
                }
                #endregion

                #region NewInvoice
                if (splitUrl.ContainsValue("Invoice") && splitUrl.ContainsValue("New"))
                {
                    splitUrl.TryGetValue("Number", out number);

                    string PayDate1;
                    splitUrl.TryGetValue("PayDate", out PayDate1);

                    DateTime paymentDate = new DateTime();
                    paymentDate = DateTime.Parse(PayDate1, System.Globalization.CultureInfo.InvariantCulture);

                    string ids;
                    splitUrl.TryGetValue("ID", out ids);
                    int.TryParse(ids, out idContact);
                    
                    splitUrl.TryGetValue("Message", out message);
                    splitUrl.TryGetValue("Comment", out comment);
                    string A1;
                    splitUrl.TryGetValue("A1", out A1);
                    int.TryParse(A1, out amount1);
                    splitUrl.TryGetValue("Art1", out article1);
                    string P1;
                    splitUrl.TryGetValue("P1", out P1);
                    int.TryParse(P1, out stk1);
                    string U1;
                    splitUrl.TryGetValue("U1", out U1);
                    int.TryParse(U1, out Ust1);

                    string A2;
                    splitUrl.TryGetValue("A2", out A2);
                    int.TryParse(A2, out amount2);
                    splitUrl.TryGetValue("Art2", out article2);
                    string P2;
                    splitUrl.TryGetValue("P2", out P2);
                    int.TryParse(P2, out stk2);
                    string U2;
                    splitUrl.TryGetValue("U2", out U2);
                    int.TryParse(U2, out Ust2);

                    string A3;
                    splitUrl.TryGetValue("A3", out A3);
                    int.TryParse(A3, out amount3);
                    splitUrl.TryGetValue("Art3", out article3);
                    string P3;
                    splitUrl.TryGetValue("P3", out P3);
                    int.TryParse(P3, out stk3);
                    string U3;
                    splitUrl.TryGetValue("U3", out U3);
                    int.TryParse(U3, out Ust3);


                    Invoice invoice = new Invoice();
                    invoice.Nummer = number;
                    invoice.Datum = paymentDate;
                    invoice.IDKontakt = idContact;
                    invoice.Nachricht = message;
                    invoice.Kommentar = comment;
                    invoice.Menge1 = amount1;
                    invoice.Artikel1 = article1;
                    invoice.Stueckpreis1 = stk1;
                    invoice.Ust1 = Ust1;
                    invoice.Menge2 = amount2;
                    invoice.Artikel2 = article2;
                    invoice.Stueckpreis2 = stk2;
                    invoice.Ust2 = Ust2;
                    invoice.Menge3 = amount3;
                    invoice.Artikel3 = article3;
                    invoice.Stueckpreis3 = stk3;
                    invoice.Ust3 = Ust3;

                    InvoiceList list = new InvoiceList();
                    list.invoice.Add(invoice);

                    bl.NewInvoice(list);

                    result2 = "Rechnung erfolgreich hinzugefügt!";

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    sw.WriteLine("content-type: text/html; charset=utf-8");
                    sw.WriteLine();
                    sw.WriteLine("{0}", result2);
                    sw.Flush();
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
