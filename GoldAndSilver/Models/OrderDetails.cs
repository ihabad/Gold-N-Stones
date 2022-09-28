using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoldAndSilver.Models
{
    public class OrderDetails
    {
        
            [Key]
            public int Id { get; set; }

            [Required]
            public int OrderId { get; set; }

            [ForeignKey("OrderId")]
            public virtual OrderHeader OrderHeader { get; set; }

            [Required]
            public int CollectionId { get; set; }

            [ForeignKey("CollectionId")]
            public virtual  Collection Collection{ get; set; }

            public int Count { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }

            [Required]
            public double Price { get; set; }
        }
}
