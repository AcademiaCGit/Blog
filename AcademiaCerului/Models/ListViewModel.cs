using AcademiaCerului.Core;
using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Models
{
    public class ListViewModel
    {
        public ListViewModel()
        {

        }

        public ListViewModel(IBlogRepository blogRepository, int pageNo)
        {
            Posts = blogRepository.Posts(pageNo - 1, 10);
            TotalPosts = blogRepository.TotalPosts();
        }

        public ListViewModel(IBlogRepository blogRepository, string text, string type, int pageNo)
        {
            switch (type)
            {
                case "Tag":
                    Posts = blogRepository.PostsForTag(text, pageNo - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForTag(text);
                    Tag = blogRepository.Tag(text);
                    break;
                case "Category":
                    Posts = blogRepository.PostsForCategory(text, pageNo - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForCategory(text);
                    Category = blogRepository.Category(text);
                    break;
                default:
                    Posts = blogRepository.PostsForSearch(text, pageNo - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForSearch(text);
                    Search = text;
                    break;
            }
        }

        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
        public Category Category { get; private set; }
        public Tag Tag { get; private set; }
        public string Search { get; set; }
    }
}