using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength =5)]
        [Required(ErrorMessage = "Please enter the title")]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the Author name")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength =30)]
        public string Description { get; set; }
        public string Categoey { get; set; }
        public string Language { get; set; }
        [Display(Name ="Total pages of book")]
        [Required(ErrorMessage = "Please enter totalPages")]
        public int? TotalPages { get; set; }
    }
}
