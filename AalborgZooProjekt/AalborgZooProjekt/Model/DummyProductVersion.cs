﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProductVersion
    {
        public bool IsActive { get; set; }
        public DummyUnit Unit { get; set; }

        public DummyProductVersion(bool isActive)
        {
            this.IsActive = isActive;
        }

        public DummyProductVersion(bool isActive, DummyUnit unit)
        {

            isActive = IsActive;
            unit = Unit;
        }

        public void ActivateProduct()
        {
            if (!IsActive)
            {
                IsActive = true;
            }
        }

        public void DeactivateProduct()
        {
            if (IsActive)
            {
                IsActive = false;
            }
        }

    }
}
