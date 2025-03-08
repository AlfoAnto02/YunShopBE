using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Size { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public virtual List<Image> Images { get; set; } = new List<Image>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
