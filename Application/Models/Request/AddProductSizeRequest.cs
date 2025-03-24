using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class AddProductSizeRequest
    {
        public int SizeId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool Express { get; set; }
        public bool Hide { get; set; }
    }
}
