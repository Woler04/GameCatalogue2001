using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogue.Models
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(500, MinimumLength = 1), Required]
        public string Title { get; set; }

        [StringLength(500, MinimumLength = 1), Required]
        public string Requirements { get; set; }

        [StringLength(500, MinimumLength = 1), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        //image
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
