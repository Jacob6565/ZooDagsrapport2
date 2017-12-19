using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.Repository
{
    class UnitRepository : IUnitRepository
    {
        public Unit GetUnitById(int id)
        {
            using (var db = new AalborgZooContainer())
            {
                Unit unit = db.UnitSet.FirstOrDefault(x => x.Id == id);
                return unit;
            }
        }
    }
}
