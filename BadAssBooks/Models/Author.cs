using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BadAssBooks.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}