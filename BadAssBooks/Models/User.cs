using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadAssBooks.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ImageThumbnail { get; set; }

        public virtual ICollection<MyBook> MyBooks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}