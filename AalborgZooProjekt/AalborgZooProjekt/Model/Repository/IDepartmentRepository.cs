﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        Department AddDepartment(Department department);
    }
}
