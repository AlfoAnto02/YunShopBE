using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.Entities {
    public class Size {
        public int Id { get; set; }
        public string SizeValue { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
