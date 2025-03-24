using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class DeleteProductRequest {
        public int ProductId { get; set; }
        public int DeletedBy { get; set; }
    }
}
