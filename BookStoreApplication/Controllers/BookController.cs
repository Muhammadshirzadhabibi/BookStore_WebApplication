using BookStoreApplication.Models;
using BookStoreApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new List<SelectListItem>()
            {
                new SelectListItem(){Text ="Dari", Value ="1"},
                new SelectListItem(){Text ="English", Value ="2"},
                new SelectListItem(){Text ="Pashto", Value ="3"},
                new SelectListItem(){Text ="Hindi", Value ="4"},
                new SelectListItem(){Text ="Russain", Value ="5"},
                new SelectListItem(){Text ="British", Value ="6"}
            };
            ViewBag.Success = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new List<SelectListItem>()
            {
                new SelectListItem(){Text ="Dari", Value ="1"},
                new SelectListItem(){Text ="English", Value ="2"},
                new SelectListItem(){Text ="Pashto", Value ="3"},
                new SelectListItem(){Text ="Hindi", Value ="4"},
                new SelectListItem(){Text ="Russain", Value ="5"},
                new SelectListItem(){Text ="British", Value ="6"}
            };

            ModelState.AddModelError("", "Please fill each input according to thier error message");
            return View();
        }

        private List<LanguageModel> GetLanguage()
        {
            return new List<LanguageModel>()
            {
                new LanguageModel(){Id=1, Text="English"},
                new LanguageModel(){Id=2, Text="Dari"}
            };
        }
    }
}
