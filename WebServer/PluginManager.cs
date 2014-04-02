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
using Interface;
using System.Reflection;
using WebLibrary;

namespace WebServer
{
    class PluginManager
    {
        //Plugins
        List<IPlugin> plugins = new List<IPlugin>();

        public PluginManager()
        {
            //In der Schleife wird der Ordner mit den dlls durchsucht
            foreach (var filename in System.IO.Directory.GetFiles(".\\plugins", "*.dll"))
            {
                Assembly myDll = Assembly.LoadFrom(filename);
                foreach (Type asmtype in myDll.GetTypes())
                {
                    if (asmtype.GetInterface("IPlugin") != null)
                    {
                        IPlugin plugs = (IPlugin)Activator.CreateInstance(asmtype);
                        plugins.Add(plugs);
                    }
                }
            }
            foreach (var plugin in plugins)
            {
                plugin.start();
            }
        }

        public void HandleRequest(Url url, NetworkStream stream)
        {
            foreach (IPlugin p in plugins)
            {
                p.handleRequest(url, stream);
            }
        }
    }
}
