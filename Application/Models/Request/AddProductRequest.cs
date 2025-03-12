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
        //TODO : Change to Size Entity
        public string Description { get; set; }
        public List<AddImageRequest> Images { get; set; } = new List<AddImageRequest>();
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }
        public int BrandId { get; set; }

        public Product ToEntity()
        {
            return new Product
            {
                Name = this.Name,
                //TODO : Change to Size Entity
                Description = this.Description,
                Images = this.Images.Select(i => i.ToEntity()).ToList(),
                CategoryId = this.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = UserId,
                BrandId = BrandId
            };
        }
    }
}
