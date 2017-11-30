using AcademiaCerului.Core;
using AcademiaCerului.Core.Objects;
using System.Collections.Generic;

namespace AcademiaCerului.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
        }

        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
    }
}