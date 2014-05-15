using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface;
using WebServer;
using WebLibrary;

namespace UnitTest
{
    [TestClass]
    public class SearchContact
    {
        [TestMethod]
        public void Search()
        {
            string data = "GET /Contacts/Search?name=null HTTP/1.1";
            Request rq = new Request(data);
           
            string expected = null;
            Facade facade = new Facade();

            string actual = facade.name;
            Assert.AreEqual(expected, actual, "Name richtig");
                 

        }
    }
}