using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SikuliModule;
using Framework;
namespace ICE.Sikuli
{
    public class ImageAction
    {
        public static void Click(string path, string failMsg,int WaitTime=0, int RedotimeOut = 0)
        {


            bool returnValue = false;
            WaitForImage(path, failMsg, WaitTime);
            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(RedotimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.Click(path);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }
                
            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public static void isExist(string path, string failMsg, int RedotimeOut = 0)
        {


            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(RedotimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.FindAll(path);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public static void RightClick(string path, string failMsg, int RedotimeOut = 0)
        {


            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(RedotimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.RightClick(path);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public static void DragAndDrop(string FromPath, string ToPath, string failMsg, int RedotimeOut = 0)
        {


            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(RedotimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.DragAndDrop(FromPath,ToPath);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        public static void WaitForImage(string path, string failMsg, int WaitTimeOut = 0)
        {


            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(WaitTimeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.FindAll(path);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }


        #region NotWorking
        public static void Type(string path,string input, string failMsg, int timeOut = 0)
        {

            string exceptionMsg=null;
            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.Type(path, input);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { exceptionMsg = e.Message.ToString(); }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg + "\n" + exceptionMsg);
        }
        public static void Hover(string path, string failMsg, int timeOut = 0)
        {


            bool returnValue = false;

            DateTime varDateTime;
            DateTime varElapseTime = DateTime.Now.AddSeconds(timeOut);
            do
            {
                varDateTime = DateTime.Now;
                try
                {

                    SikuliAction.Hover(path);
                    returnValue = true;
                    break;

                }
                catch (Exception e) { }

            } while (varDateTime <= varElapseTime);


            BaseTest.Assert.IsTrue(returnValue, failMsg);
        }
        #endregion
    }
}
