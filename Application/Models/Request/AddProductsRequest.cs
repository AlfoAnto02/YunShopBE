﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.Request {
    public class AddProductsRequest {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AddImageRequest> Images { get; set; } = new List<AddImageRequest>();
        public int CategoryId { get; set; }
        public int AddedBy { get; set; }
        public int BrandId { get; set; }
        public List<AddProductSizeRequest> Sizes { get; set; } = new List<AddProductSizeRequest>();

        public Product ToEntity()
        {
            return new Product
            {
                Name = this.Name,
                Description = this.Description,
                Images = this.Images.Select(i => i.ToEntity()).ToList(),
                CategoryId = this.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = AddedBy,
                BrandId = BrandId,
            };
        }
    }
}
