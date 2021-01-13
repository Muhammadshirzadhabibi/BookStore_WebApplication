using BookStoreApplication.Data;
using BookStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value:0,
                UpdatedOn = DateTime.UtcNow
            };
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();

            return newBook.Id;

        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Categoey = book.Categoey,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Categoey = book.Categoey,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }

            return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id =1, Title = "ASP.NET Core", Author="Muhammad", Description="Some description about Muhammad",Categoey="Programming", Language="English", TotalPages=234},
                new BookModel(){Id =2, Title = "English", Author="Zahra", Description="Some description about Zahra",Categoey="Programming", Language="English", TotalPages=534},
                new BookModel(){Id =3, Title = "Web", Author="Nematullah", Description="Some description about Nematullah",Categoey="Web Designing", Language="English", TotalPages=244},
                new BookModel(){Id =4, Title = "JavaScript", Author="Baqir", Description="Some description about Baqir",Categoey="Skills", Language="English", TotalPages=664},
            };
        }
    }
}
