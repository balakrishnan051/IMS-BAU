using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Framework.Common
{
    public class ChangeSystemTime
    {

        //********************************************************************
        /// <summary> This structure represents a date and time. </summary>
        public struct SYSTEMTIME
        {
            public ushort wYear, wMonth, wDayOfWeek, wDay,
               wHour, wMinute, wSecond, wMilliseconds;
        }
        //*********************************************************************
        /// <summary>
        /// The code below retrieves the current system date and time expressed in Coordinated Universal Time (UTC).
        /// </summary>
        /// <param name="lpSystemTime">[out] Pointer to a SYSTEMTIME structure to
        /// receive the current system date and time.</param>
        [DllImport("kernel32.dll")]
        public extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("kernel32.dll")]
        public extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);
        SYSTEMTIME st = new SYSTEMTIME();
        public void ChangeSystemDate(int days, bool resetTime)
        {
            if (resetTime == false)
            {
                GetSystemTime(ref st);
                // st.wDay = (ushort)(st.wDay + Convert.ToInt32(days));
                st.wDay = (ushort)(st.wDay + days);
                SetSystemTime(ref st);
            }

            if (resetTime == true)
            {
                st.wDay = (ushort)(st.wDay - days);
                SetSystemTime(ref st);
            }

        }
    }
}
