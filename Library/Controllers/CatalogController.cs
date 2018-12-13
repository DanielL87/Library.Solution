using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Linq;
using System;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        [HttpGet("/catalog")]
        public ActionResult Index()
        {
            return View();
        }

       [HttpGet("/catalog/books/show")] 
       public ActionResult ShowBooks()
        {   
            List<BookClass> allBooks = BookClass.GetAll();
            return View(allBooks);
        }

       [HttpGet("/catalog/books/{id}")] 
       public ActionResult ShowBook(int id) 
       {
           Dictionary<string, object> dictionary= new Dictionary<string, object> {};
           BookClass newBook = BookClass.GetBookById(id);
           AuthorClass newAuthor = AuthorClass.GetAuthorByBookId(id);
           int amount = CopiesClass.GetAmountByBookId(id);
           int total = CopiesClass.GetTotalByBookId(id);
           CopiesClass copies = new CopiesClass(id, amount, total);
           dictionary.Add("book", newBook);
           dictionary.Add("author", newAuthor);
           dictionary.Add("copies", copies);

           return View("ShowBook", dictionary);
       }

       [HttpGet("/catalog/authors/show")] 
       public ActionResult ShowAuthors()
        {   
            List<AuthorClass> allAuthors = AuthorClass.GetAll();
            return View(allAuthors);
        }
        
       [HttpGet("/catalog/authors/{id}")] 
       public ActionResult ShowAuthor(int id)
       {
         Dictionary<string, object> dictionary= new Dictionary<string, object> {};
         List<BookClass> books = BookClass.GetBooksByAuthorId(id);
         AuthorClass author = AuthorClass.GetAuthorById(id);
         dictionary.Add("books", books);
         dictionary.Add("author", author);
         return View(dictionary);  
       }

       [HttpPost("/catalog/books/search")]
       public ActionResult SearchBook(string bookTitleSearch)
       {
           BookClass book = BookClass.GetBookByTitle(bookTitleSearch);
           int id = book.GetId();
           Dictionary<string, object> dictionary= new Dictionary<string, object> {};
           BookClass newBook = BookClass.GetBookById(id);
           AuthorClass newAuthor = AuthorClass.GetAuthorByBookId(id);
           int amount = CopiesClass.GetAmountByBookId(id);
           int total = CopiesClass.GetTotalByBookId(id);
           CopiesClass copies = new CopiesClass(id, amount, total);
           dictionary.Add("book", newBook);
           dictionary.Add("author", newAuthor);
           dictionary.Add("copies", copies);
           return View("ShowBook", dictionary);
       }

       [HttpPost("/catalog/authors/search")]
       public ActionResult SearchAuthor(string authorNameSearch)
       {
           AuthorClass author = AuthorClass.GetAuthorByName(authorNameSearch);
           int id = author.GetId();
           Dictionary<string, object> dictionary= new Dictionary<string, object> {};
           List<BookClass> books = BookClass.GetBooksByAuthorId(id);
           dictionary.Add("books", books);
           dictionary.Add("author", author);
           return View("ShowAuthor", dictionary);
       }
    }
}
