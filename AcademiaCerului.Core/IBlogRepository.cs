using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Core
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNo, int pageSize);
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        int TotalPosts();
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);
        Category Category(string categorySlug);
        Tag Tag(string tagSlug);
        Post Post(int year, int month, string titleSlug);
    }
}
