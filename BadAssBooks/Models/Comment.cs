using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BadAssBooks.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}