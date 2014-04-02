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

                if (splitUrl.ContainsValue("Contacts") && splitUrl.ContainsValue("Search"))
                {
                    string name;
                    splitUrl.TryGetValue("name", out name);
   
                    var result = bl.searchContacts(name);

                    foreach (Contact con in result)
                    {
                        //Console.WriteLine("{0} {1}", con.ID, con.Vorname);
                        XElement contacts = new XElement("Contacts", new XElement("Contact", new XElement("ID", con.ID), new XElement("Firstname", con.Vorname), new XElement("Lastname", con.Nachname), new XElement("Suffix", con.Suffix), new XElement("Birthday", con.Geburtsdatum), new XElement("Address", new XElement("Adresse", con.Adresse), new XElement("deliveryaddress", con.Lieferadresse), new XElement("billingaddress", con.Rechnungsadresse))));
                        string msg = ToXmlString(contacts);
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("{0}", msg);
                        sw.Flush();
                    
                    }

                    //resp.Send(result.ToXmlString(), "text/xml");

                }
            }
        }

        public static string ToXmlString(object obj)
        { //if (obj == null) { throw new ArgumentNullException("obj"); } 

            XmlSerializer xml = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            xml.Serialize(new StringWriter(sb), obj);
            return sb.ToString();
        }
       







    }

}
