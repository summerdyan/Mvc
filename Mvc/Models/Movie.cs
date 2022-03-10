using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Movie
    {
        public int Id { get; set; }

        // set max string length to 60 and min string length to 3
        [StringLength(60, MinimumLength = 3)]
        [Required] // property must have a value but user can enter white space to satisfy it
        public string Title { get; set; }

        // shows "Release Date" on web application
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)] // maps Release Date to date value
        public DateTime ReleaseDate { get; set; }

        // can only include characters, first character must be capitalized,
        // white spaces allowed but numbers and special characters are not
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]          // property must have value
        [StringLength(30)]  // constrains Genre string length to 30
        public string Genre { get; set; }

        [Range(1, 100)]                 // constrains price to be between 1 and 100
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // first character must be capitalized, allows special characters
        // and numbers in subsequent spaces
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]   // limits rating string length to 5
        [Required]          // property must have value
        public string Rating { get; set; }
    }
}
