using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Model.Entities;

namespace Application.Abstractions {
    public interface ICategoryService : GeneralService<Category> {
         Task DeleteAsync(DeleteCategoryRequest categoryName);
    }
}
