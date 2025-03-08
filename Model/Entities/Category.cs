using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Entities {
    public class Category {
        public int Id { get; set; }
        public int AddedById { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual User AddedBy { get; set; }
        [JsonIgnore]
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
