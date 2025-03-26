using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Model.Entities;

namespace Application.Abstractions {
    public interface IProductService : GeneralService<Product> {
        new Task AddAsync(Product entity, List<AddProductSizeRequest> sizes);
        Task DeleteAsync(int productId, int userId);
        Task UpdateAsync(int productId, Product product);
    }
}
