using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs {
    public class ProductSizeDTO {
        public string Size { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool Express { get; set; }
        public bool Hide { get; set; }
        public ProductSizeDTO(Model.Entities.ProductSize productSize) {
            Size = productSize.Size.SizeValue;
            Stock = productSize.Stock;
            Price = productSize.Price;
            Express = productSize.Express;
            Hide = productSize.Hide;
        }
    }
}
