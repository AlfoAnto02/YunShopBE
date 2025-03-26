using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs {
    public class SizeDTO {
        public int Id { get; set; }
        public string SizeValue { get; set; }
        public int AddedBy { get; set; }

        public SizeDTO(Model.Entities.Size size) {
            Id = size.Id;
            SizeValue = size.SizeValue;
            AddedBy = size.UserId;
        }
    }
}
