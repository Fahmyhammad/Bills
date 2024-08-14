using bill_DataAccess.Data;
using bill_Entities.Repoistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            company = new CompanyRepoistory(db);
            type = new TypeRepoistory(db);
            unitss = new UnitssRepoistory(db);
            client = new ClientRepoistory(db);
            item = new ItemRepoistory(db);
            salesInvoice = new SalesInvoiceRepoistroy(db);
            report = new ReportRepoistory(db);
        }

        public ICompanyRepoistory company { get; private set; }

        public ITypeRepoistory type { get; private set; }

        public IUnitssRepoistory unitss {  get; private set; }

        public IClientRepoistory client {  get; private set; }

        public IItemRepoistory item {  get; private set; }

        public ISalesInvoiceRepoistory salesInvoice {  get; private set; }

        public IReportRepoistory report {  get; private set; }

        public async Task<int> Complete()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
                                                                                                                                                                                                             _db.Dispose();
        }
    }
}




