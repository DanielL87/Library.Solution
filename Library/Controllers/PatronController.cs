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
            return View();
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

        [HttpPost("/patron/return")]
        public ActionResult Update(string patronName, string bookTitle)
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

    }
}
