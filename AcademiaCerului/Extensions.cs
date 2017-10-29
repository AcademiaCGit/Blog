﻿using System;
using System.Configuration;

namespace AcademiaCerului
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDateTime)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return string.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneInfo).ToShortDateString(), ConfigurationManager.AppSettings["TimezoneAbbr"]);
        }
    }
}