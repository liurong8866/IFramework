using System;
using System.Linq;

namespace IFramework.Core
{
    /// <summary>
    /// 日期时间工具类
    /// </summary>
    public static class DateTimeExtention
    {
        /// <summary>
        /// Unix时间起始时间
        /// Unix时间戳（TimeStamp）是指格林尼治时间1970年1月1日0时（北京时间1970年1月1日8时）起至现在的总秒数（10位）或总毫秒数（13位）
        /// </summary>
        public static readonly DateTime StarTime = new DateTime(1970, 1, 1).ToLocalTime();

        /// <summary>
        /// 小时格式化
        /// </summary>
        public static readonly string HourFormat = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 秒格式化
        /// </summary>
        public static readonly string SecondsFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 毫秒格式化
        /// </summary>
        public static readonly string MilliSecondsFormat = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>
        /// 周未定义
        /// </summary>
        private static readonly DayOfWeek[] Weekend = { DayOfWeek.Saturday, DayOfWeek.Sunday };

        /// <summary>
        /// 小时格式化
        /// </summary>
        public static string ToHourFormat(this DateTime date)
        {
            return date.ToString(HourFormat);
        }

        /// <summary>
        /// 秒格式化
        /// </summary>
        public static string ToSecondsFormat(this DateTime date)
        {
            return date.ToString(SecondsFormat);
        }

        /// <summary>
        /// 毫秒格式化
        /// </summary>
        public static string ToMilliSecondsFormat(this DateTime date)
        {
            return date.ToString(MilliSecondsFormat);
        }

        /// <summary>
        /// 获取从Unix起始时间到给定时间的毫秒数时间戳
        /// </summary>
        public static long ToUnixMilliseconds(this DateTime datetime)
        {
            TimeSpan ts = datetime.Subtract(StarTime);
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 获取从Unix起始时间到给定时间的秒数时间戳
        /// </summary>
        public static long ToUnixSeconds(this DateTime datetime)
        {
            TimeSpan ts = datetime.Subtract(StarTime);
            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// 时间戳转日期
        /// </summary>
        public static DateTime ToDateTimeBySeconds(this long timestamp)
        {
            if (timestamp.ToString().Length != 10) {
                throw new ArgumentException("时间戳长度不正确。");
            }
            return StarTime.AddSeconds(timestamp);
        }

        /// <summary>
        /// 时间戳转日期
        /// </summary>
        public static DateTime ToDateTimeByMilliseconds(this long timestamp)
        {
            if (timestamp.ToString().Length != 13) {
                throw new ArgumentException("时间戳长度不正确。");
            }
            return StarTime.AddMilliseconds(timestamp);
        }

        /// <summary>
        /// 明天
        /// </summary>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }

        /// <summary>
        /// 昨天
        /// </summary>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }

        /// <summary>
        /// 判断是否为今天
        /// </summary>
        public static bool IsToday(this DateTime date)
        {
            return date.Date == DateTime.Now.Date;
        }

        /// <summary>
        /// 是否是工作日
        /// </summary>
        public static bool IsWeekDay(this DateTime date)
        {
            return !date.IsWeekend();
        }

        /// <summary>
        /// 是否是周未
        /// </summary>
        public static bool IsWeekend(this DateTime date)
        {
            return Weekend.Any(p => p == date.DayOfWeek);
        }

        /// <summary>
        /// 与给定日期是否是同一天
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="other">给定日期</param>
        public static bool IsEqual(this DateTime date, DateTime other)
        {
            return date.Date == other.Date;
        }

        /// <summary>
        /// 早于给定日期
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="other">给定日期</param>
        public static bool IsBefore(this DateTime date, DateTime other)
        {
            return date.CompareTo(other) < 0;
        }

        /// <summary>
        /// 晚于给定日期
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="other">给定日期</param>
        public static bool IsAfter(this DateTime date, DateTime other)
        {
            return date.CompareTo(other) > 0;
        }

        /// <summary>
        ///  给定日期开始一刻,精确到0:0:0.0
        /// </summary>
        public static DateTime StartTimeOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        ///  给定日期的中午,精确到12:0:0.0
        /// </summary>
        public static DateTime NoonOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
        }

        /// <summary>
        /// 给定日期最后一刻,精确到23:59:59.999
        /// </summary>
        public static DateTime EndTimeOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 给定月份的第1天
        /// </summary>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 给定月份的最后1天
        /// </summary>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return date.GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 给定日期所在月份第1个星期几所对应的日期
        /// </summary>
        /// <param name="date">给定日期</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>所对应的日期</returns>
        public static DateTime GetFirstWeekDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            DateTime dt = date.GetFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek) dt = dt.AddDays(1);
            return dt;
        }

        /// <summary>
        /// 给定日期所在月份最后1个星期几所对应的日期
        /// </summary>
        /// <param name="date">给定日期</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <returns>所对应的日期</returns>
        public static DateTime GetLastWeekDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            DateTime dt = date.GetLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek) dt = dt.AddDays(-1);
            return dt;
        }

        /// <summary>
        /// 给定日期所在月份共有多少天
        /// </summary>
        public static int GetCountDaysOfMonth(this DateTime date)
        {
            return date.GetLastDayOfMonth().Day;
        }
    }
}
