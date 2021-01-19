using BookStoreApplication.Helpers;
using Microsoft.AspNetCore.Http;
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
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title")]
        //[MyCustomValidation("Mvc")]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the Author name")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength =30)]
        public string Description { get; set; }
        public string Categoey { get; set; }
        [Required(ErrorMessage ="Please choose book language")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Display(Name ="Total pages of book")]
        [Required(ErrorMessage = "Please enter totalPages")]
        public int? TotalPages { get; set; }
        [Display(Name ="Please upload your cover photo")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name ="Please upload your slide show images")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }

    }
}
