using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.Request {
    public class AddProductRequest {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Size { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public List<AddImageRequest> Images { get; set; } = new List<AddImageRequest>();
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }

        public Product ToEntity()
        {
            return new Product
            {
                Name = this.Name,
                Price = this.Price,
                Size = this.Size,
                Description = this.Description,
                Brand = this.Brand,
                Images = this.Images.Select(i => i.ToEntity()).ToList(),
                CategoryId = this.CategoryId,
                Stock = this.Stock,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = UserId
            };
        }
    }
}
