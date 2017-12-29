using Newtonsoft.Json;
using System.Collections.Generic;

namespace AcademiaCerului.Core.Objects
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string UrlSlug { get; set; }
        public virtual string Description { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts { get; set; }
    }
}