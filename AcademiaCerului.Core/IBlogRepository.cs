using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Core
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNo, int pageSize);
        int TotalPosts();
    }
}
