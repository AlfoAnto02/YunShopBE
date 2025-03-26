using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.Request {
    public class AddBrandRequest {
        public string Name { get; set; }
        public int AddedBy { get; set; }

        public Brand ToEntity()
        {
            return new Brand()
            {
                Name = this.Name,
                AddedBy = this.AddedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
