using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Model.Entities;

namespace Application.Abstractions {
    public interface IProductSizeService : GeneralService<ProductSize>
    {
        Task AddRelationsAsync(int entityId,List<AddProductSizeRequest> productSizes);
    }
}
