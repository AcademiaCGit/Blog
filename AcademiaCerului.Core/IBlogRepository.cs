using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Core
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNo, int pageSize);
        IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        IList<Category> Categories();
        IList<Tag> Tags();
        int TotalPosts(bool isPublished = true);
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);
        Category Category(string categorySlug);
        Category Category(int id);
        Tag Tag(string tagSlug);
        Tag Tag(int id); 
        Post Post(int year, int month, string titleSlug);
        int AddPost(Post post);
        void EditPost(Post post);
        void DeletePost(int id);
        int AddCategory(Category category);
        void EditCategory(Category category);
    }
}
