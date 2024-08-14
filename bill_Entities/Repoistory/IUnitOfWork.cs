using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepoistory company { get; }
        ITypeRepoistory type {  get; }
        IUnitssRepoistory unitss { get; }
        IItemRepoistory item { get; }
        IClientRepoistory client { get; }
        ISalesInvoiceRepoistory salesInvoice { get; }
        IReportRepoistory report { get; }
        Task<int> Complete();
    }
}
