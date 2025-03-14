using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Entities {
    public class ProductSize {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        public int SizeId { get; set; }
        [JsonIgnore]
        public virtual Size Size { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
