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

        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
    }
}