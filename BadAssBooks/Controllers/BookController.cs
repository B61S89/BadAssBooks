using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BadAssBooks.DAL;
using BadAssBooks.Models;
using BadAssBooks.ViewModels;
using PagedList;

namespace BadAssBooks.Controllers
{
    public class BookController : Controller
    {
        private DBooksContext db = new DBooksContext();

        // GET: Book
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var books = from b in db.Books
                        select b;
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(i => i.Title.Contains(searchString));
                
            }
            switch (sortOrder)
            {
                default:  // Name ascending 
                    books = books.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var viewModel = new BookIndexData();
            viewModel.Books = db.Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Include(b => b.Comments)
                .Include(b => b.Ratings)
                .Include(b => b.Users)
                .Where(b => b.BookID == id);
            if (viewModel.Books == null)
            {
                return HttpNotFound();
            }
            
            viewModel.Authors = viewModel.Books.Where(i => i.BookID == id.Value).Single().Authors;
            viewModel.Categories = viewModel.Books.Where(i => i.BookID == id.Value).Single().Categories;
            viewModel.Comments = viewModel.Books.Where(i => i.BookID == id.Value).Single().Comments;
            viewModel.Ratings = viewModel.Books.Where(i => i.BookID == id.Value).Single().Ratings;
            viewModel.Users = viewModel.Books.Where(i => i.BookID == id.Value).Single().Users;
            viewModel.MyBooks = viewModel.Books.Where(i => i.BookID == id.Value).Single().MyBooks;

            double totalRatings = 0;
            foreach(Rating rating in viewModel.Ratings)
            {
                totalRatings += rating.GivenRating;
            }
            ViewBag.totalRatings = Convert.ToDouble((totalRatings/ viewModel.Ratings.Count()).ToString("#.##"));

            return View(viewModel);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,ISBN,PageCount,PublishedDate,ThumbnailURL,ShortDescription,LongDescription,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,ISBN,PageCount,PublishedDate,ThumbnailURL,ShortDescription,LongDescription,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
