using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System;

namespace Library.Controllers
{
    public class LibrarianController : Controller
    {
        [HttpGet("/librarian")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/librarian/books/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/librarian/books/new")]
        public ActionResult Create(string bookTitle, string bookAuthor)
        {
            
            if (BookClass.CheckBookExistByTitle(bookTitle) == false && AuthorClass.CheckAuthorExistByName(bookAuthor) == false)
            {
                BookClass.Save(bookTitle);
                AuthorClass.Save(bookAuthor);
                int bookId = BookClass.GetBookByTitle(bookTitle).GetId();
                int authorId = (AuthorClass.GetAuthorByName(bookAuthor)).GetId();
                JoinBookAuthorClass.Save(authorId, bookId);
                int initial = 1;
                CopiesClass.Save(bookId, initial, initial);
                return RedirectToAction("New");
            }
            else if (BookClass.CheckBookExistByTitle(bookTitle) == true && AuthorClass.CheckAuthorExistByName(bookAuthor) == true)
            {
                int bookId = BookClass.GetBookByTitle(bookTitle).GetId();
                int amount = CopiesClass.GetAmountByBookId(bookId);
                int totalAmount = CopiesClass.GetTotalByBookId(bookId);
                totalAmount++;
                amount++;
                CopiesClass.Update(bookId, amount);
                CopiesClass.UpdateTotal(bookId, totalAmount);
                return RedirectToAction("New");
            }
            else
            {
                BookClass.Save(bookTitle);
                int bookId = BookClass.GetBookByTitle(bookTitle).GetId();
                int authorId = (AuthorClass.GetAuthorByName(bookAuthor)).GetId();
                JoinBookAuthorClass.Save(authorId, bookId);
                int initial = 1;
                CopiesClass.Save(bookId, initial, initial);
                return RedirectToAction("New");
            }
        }
    }
}
