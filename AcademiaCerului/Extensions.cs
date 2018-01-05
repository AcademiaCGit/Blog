using AcademiaCerului.Core.Objects;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace AcademiaCerului
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDateTime)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneInfo).ToShortDateString();
        }

        public static string Href(this Post post, UrlHelper helper)
        {
            return helper.RouteUrl(new { controller = "Blog", action = "Post", year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug });
        }
    }
}