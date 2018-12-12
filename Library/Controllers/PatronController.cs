using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        [HttpGet("/patron")]
        public ActionResult Index()
        {
            int returned = 0;
            return View(returned);
        }

        [HttpGet("/patron/checkout")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/patron/checkout")]
        public ActionResult Create(string patronName, string bookTitle)
        {
            int check = 1;
            if (BookClass.CheckBookExistByTitle(bookTitle) == false)
            {
                check = 0;
            }
            else
            {
                if (PatronClass.CheckPatronExistByName(patronName) == false)
                {
                    PatronClass.Save(patronName);
                    int bookId = BookClass.GetBookByTitle(bookTitle).GetId();
                    int patronId = PatronClass.GetPatronIdByName(patronName);
                    JoinPatronBookClass.SavePatronCopy(patronId, bookId);
                    int amount = CopiesClass.GetAmountByBookId(bookId);
                    amount--;
                    CopiesClass.Update(bookId, amount);
                }
                else
                {
                    int bookId = BookClass.GetBookByTitle(bookTitle).GetId();
                    int patronId = PatronClass.GetPatronIdByName(patronName);
                    JoinPatronBookClass.SavePatronCopy(patronId, bookId);
                    int amount = CopiesClass.GetAmountByBookId(bookId);
                    amount--;
                    CopiesClass.Update(bookId, amount);
                }
            }
            return View("New", check);
        }

        [HttpPost("/patron/account")]
        public ActionResult Show(string patronName)
        {
            Dictionary<string, object> patronAndBooks = new Dictionary<string, object>();
            int patronId = PatronClass.GetPatronIdByName(patronName);
            List<BookClass> patronBooks = PatronClass.GetBooksByPatronId(patronId);
            PatronClass patron = new PatronClass(patronName, patronId);
            patronAndBooks.Add("books", patronBooks);
            patronAndBooks.Add("patron", patron);
            return View(patronAndBooks);
        }

        [HttpPost("/patron/account/{id}/remove")]
        public ActionResult Update(int id, int bookId)
        {
            JoinPatronBookClass.DeletePatronCopy(id, bookId);
            int amount = CopiesClass.GetAmountByBookId(bookId);
            amount++;
            CopiesClass.Update(bookId, amount);
            string patronName = PatronClass.GetPatronNameById(id);
            int returned = 1;
            return View("Index", returned);
        }
    }
}
