using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Model.Entities;

namespace Application.Models.Request {
    public class AddCategoryRequest {
        public string Name { get; set; }
        public int AddedBy { get; set; }

        public Category ToEntity()
        {
            return new Category
            {
                Name = this.Name,
                AddedById = this.AddedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
