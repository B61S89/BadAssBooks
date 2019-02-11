using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BadAssBooks.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BadAssBooks.DAL
{
    public class DBooksContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MyBook> MyBooks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //RecommentadionDB
            modelBuilder.Entity<Book>()
                .HasMany(c => c.Users).WithMany(i => i.Books)
                .Map(t => t.MapLeftKey("BookID")
                    .MapRightKey("UserID")
                    .ToTable("Recommendation"));
        }
    }
}