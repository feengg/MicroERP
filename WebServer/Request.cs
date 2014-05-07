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
using System.Collections;
using WebLibrary;
using System.Web;


namespace WebServer
{

    public class Request
    {
        private String http_method;
        protected String http_url;
        private NetworkStream stream;
        private StreamReader sr;
        //private String[,] httpHeaders;
        public Hashtable httpHeaders = new Hashtable();
        //public string[] SplitUrl;
        public Url theUrl = new Url();
        public bool favicon;
        private Dictionary<string, string> SplitUrl = new Dictionary<string, string>();


        //für Test ~*
        public Request(String httpURL)
        {
            http_url = httpURL;
            http_method = "GET";
            handleGETRequest();
        }

        //~*

        //get für URL Objekt
        public object getURL()
        {
            return theUrl;
        }

        public Request(object clientStream)
        {
            stream = (NetworkStream)clientStream;
            sr = new StreamReader(stream);

            string data = sr.ReadLine();
            parseRequest(data);
            readHeaders();
            if (http_method == "GET")
            {
                handleGETRequest();
            }
            //else if (http_method == "POST")
            //{
            //    handlePOSTRequest();
            //}
        }


        #region parseRequest
        public void parseRequest(string data)
        {
            String[] tokens = data.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            String new_http_url = http_url;
            Console.WriteLine("Received {0}", http_url);
        }
        #endregion

        #region readHeaders
        private void readHeaders()
        {
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    Console.WriteLine("Got headers");
                    return;
                }
                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;

                //Leerzeichen beseitigen
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                httpHeaders[name] = value;
                //Console.WriteLine("header: {0}:{1}", name, value);
            }
        }
        #endregion

        #region GetRequest
        public void handleGETRequest()
        {

            //Console.WriteLine("GET");
            http_url = http_url.Substring(1);
            //string[] split = Regex.Split(http_url, "/");
            string[] split = http_url.Split(new Char[] { '/', '?' });
            //böses favicon
            favicon = http_url.StartsWith("favicon.ico", System.StringComparison.CurrentCultureIgnoreCase);
            if (favicon == false)
            {
                theUrl.setFullUrl(http_url);
                theUrl.setPluginName(split[0]);

                SplitUrl.Add("Table", split[0]);
                SplitUrl.Add("Function", split[1]);
                


                int markIndex = http_url.IndexOf('?');
                string function = http_url.Substring(2);

                if (markIndex != -1 && markIndex + 1 < http_url.Length)
                {
                    string paramenterString = http_url.Substring(markIndex + 1);
                    var parameters = HttpUtility.ParseQueryString(paramenterString);

                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            SplitUrl.Add(key, parameters[key]);
                        }
                    }

                }
                theUrl.setSplitUrl(SplitUrl);


                Console.WriteLine("Got this:");

                foreach (KeyValuePair<string, string> o in SplitUrl)
                {
                    Console.WriteLine(o);
                }
            }
            else
            {
                Console.WriteLine("Favicon!");
                //throw new System.ArgumentException("Favicon!", http_url);
            }

        }
        #endregion

                #region handleRequest
        //        private void handleRequests(string type)
        //        {

        //            string postname;
        //            //PluginNamen raussuchen
        //            postname = Convert.ToString(this.httpHeaders["Referer"]);
        //            int index = postname.LastIndexOf("/");
        //            if (index > 0)
        //                postname = postname.Substring(index + 1);
        //            theUrl.setPluginName(postname);

        //            //schneidet alle = und & weg
        //            string[] split2 = type.Split(new Char[] { '=', '&' });

        //            //for (int j = 0; j < split2.Length; j++)
        //            //{
        //            //    SplitUrl.Add(split2[j], split2[j + 1]);
        //            //}
        //            ////und setzen die einzelnen Werte in das URL Objekt
        //            //theUrl.setSplitUrl(SplitUrl);

        //            ////dann basteln wir eine neue URL
        //            //StringBuilder builder = new StringBuilder();
        //            //foreach (KeyValuePair<string, string> value in SplitUrl)
        //            //{
        //            //    builder.Append(value);
        //            //    builder.Append('/');
        //            //}
        //            SplitUrl = new string[(split2.Length / 2) + 1];
        //            int k = 1;
        //            SplitUrl[0] = (string)postname.Clone();
        //            //weil wir ja nur die Werte wollen, nehmen wir jedes 2. Element
        //            for (int j = 1; j < split2.Length; j += 2)
        //            {
        //                string tempstring;
        //                tempstring = Convert.ToString(split2[j]);
        //                SplitUrl[k] = (string)tempstring.Clone();
        //                k++;
        //            }
        //            //und setzen die einzelnen Werte in das URL Objekt
        //            theUrl.setSplitUrl(SplitUrl);

        //            //dann basteln wir eine neue URL
        //            StringBuilder builder = new StringBuilder();
        //            foreach (string value in SplitUrl)
        //            {
        //                builder.Append(value);
        //                builder.Append('/');
        //            }
        //            string url = builder.ToString();
        //            //setzen die in den String
        //            theUrl.setFullUrl(url);

        //            Console.WriteLine("Got this:");

        //            foreach (string o in SplitUrl)
        //            {
        //                Console.WriteLine(o);
        //            }

        //            //foreach (KeyValuePair<string, string> o in SplitUrl)
        //            //{
        //            //    Console.WriteLine(o);
        //            //}
        //        }
        //        #endregion


        //        #region PostRequest
        //        private const int BUF_SIZE = 4096;
        //        public void handlePOSTRequest()
        //        {
        //            Console.WriteLine("POST");
        //            int content_len = 0;
        //            //string content_type;
        //            MemoryStream ms = new MemoryStream();
        //            if (this.httpHeaders.ContainsKey("Content-Length"))
        //            {
        //                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
        //                byte[] buf = null;
        //                int to_read = content_len;
        //                while (to_read > 0)
        //                {
        //                    Console.WriteLine("starting Read, to_read={0}", to_read);
        //                    int lengthToRead = Math.Min(BUF_SIZE, to_read);

        //                    // Das unterstützt KEINE binärdaten (von Hr. Zaczek freigegeben, nachdem es eine Belehrung über diesen pfusch gegeben hat)
        //                    var charBuffer = new char[lengthToRead];
        //                    int numread = this.sr.Read(charBuffer, 0, lengthToRead);
        //                    buf = charBuffer.Take(numread).Select(c => (byte)c).ToArray();
        //                    Console.WriteLine("read finished, numread={0}", numread);
        //                    var x_www_form_urlencoded = new string(charBuffer);

        //                    if (numread == 0)
        //                    {
        //                        if (to_read == 0)
        //                        {
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("client disconnected during post");
        //                        }
        //                    }
        //                    to_read -= numread;
        //                    ms.Write(buf, 0, numread);

        //                    //PARSEN mal wieder T_T
        //                    string type = Convert.ToString(x_www_form_urlencoded);
        //                    handleRequests(type);

        //                }
        //                ms.Seek(0, SeekOrigin.Begin);
        //            }
        //            Console.WriteLine("get post data end");
        //        }


        //    }
               #endregion
    }
}
