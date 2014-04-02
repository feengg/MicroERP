using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using WebLibrary;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WebServer
{
    public class Response
    {
        public StreamWriter sw;
        private NetworkStream stream;


        public Response(object clientStream, object theUrl)
        {
            stream = (NetworkStream)clientStream;
            StreamWriter sw = new StreamWriter(stream);
            Url newUrl = new Url();
            newUrl = (Url)theUrl;
            string pluginName = newUrl.getPluginName();
            string http_url = newUrl.getFullUrl();


            //sw.WriteLine("HTTP/1.1 200 OK");
            //sw.WriteLine("connection: close");
            //sw.WriteLine("content-type: text/html");
            //sw.WriteLine();
            //sw.WriteLine("<html><head><title>Webserver</title>");
            //sw.WriteLine("<style type='text/css'>*{margin:0px; padding: 0px;}");
            //sw.WriteLine("body {font-family: Arial;background-color: #E6E6E6;}");
            //sw.WriteLine("#content {padding-top:30px;}");
            //sw.WriteLine("#Navi {padding-top:30px;padding-bottom:30px;background-color: #CC3333;}");
            //sw.WriteLine("h1  {font-family: Hobo std;font-size: 40px;  font-weight: bold; color: #CC3333;padding-top: 20px;padding-left: 50px; padding-top:50px;}");
            //sw.WriteLine("h4 {font-family: Verdana;font-size: 25px;  color: #DF0101;font-weight: bold;padding-bottom: 30px;padding-left: 50px;}");
            //sw.WriteLine("p {font-family: Arial;color: #330000;padding-left: 50px;}");
            //sw.WriteLine("ul {padding-left: 50px;}");
            //sw.WriteLine("a {color:#330000;font-weight: bold;}");
            //sw.WriteLine("ul li {list-style-type: none;}");
            //sw.WriteLine("form {padding-left: 50px;}");
            //sw.WriteLine("</style></head>");
            //sw.WriteLine("<html><body><h1>WebServer</h1>");
            //sw.Flush();


            //XElement contacts = new XElement("Contacts", new XElement("Contact", new XElement("Name", "Patrick Hines"), new XElement("Phone", "206-555-0144"), new XElement("Address", new XElement("Street1", "123 Main St"), new XElement("City", "Mercer Island"), new XElement("State", "WA"), new XElement("Postal", "68042") ) ) ); 

            //string msg = ToXmlString(contacts);
            //sw.WriteLine("HTTP/1.1 200 OK");
            //sw.WriteLine("connection: close");
            //sw.WriteLine("content-type: text/html");
            //sw.WriteLine();
            //sw.WriteLine("{0}", msg);
            //sw.Flush();
           }
            //    public string ToXmlString(object obj) { //if (obj == null) { throw new ArgumentNullException("obj"); } 

            //    XmlSerializer xml = new XmlSerializer(obj.GetType());
            //    StringBuilder sb = new StringBuilder();
            //    xml.Serialize(new StringWriter(sb), obj);
            //    return sb.ToString(); }
        }
                    
}

