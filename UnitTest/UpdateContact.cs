//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Interface;
//using WebServer;
//using WebLibrary;

//namespace UnitTest
//{
//    [TestClass]
//    public class UpdateContact
//    {
//        [TestMethod]
//        public void Update()
//        {
//            string data = "GET /Contacts/ID?id=3 HTTP/1.1";
//            Request rq = new Request(data);

//            string expected = null;
//            Facade facade = new Facade();
//            string actual = facade.result2;
//            Assert.AreEqual(expected, actual, "ID richtig");
            
//        }
//    }
//}
