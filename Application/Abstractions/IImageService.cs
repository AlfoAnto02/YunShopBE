using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Abstractions {
    public interface IImageService : GeneralService<Image> {
        public void CheckImages(IEnumerable<Image> images);
        public Task DeleteAsync(int id);
    }
}
