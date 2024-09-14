using bill_Entities.Models;
using bill_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface IUnitssRepoistory : IGenericRepoistory<Unit>
    {
        void UpDate(Unit unit);

        bool UnitsName(UnitViewModel unit);
    }
}
