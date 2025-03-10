using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs {
    public class ProductDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ImageUrls { get; set; }
        public int AddedBy { get; set; }

        public ProductDTO(Model.Entities.Product product) {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Name;
            ImageUrls = new List<string>();
            foreach (var image in product.Images) {
                ImageUrls.Add(image.Url);
            }
            AddedBy = product.UserId;
        }
    }
}
