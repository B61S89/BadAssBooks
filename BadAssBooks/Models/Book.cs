using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadAssBooks.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        public string Title { get; set; }
        public string ISBN { get; set; }
        public string PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ThumbnailURL { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<MyBook> MyBooks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}