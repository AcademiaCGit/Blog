using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademiaCerului.Core.Objects
{
    public class Category
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Nume: Câmpul este obligatoriu")]
        [StringLength(500, ErrorMessage = "Nume: Lungimea maximă este de 500 de caractere")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "UrlSlug: Câmpul este obligatoriu")]
        [StringLength(500, ErrorMessage = "UrlSlug: Lungimea maximă este de 500 de caractere")]
        public virtual string UrlSlug { get; set; }

        public virtual string Description { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts { get; set; }
    }
}