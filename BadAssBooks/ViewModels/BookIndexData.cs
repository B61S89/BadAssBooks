using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadAssBooks.Models;

namespace BadAssBooks.ViewModels
{
    public class BookIndexData
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<MyBook> MyBooks { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public IEnumerable<User> Users { get; set; }

    }
}