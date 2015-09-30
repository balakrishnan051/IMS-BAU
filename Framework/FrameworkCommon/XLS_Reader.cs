using System;
using System.Data;
using System.Data.OleDb;

namespace Framework
{
    public class XlsReader
    {
        /// <summary>
        /// Parses the 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable LoadExcelData(string filePath, string sheetName)
        {
            try
            {

                String strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 12.0;";
                //You must use the $ after the object
                //you reference in the spreadsheet

                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM ["+sheetName+"$]", strConn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public static string ParseExcelData(DataTable dt)
        {
            try
            {
                // Insert row index and column Index into dt.Select()[row].ItemArray[Column].ToString();
                string value = dt.Select()[0].ItemArray[0].ToString();
                return value;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public static DataTable LoadConditionalExcelData(string Field, string filePath, string sheetName, string TestID)
        {
            try
            {
                String strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 12.0;";
                //You must use the $ after the object
                //you reference in the spreadsheet

                var da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "$] WHERE " + Field + " in ('" + TestID + "')", strConn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

    }
}