using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class AddSizeRequest {
        public string SizeValue { get; set; }

        public Size ToEntity() {
            return new Size {
                SizeValue = this.SizeValue,
            };
        }
    }
}
