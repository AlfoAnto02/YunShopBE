using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.DTOs {
    public class ProductDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ImageUrls { get; set; }
        public int AddedBy { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<ProductSizeDTO> Sizes { get; set; } = new List<ProductSizeDTO>();

        public ProductDTO(Model.Entities.Product product) {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Name;
            ImageUrls = new List<string>();
            foreach (var image in product.Images) {
                ImageUrls.Add(image.Url);
            }
            AddedBy = product.UserId;
            BrandId = product.BrandId;
            BrandName = product.Brand.Name;
            foreach (var productSize in product.ProductSizes) {
                Sizes.Add(new ProductSizeDTO(productSize));
            }
        }
    }
}
