using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface;
using WebServer;
using WebLibrary;

namespace UnitTest
{
    [TestClass]
    public class ID
    {
        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ID_right()
        {
            string data = "GET /Contacts/ID?id=3 HTTP/1.1";
            Request rq = new Request(data);

            int expected = 0;
            Facade facade = new Facade();
            var list = facade.result1;
            foreach (Contact i in list)
            {
                int actual = i.ID;
                Assert.AreEqual(expected, actual, "ID richtig");
            }
            


        }
    }
}
