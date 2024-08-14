using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
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

        public bool CompanyName(Company company)
        {
            bool result = _db.companies.Any(x => x.Name == company.Name && x.Id != company.Id);
            return result;
        }

        public async void UpDate(Company company)
        {
            var result = await _db.companies.FirstOrDefaultAsync(x => x.Id == company.Id);
            if (result != null)
            {
                result.Name = company.Name;
                result.Notes = company.Notes;
            }
        }
    }
}
