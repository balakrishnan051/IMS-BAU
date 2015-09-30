using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using MbUnit.Framework;
using System.Threading;
using System.Globalization;
using OpenQA.Selenium.Interactions;
using Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Framework
{
    public class Generic
    {
        public static string GetPublic_IPAddress()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return (a4);
        }
        public static DateTime Covert_CurrentISTtoGMT()
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddHours(-4).AddMinutes(-30);
            return dt;
        }

    }//class
}//namespace
