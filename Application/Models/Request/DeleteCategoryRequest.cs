using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class DeleteCategoryRequest {
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
