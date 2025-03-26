using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Models.DTOs {
    public class BrandDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddedBy { get; set; }

        public BrandDTO(Brand brand) {
            Id = brand.Id;
            Name = brand.Name;
            AddedBy = brand.AddedBy;
        }
    }
}
