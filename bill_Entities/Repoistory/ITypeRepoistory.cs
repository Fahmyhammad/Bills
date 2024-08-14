using bill_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface ITypeRepoistory : IGenericRepoistory<Types>
    {
        void UpDate(Types types);
        bool TypeName(Types types);
    }
}
