using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademiaCerului.Core.Objects
{
    public class Post
    {
        [Required(ErrorMessage = "Id: Câmpul este obligatoriu")]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Title: Câmpul este obligatoriu")]
        [StringLength(500, ErrorMessage = "Title: Lungimea maximă este de 500 de caractere")]
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "ShortDescription: Câmpul este obligatoriu")]
        public virtual string ShortDescription { get; set; }

        [Required(ErrorMessage = "Description: Câmpul este obligatoriu")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Meta: Câmpul este obligatoriu")]
        [StringLength(1000, ErrorMessage = "Meta: Lungimea maximă este de 1000 de caractere")]
        public virtual string Meta { get; set; }

        [Required(ErrorMessage = "UrlSlug: Câmpul este obligatoriu")]
        [StringLength(50, ErrorMessage = "UrlSlug: Lungimea maximă este de 50 de caractere")]
        public virtual string UrlSlug { get; set; }
        
        public virtual bool Published { get; set; }

        [Required(ErrorMessage = "PostedOn: Câmpul este obligatoriu")]
        public virtual DateTime PostedOn { get; set; }

        public virtual DateTime? Modified { get; set; }

        public virtual Category Category { get; set; }

        public virtual IList<Tag> Tags { get; set; }
    }
}
