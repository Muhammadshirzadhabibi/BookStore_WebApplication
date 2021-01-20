using BookStoreApplication.Models;
using BookStoreApplication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Text" );
            ViewBag.Success = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "Books/cover/";
                 bookModel.CoverImageUrl =  await UploadImage(folder, bookModel.CoverPhoto);
                }
                bookModel.Gallery = new List<GalleryModel>();
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "Books/gallery/";
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }
                if (bookModel.BookPdf != null)
                {
                    string folder = "Books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Text");

            ModelState.AddModelError("", "Please fill each input according to thier error message");
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
           
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
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
