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
    public class StringCommonMethods
    {

        public string ReadSubString(string input, string after, string before)
        {
            if (!input.ToString().Contains(after))
                return null;

            int i = input.ToString().LastIndexOf(before);
            int j = input.ToString().LastIndexOf(after);
            int k = input.Length;
            string value = input.ToString().Substring(j + after.Length, i - (j + after.Length));

            return value;


        }
        public string ReadSubString(string input, string after)
        {
            if (!input.ToString().Contains(after))
                return null;

            int j = input.ToString().LastIndexOf(after);
            int k = input.Length;
            string value = input.ToString().Substring(j + after.Length);

            return value;


        }
        public string ReadSubStringBefore(string input, string before)
        {
            if (!input.ToString().Contains(before))
                return null;

            int j = input.ToString().LastIndexOf(before);
            //int k = input.Length;
            string value = input.ToString().Substring(0 , j);

            return value;


        }


        public static string GenerateAlphabeticGUID()
        {
            DateTime dateFormat = DateTime.Now;
            string dateTime = (dateFormat.ToString("yyyyMMddHHmmss"));
            return Convert2NumericToAlpha(Int64.Parse(dateTime));
        }
        private static string Convert2NumericToAlpha(Int64 intValue)
        {
            string value = intValue.ToString();

            string[] split = new string[value.Length / 2 + (value.Length % 2 == 0 ? 0 : 1)];
            string finalStr = "";
            for (int i = 0; i < (value.Length / 2) + (value.Length % 2 == 0 ? 0 : 1); i++)
            {
                split[i] = value.Substring(i * 2, i * 2 + 2 > value.Length ? 1 : 2);
                finalStr = finalStr + GetColumnName(int.Parse(split[i]));
            }
            return finalStr.ToString();
        }
        private static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        public static double ReadIntegerfromString(string value)
        {
            try
            {
                string bval = value.Replace(",", string.Empty).Trim();
                Regex re = new Regex(@"\d+");
                Match b4Value = re.Match(bval);
                return int.Parse(b4Value.Value);
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public static decimal ReadDecimalfromString(string value)
        {
            try
            {
                Char[] strarr = value.ToCharArray().Where(c => Char.IsDigit(c) || Char.IsPunctuation(c)).ToArray();
                decimal number = Convert.ToDecimal(new string(strarr));
                return number;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public static double ReadDoublefromString(string value)
        {
            try
            {
                Char[] strarr = value.ToCharArray().Where(c => Char.IsDigit(c) || Char.IsPunctuation(c)).ToArray();
                decimal number = Convert.ToDecimal(new string(strarr));
                return double.Parse(number.ToString());
            }
            catch (Exception e)
            {
                return -1;
            }
        }

    }//class
}//namespace
