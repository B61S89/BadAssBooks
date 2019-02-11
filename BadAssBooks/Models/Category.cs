using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BadAssBooks.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public string Classification { get; set; }

        public virtual Book Book { get; set; }
    }
}