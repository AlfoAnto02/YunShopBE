using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.DTOs {
    public class CategoryDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddedBy { get; set; }

        public CategoryDTO(Category category) {
            Id = category.Id;
            Name = category.Name;
            AddedBy = category.AddedById;
        }

    }
}
