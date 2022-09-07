using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class ArticleCategory 
    {
        //composit key primary+forenkey
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
