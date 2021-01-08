using BookStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id =1, Title = "ASP.NET Core", Author="Muhammad"},
                new BookModel(){Id =2, Title = "English", Author="Zahra"},
                new BookModel(){Id =3, Title = "Web", Author="Nematullah"},
                new BookModel(){Id =4, Title = "JavaScript", Author="Baqir"},
            };
        }
    }
}
