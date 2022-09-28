using System.Collections.Generic;

namespace GoldAndSilver.Models
{
    public class CollectionViewModel
    {

        public Collection Collection { get; set; }

        public IEnumerable<Category> Category { get; set; }
    }
}
