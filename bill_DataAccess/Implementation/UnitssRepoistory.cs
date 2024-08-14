using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Implementation
{
    public class UnitssRepoistory : GenericRepoistory<Unit>, IUnitssRepoistory
    {
        private AppDbContext _db;
        public UnitssRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UnitsName(Unit unit)
        {
            bool result = _db.units.Any(x=>x.UnitName == unit.UnitName && x.Id != unit.Id);
            return result;
        }

        public void UpDate(Unit unit)
        {
            var value = _db.units.FirstOrDefault(x => x.Id == unit.Id);
            if(value != null)
            {
                value.UnitName = unit.UnitName;
                value.Notes = unit.Notes;
            }
        }
    }
}
