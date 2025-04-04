﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Application.Abstractions {
    public interface IBrandService : GeneralService<Brand> {
        Task DeleteByIdAsync(int id);
    }
}
