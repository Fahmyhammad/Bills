using bill_Entities.Models;
using bill_Entities.ViewModel;
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
        bool CompanyName(CompanyViewModel company);

    }
}
