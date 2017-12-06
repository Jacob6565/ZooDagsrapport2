using System;
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

        public DummyProductVersion(string unit)
        {
            Unit = new DummyUnit(unit);
        }

        public DummyProductVersion(DummyUnit unit)
        {
            Unit = unit;
        }

    }
}
