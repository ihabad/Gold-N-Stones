using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoldAndSilver.Models
{
    public class Collection
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price must be at least ${1}")]
        public double Price { get; set; }

    }
}
