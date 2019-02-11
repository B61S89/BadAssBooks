using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BadAssBooks.Models
{
    public enum ReadStatus
    {
        Read,
        Reading,
        WantToRead
    }
    public class MyBook
    {
        public int MyBookID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public ReadStatus? ReadStatus { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}