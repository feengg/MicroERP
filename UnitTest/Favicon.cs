using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;

namespace UnitTest
{
    [TestClass]
    public class Favicon
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void handleGETRequest_Favicon()
        {
            String http_url = "GET /favicon.ico HTTP/1.1";
            bool expected = true;

            Request newRequest = new Request(http_url);
            newRequest.handleGETRequest();

            bool actual = newRequest.favicon;
            Assert.AreEqual(expected, actual, "Stimmt");
        }
    }
}