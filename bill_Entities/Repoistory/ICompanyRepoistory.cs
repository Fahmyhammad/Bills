using bill_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface ICompanyRepoistory : IGenericRepoistory<Company>
    {

        void UpDate(Company company);
        bool CompanyName(Company company);

    }
}
