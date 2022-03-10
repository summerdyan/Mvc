using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // shows "Release Date" on web application
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        // correctly maps Price to currency in the database
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string Rating { get; set; }
    }
}
