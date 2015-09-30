using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Framework
{
    public class XMLReader
    {
        public string ReadBy_Tag_And_AttributeValue(string path, string TagName, string _attribute, string _attributeVal, string returnAttribute)
        {
            XmlTextReader xmlFile = new XmlTextReader(path);
            Boolean flag = false;
            string attributeValue = null;
            while (xmlFile.Read())
            {
                if (flag) break;
                switch (xmlFile.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (xmlFile.Name == TagName)
                            for (int i = 1; i <= xmlFile.AttributeCount; i++)
                                if (xmlFile.GetAttribute(_attribute) == _attributeVal)
                                    attributeValue = xmlFile.GetAttribute(returnAttribute);
                                else
                                    xmlFile.MoveToNextAttribute();


                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(xmlFile.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        break;
                }

            }
            xmlFile.Close();
            return attributeValue;
        }
        public static string ReadXMLQuery_ByKeyValue(string path, string TagName, string _attributeVal)
        {
            try
            {
                XDocument xDoc = XDocument.Load(path);
                var pairs = XDocument.Parse(xDoc.ToString())
                          .Descendants(TagName)
                          .Select(x => new
                          {
                              Key = x.Attribute("key").Value,
                              Value = x.Attribute("value").Value

                          })
                         .Where(addr => (string)addr.Key.ToString() == _attributeVal)

                          .ToDictionary(item => item.Key, item2 => item2.Value);

                string val = null;
                val = pairs[_attributeVal];
                return val;
            }
            catch (Exception e) { BaseTest.Fail("Issue while reading :" + _attributeVal + " from XML OR files"); }
            return null;

        }

        //For Code
        public string ReadLocatorValue(string ControlFile, string locatorType, string locatorName)
        {
            
            return ReadBy_Tag_And_AttributeValue(ControlFile, locatorType, "name", locatorName, "value");

        }

    }
}
