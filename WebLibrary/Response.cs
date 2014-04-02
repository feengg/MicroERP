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

namespace WebLibrary
{
   public class Response
    {
       private NetworkStream stream;
       private string pluginName;
       private string http_url;

        public Response(object clientStream) //object clientStream, object theUrl
        {
            stream = (NetworkStream)clientStream;
            
        }
            public void HandleRequest(Url url)
            {
            Url newUrl = new Url();
            newUrl = (Url)url;
            pluginName = newUrl.getPluginName();
            http_url = newUrl.getFullUrl();
            StreamWriter sw = new StreamWriter(stream);

                if (String.Compare(pluginName, "getTemperature") == 0)
                {

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    //sw.WriteLine("content-length: 11");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("<html><body><h1>WebServer - SensorCloud</h1>");
                    sw.WriteLine("url : {0}", http_url);
                    sw.WriteLine("<form method=post action=/form>");
                    sw.WriteLine("<input type=text name=date value=Insert Date>");
                    sw.WriteLine("<input type=submit value=los>");
                    sw.WriteLine("</form>");
                    sw.WriteLine("</body></html>");
                    sw.Flush();
                }
                else if (String.Compare(pluginName, "StaticFile") == 0)
                {

                    string path = @".\" + filename;

                    if (System.IO.File.Exists(path) == true)
                    {
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        //sw.WriteLine("content-length: 11");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("<html><body><h1>WebServer - StaticFile</h1>");
                        //sw.WriteLine("<img src="path">");
                        sw.WriteLine("<br>Usage: '/StaticFile/Filename'");
                        sw.WriteLine("</body></html>");
                        sw.Flush();
                        Console.WriteLine("true");
                    }

                    else
                    {
                        Console.WriteLine("false");
                        //url.parsePath("http://127.0.0.1/StaticFile/index.html");
                    }

                    //sw.WriteLine("HTTP/1.1 200 OK");
                    //sw.WriteLine("connection: close");
                    ////sw.WriteLine("content-length: 11");
                    //sw.WriteLine("content-type: text/html");
                    //sw.WriteLine();
                    //sw.WriteLine("<html><body><h1>WebServer - StaticFile</h1>");
                    //sw.WriteLine("url : {0}", http_url);
                    //sw.WriteLine("<br>Usage: '/StaticFile/Filename'");
                    //sw.WriteLine("</body></html>");
                    //sw.Flush();
                }
                else if (String.Compare(pluginName, "Navi") == 0)
                {
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    //sw.WriteLine("content-length: 11");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("<html><body><h1>WebServer - Navi</h1>");
                    sw.WriteLine("url : {0}", http_url);
                    sw.WriteLine("<br>Not implemented");
                    sw.WriteLine("</body></html>");
                    sw.Flush();
                }
                else if (String.Compare(pluginName, "Eigen") == 0)
                {
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    //sw.WriteLine("content-length: 11");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("<html><body><h1>WebServer - Eigenes Plugin</h1>");
                    sw.WriteLine("url : {0}", http_url);
                    sw.WriteLine("<br>Not implemented");
                    sw.WriteLine("</body></html>");
                    sw.Flush();
                }
                else
                {
                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("connection: close");
                    //sw.WriteLine("content-length: 11");
                    sw.WriteLine("content-type: text/html");
                    sw.WriteLine();
                    sw.WriteLine("<html><body><h1>WebServer</h1>");
                    sw.WriteLine("url : {0}", http_url);
                    sw.WriteLine("<form method=post action=/form>");
                    sw.WriteLine("<input type=text name=foo value=foovalue>");
                    sw.WriteLine("<input type=submit name=bar value=los>");
                    sw.WriteLine("</form>");
                    sw.WriteLine("<ul>");
                    sw.WriteLine("<li><a href='http://localhost:8080/getTemperature'>SensorCloud</a></li>");
                    sw.WriteLine("<li><a href='http://localhost:8080/StaticFile'>StaticFile</a></li>");
                    sw.WriteLine("<li><a href='http://localhost:8080/Navi'>Navi</a></li>");
                    sw.WriteLine("<li><a href='http://localhost:8080/Eigen'>Eigenes Plugin</a></li>");
                    sw.WriteLine("</ul></body></html>");
                    sw.Flush();
                }
            }

    }
}
