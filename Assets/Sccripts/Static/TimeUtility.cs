
using System;
using UnityEngine;

namespace GameUtil
{
    public enum TimeConversion
    {
        Year2Day = 365,
        Year2Huor = 8766,
        Year2Minute = 525960,
        Year2Second = 31557600,
        Day2Huor = 24,
        Day2Minute = 1440,
        Day2Second = 86400,
        Huor2Minute = 60,
        Huor2Second = 3600,
        Minute2Second = 60,
    }

    /// <summary>
    /// 时间工具类
    /// </summary>
    public static class TimeUtility
    {
        #region 获取时间戳间隔
        /// <summary>
        /// 获取两个时间戳时间间隔（单位秒）
        /// </summary>
        /// <param name="timestapMax">时间戳1(10位-秒级)</param>
        /// <param name="timestapMin">时间戳2(10位-秒级)</param>
        /// <returns>返回的结果为四舍五入的</returns>
        public static int GetTwoTimeStampInterval_1(long timestapMax, long timestapMin)
        {
            int resultTime = Mathf.RoundToInt((timestapMax - timestapMin));
            return resultTime;
        }

        /// <summary>
        /// 获取日期之间的天数
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDataTime"></param>
        /// <returns>日期之间的间隔天数</returns>
        public static int GetTwoDateTimeIntervalDay(DateTime startDateTime, DateTime endDataTime)
        {
            //对2008-1-20 11:44:47使用ToShortDateString，结果就是2008-1-20，因为我们只关心相差的天数
            DateTime start = Convert.ToDateTime(startDateTime.ToShortDateString());
            DateTime end = Convert.ToDateTime(endDataTime.ToShortDateString());
            TimeSpan result = end - start;
            return result.Days;
        }

        /// <summary>
        /// 获取当前时间戳（13位）
        /// </summary>
        /// <returns>返回数值的单位是毫秒</returns>
        public static long GetCurrentUnixTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
        #endregion

        #region 类型转换
        /// <summary>
        /// DateTime转换为long
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        public static long ConvertDateTimeToLong(DateTime _dateTime)
        {
            //获取当地的格林尼治时间
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //获取标准时间差
            TimeSpan toNowTime = _dateTime - dtStart;
            long timeStamp = toNowTime.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }

        /// <summary>
        /// long转换为DateTime
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime ConvertLongToDateTime(long d)
        {
            //获取当地的格林尼治时间
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNowTime = new TimeSpan(lTime);
            DateTime dtResult = dtStart + toNowTime;
            return dtResult;
        }

        /// <summary>
        /// DateTime转换为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isSeconds">是否是秒级时间戳，false就是毫秒级</param>
        /// <returns></returns>
        public static long DateTimeToUnix(DateTime dateTime,bool isSeconds = true)
        {
            // 当地时区时间
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            // 相差秒数
            long timeStamp = isSeconds ? (long)(dateTime - startTime).TotalSeconds : (long)(dateTime - startTime).TotalMilliseconds;
            return timeStamp;
        }

        /// <summary>
        /// Unix时间戳转换为 DateTime
        /// </summary>
        /// <param name="unixTimeStamp">Unix时间戳（10位-秒级）</param>
        /// <returns></returns>
        public static string UnixToDateTime(long unixTimeStamp)
        {
            // 当地时区
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); 
            DateTime dt = startTime.AddSeconds(unixTimeStamp);
            return dt.ToString("yyyy/MM/dd HH:mm:ss:ffff");
        }
        #endregion

        #region 显示时间格式转换
        /// <summary>
        /// 计时时间转 00:00格式
        /// </summary>
        /// <param name="time">转换时间</param>
        /// <param name="showHour">显示小时</param>
        /// <returns></returns>
        public static string SecondToString_1(int time, bool showHour = false)
        {
            if (showHour)
            {
                return (time / 3600 > 9 ? "" : "0") + time / 3600 + ":" + ((time % 3600) / 60 > 9 ? "" : "0") +
                       (time % 3600) / 60 + ":" + (time % 60 > 9 ? "" : "0") + time % 60;
            }
            else
            {
                return (time / 60 > 9 ? "" : "0") + time / 60 + ":" + (time % 60 > 9 ? "" : "0") + time % 60;
            }
        }

        public static string SecondToString_2(int time)
        {

            return "";
        }
        #endregion

        #region 时间戳检查相关
        /// <summary>
        /// 两个日期是否是同一天
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool IsSameDay(string DateTime1, string DateTime2)
        {
            DateTime t1 = Convert.ToDateTime(DateTime1);
            DateTime t2 = Convert.ToDateTime(DateTime2);
            return (t1.Year == t2.Year && t1.Month == t2.Month && t1.Day == t2.Day);
        }
        #endregion 
    }
}