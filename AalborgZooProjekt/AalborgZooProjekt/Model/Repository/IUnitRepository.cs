﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.Repository
{
    public interface IUnitRepository
    {
        Unit GetUnitById(int id);
    }
}
