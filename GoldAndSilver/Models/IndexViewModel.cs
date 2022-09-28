using System.Collections.Generic;

namespace GoldAndSilver.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Collection> Collection { get; set; }
        public IEnumerable<Category> Category { get; set; }

    }
}
