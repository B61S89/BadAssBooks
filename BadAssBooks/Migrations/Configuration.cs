namespace BadAssBooks.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq; 
    using BadAssBooks.Models;
    using System.Collections.Generic;
    using BadAssBooks.DAL;
    using Newtonsoft.Json;
   

    internal sealed class Configuration : DbMigrationsConfiguration<DBooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public class PublishedDate
        {
            public DateTime date { get; set; }
        }

        public class tempBook
        {
            public int _id { get; set; }
            public string title { get; set; }
            public string isbn { get; set; }
            public int pageCount { get; set; }
            public PublishedDate publishedDate { get; set; }
            public string thumbnailUrl { get; set; }
            public string shortDescription { get; set; }
            public string longDescription { get; set; }
            public string status { get; set; }
            public IList<string> authors { get; set; }
            public IList<string> categories { get; set; }
        }



        protected override void Seed(DBooksContext context)
        {
            Random rnd = new Random();
            string[] names =
            {"Abigail","Alexandra","Alison","Amanda","Amelia","Amy","Andrea","Angela",
"Anna","Anne","Audrey","Ava","Bella","Bernadette","Carol","Caroline","Carolyn","Chloe","Claire","Deirdre",
"Diana","Diane","Donna","Dorothy","Elizabeth","Ella","Emily","Emma","Faith","Felicity","Fiona","Gabrielle",
"Grace","Hannah","Heather","Irene","Jan","Jane","Jasmine","Jennifer","Jessica","Joan","Joanne","Julia",
"Karen","Katherine","Kimberly","Kylie","Lauren","Leah","Lillian","Lily","Lisa","Madeleine","Maria",
"Mary","Megan","Melanie","Michelle","Molly","Natalie","Nicola","Olivia","Penelope","Pippa","Rachel",
"Rebecca","Rose","Sally","Samantha","Sarah","Sonia","Sophie","Stephanie","Sue","Theresa","Tracey",
"Una","Vanessa","Victoria","Virginia","Wanda","Wendy","Yvonne","Zoe","Andrew","Anthony","Austin","Benjamin",
"Blake","Boris","Brandon","Brian","Cameron","Carl","Charles","Christian","Christopher","Colin","Connor",
"Dan","David","Dominic","Dylan","Edward","Eric","Evan","Jason","Joe","John","Jonathan","Joseph","Joshua",
"Julian","Justin","Keith","Kevin","Leonard","Liam","Lucas","Luke","Matt","Max","Michael","Nathan","Neil",
"Nicholas","Oliver","Owen","Paul","Peter","Phil","Piers","Richard","Robert","Ryan","Sam","Sean","Sebastian",
"Simon","Stephen","Steven","Stewart","Trevor","Victor","Warren","William"
            };
            string[] lastNames =
            {
                "Anderson","Arnold","Avery","Bailey","Baker","Ball","Bell","Berry","Black","Blake","Bond",
"Bower","Brown","Buckland","Burgess","Butler","Cameron","Campbell","Carr","Chapman","Churchill","Clark",
"Clarkson","Coleman","Cornish","Davidson","Davies","Dickens","Dowd","Duncan","Dyer","Edmunds",
"Ellison","Ferguson","Fisher","Forsyth","Fraser","Gibson","Gill","Glover","Graham","Grant","Gray","Greene",
"Hamilton","Hardacre","Harris","Hart","Hemmings","Henderson","Hill","Hodges","Howard","Hudson","Hughes",
"Hunter","Ince","Jackson","James","Johnston","Jones","Kelly","Kerr","King","Knox","Lambert","Langdon",
"Lawrence","Lee","Lewis","Ross","Russell","Rutherford","Sanderson","Scott","Sharp","Short","Simpson",
"Skinner","Slater","Smith","Springer","Stewart","Sutherland","Taylor","Terry","Thomson","Tucker","Turner",
"Underwood","Vance",
            };
            string[] loremIpsum = { "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            "We spend so much of our life looking - but never seeing. We need dark in order to show light. Let's put some happy little bushes on the other side now. We touch the canvas, the canvas takes what it wants. Pretend you're water. Just floating without any effort. Having a good day.",
            "Anything you want to do you can do here. Even the worst thing we can do here is good. But we're not there yet, so we don't need to worry about it.",
            "That is when you can experience true joy, when you have no fear. A big strong tree needs big strong roots. Let's just drop a little Evergreen right here. Let's put a touch more of the magic here.",
            "It's important to me that you're happy. Talk to trees, look at the birds. Whatever it takes. Mix your color marbly don't mix it dead. Nice little fluffy clouds laying around in the sky being lazy",
            "There is no right or wrong - as long as it makes you happy and doesn't hurt anyone. Let's put some highlights on these little trees. The sun wouldn't forget them. Here we're limited by the time we have. Everybody's different. Trees are different. Let them all be individuals."
        };
            List<String> classifications = new List<String>();


            var authors = new List<Author>();            
            for (int i=0;i<100 ;i++ )
            {
                int nameIndex = rnd.Next(names.Length);
                int lNameIndex = rnd.Next(lastNames.Length);
                Author seedA = new Author
                {
                    LastName = lastNames[lNameIndex],
                    FirstName = names[nameIndex],
                };
                authors.Add(seedA);
            }
            authors.ForEach(a => context.Authors.AddOrUpdate(p => p.LastName, a));
            context.SaveChanges();
                                   
            var books = new List<Book> { };
            string[] catalog = System.IO.File.ReadAllLines(@"C:\Users\BS\source\repos\BadAssBooks\BadAssBooks\catalog.json");
            for(int i =0; i< catalog.Length; i++)
            {
                tempBook parsedJson = JsonConvert.DeserializeObject<tempBook>(catalog[i]);
                if (string.IsNullOrWhiteSpace(parsedJson.thumbnailUrl))
                    parsedJson.thumbnailUrl = "~/Image/BABImgPlaceholder.png";
                if (string.IsNullOrWhiteSpace(parsedJson.shortDescription))
                    parsedJson.shortDescription = loremIpsum[rnd.Next(6)];
                if (string.IsNullOrWhiteSpace(parsedJson.longDescription))
                    parsedJson.longDescription = loremIpsum[rnd.Next(6)];
                DateTime dateTime = new DateTime(1955, 1, 1);

                if (parsedJson.categories.Count > 0)
                    if(!classifications.Contains(parsedJson.categories[0]))
                        classifications.Add(parsedJson.categories[0]);

                if (!string.IsNullOrWhiteSpace(parsedJson.isbn))
                {
                    Book seedB = new Book
                    {
                        Title = parsedJson.title,
                        ISBN = parsedJson.isbn,
                        PageCount = parsedJson.pageCount.ToString(),
                        PublishedDate = dateTime.AddDays(rnd.Next(22995)),
                        ThumbnailURL = parsedJson.thumbnailUrl,
                        ShortDescription = parsedJson.shortDescription,
                        LongDescription = parsedJson.longDescription,
                        Status = parsedJson.status,
                        Authors = new List<Author>(),
                        Categories = new List<Category>(),
                        Users = new List<User>()
                    };
                    books.Add(seedB);
                }                
            }
            books.ForEach(b => context.Books.AddOrUpdate(p => p.ISBN, b));
            context.SaveChanges();

            var users = new List<User>();
            for (int i=0;i<100 ;i++ )
            {
                int nameIndex = rnd.Next(names.Length);
                int lNameIndex = rnd.Next(lastNames.Length);
                User seedU = new User
                {
                    LastName = lastNames[lNameIndex],
                    FirstName = names[nameIndex],
                    ImageThumbnail = "~/Image/BABImgPlaceholder.png"
                };
                users.Add(seedU);
            }
            users.ForEach(u => context.Users.AddOrUpdate(p => p.LastName, u));
            context.SaveChanges();


            var myBooks = new List<MyBook>();
            for(int i =0; i<users.Count; i++)
            {
                for (int j = 0; j < 1 + rnd.Next(7); j++)
                {
                    int userIndex = users[i].UserID;
                    int bookIndex = rnd.Next(books.Count);
                    int statusIndex = rnd.Next(3);
                    MyBook seedMB = new MyBook
                    {
                        BookID = books[bookIndex].BookID,
                        UserID = userIndex,
                        ReadStatus = (ReadStatus)statusIndex
                    };
                    myBooks.Add(seedMB);
                }
            }
            foreach (MyBook mb in myBooks)
            {
                var myBooksInDB = context.MyBooks.Where(
                    s =>
                        s.User.UserID == mb.UserID &&
                        s.Book.BookID == mb.BookID).SingleOrDefault();
                if (myBooksInDB == null)
                {
                    context.MyBooks.Add(mb);
                }
            }
            context.SaveChanges();

            var comments = new List<Comment>();
            var ratings = new List<Rating>();
            for (int i = 0; i < books.Count; i++)
            {
                for (int j = 0; j < 1 + rnd.Next(10); j++)
                {
                    int userIndex = rnd.Next(users.Count);
                    int bookIndex = books[i].BookID;
                    Comment seedMB = new Comment
                    {
                        BookID = bookIndex,
                        UserID = users[userIndex].UserID,
                        CommentText = loremIpsum[rnd.Next(6)]
                    };
                    comments.Add(seedMB);
                    Rating seedR = new Rating
                    {
                        BookID = bookIndex,
                        UserID = users[userIndex].UserID,
                        GivenRating = rnd.Next(5) + rnd.NextDouble()
                    };
                    ratings.Add(seedR);
                }
            }
            foreach (Comment c in comments)
            {
                var myCommentsInDB = context.Comments.Where(
                    s =>
                        s.User.UserID == c.UserID &&
                        s.Book.BookID == c.BookID).SingleOrDefault();
                if (myCommentsInDB == null)
                {
                    context.Comments.Add(c);
                }
            }
            foreach (Rating r in ratings)
            {
                var myCommentsInDB = context.Ratings.Where(
                    s =>
                        s.User.UserID == r.UserID &&
                        s.Book.BookID == r.BookID).SingleOrDefault();
                if (myCommentsInDB == null)
                {
                    context.Ratings.Add(r);
                }
            }
            context.SaveChanges();

            for(int i=0; i<books.Count; i++)
            {
                for(int j=0; j < 1 + rnd.Next(3); j++)
                {
                    var aID = authors[rnd.Next(authors.Count)].AuthorID;
                    var bID = books[i].BookID;
                    AddOrUpdateAuthor(context, aID, bID);
                }                
            }
            context.SaveChanges();

            var categories = new List<Category>();
            for (int i = 0; i < books.Count; i++)
            {
                for (int j = 0; j < 1 + rnd.Next(3); j++)
                {
                    var cID = classifications[rnd.Next(classifications.Count)];
                    var bID = books[i].BookID;
                    Category c = new Category
                    {
                        BookID = bID,
                        Classification = cID
                    };
                    context.Categories.AddOrUpdate(c);                    
                }
            }
            context.SaveChanges();

            for (int i = 0; i < books.Count; i++)
            {
                for (int j = 0; j < rnd.Next(3); j++)
                {
                    var uID = users[rnd.Next(users.Count)].UserID;
                    var bID = books[i].BookID;
                    AddOrUpdateRecommendation(context, uID, bID);
                }
            }
            context.SaveChanges();
            
        }

        void AddOrUpdateAuthor(DBooksContext context, int aID, int bID)
        {
            var bk = context.Books.SingleOrDefault(b => b.BookID == bID);
            var aut = bk.Authors.SingleOrDefault(a => a.AuthorID == aID);
            bk.Authors.Add(context.Authors.Single(a => a.AuthorID == aID));
        }

        void AddOrUpdateRecommendation(DBooksContext context, int uID , int bID)
        {
            var bk = context.Books.SingleOrDefault(b => b.BookID == bID);
            var aut = bk.Users.SingleOrDefault(a => a.UserID == uID);
                bk.Users.Add(context.Users.Single(a => a.UserID == uID)); 
        }
    }
}
