using AcademiaCerului.Core;
using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Models
{
    public class ListViewModel
    {
        public ListViewModel(IBlogRepository blogRepository, int pageNo)
        {
            Posts = blogRepository.Posts(pageNo - 1, 10);
            TotalPosts = blogRepository.TotalPosts();
        }

        public ListViewModel(IBlogRepository blogRepository, string slug, string type, int pageNo)
        {
            switch (type)
            {
                case "Tag":
                    Posts = blogRepository.PostsForTag(slug, pageNo - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForTag(slug);
                    Tag = blogRepository.Tag(slug);
                    break;
                default:
                    Posts = blogRepository.PostsForCategory(slug, pageNo - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForCategory(slug);
                    Category = blogRepository.Category(slug);
                    break;
            }
        }

        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
        public Category Category { get; private set; }
        public Tag Tag { get; private set; }
    }
}