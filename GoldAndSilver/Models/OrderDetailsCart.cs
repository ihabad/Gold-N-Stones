using System.Collections.Generic;

namespace GoldAndSilver.Models
{
    public class OrderDetailsCart
    {
        
            public List<ShoppingCart> listCart { get; set; }
            public OrderHeader OrderHeader { get; set; }
        
    }

}
