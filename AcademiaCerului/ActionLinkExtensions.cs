using AcademiaCerului.Core.Objects;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AcademiaCerului
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog",
                new
                {
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new
                {
                    title = post.Title
                });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            return helper.ActionLink(category.Name, "GetPostsByCategory", "Blog",
                new { category = category.UrlSlug },
                new { title = string.Format("Vezi toate postarile din {0}", category.Name) });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "GetPostsByTag", "Blog",
                new { tag = tag.UrlSlug },
                new { title = string.Format("Vezi toate postarile din {0}", tag.Name) });
        }
    }
}