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
    public class ReportRepoistory : GenericRepoistory<Report>, IReportRepoistory
    {
        private AppDbContext _db;
        public ReportRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
