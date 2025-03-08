using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class AddImageRequest {
        public string Url { get; set; }

        public Image ToEntity()
        {
            return new Image
            {
                Url = this.Url,
            };
        }
    }

}
