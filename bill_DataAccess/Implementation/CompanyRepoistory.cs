using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Implementation
{
    public class CompanyRepoistory : GenericRepoistory<Company>,ICompanyRepoistory
    {
        private AppDbContext _db;
        public CompanyRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

      

        public bool CompanyName(CompanyViewModel company)
        {
            bool result = _db.companies.Any(x => x.Name == company.Name && x.Id != company.Id);
            return result;
        }

        public void UpDate(Company company)
        {
            var result =  _db.companies.FirstOrDefault(x => x.Id == company.Id);
            if (result != null)
            {
                result.Name = company.Name;
                result.Notes = company.Notes;

                 _db.SaveChanges();
            }
        }
    }
}
