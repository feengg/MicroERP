using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using System.Reflection;


namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Starting in: \n " + System.Environment.CurrentDirectory);
            //Console.WriteLine("--------------------------------------------------------------------------------");
            PluginManager newPlugin = new PluginManager();
            Server neuerServer = new Server(ref newPlugin);
            Console.WriteLine("Server started...");
        }
    }
}
