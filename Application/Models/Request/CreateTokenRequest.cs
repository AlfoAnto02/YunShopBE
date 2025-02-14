using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class CreateTokenRequest {
        public string userId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

    }
}
