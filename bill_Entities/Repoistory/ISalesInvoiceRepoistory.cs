using bill_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface ISalesInvoiceRepoistory : IGenericRepoistory<SalesInvoice>
    {
        void Update(SalesInvoice salesInvoice);
        int RandomNum();
        decimal CalculatPrice(int quantity, decimal Price);

        decimal CalculatNetPrice(decimal Total, decimal Discount);

        decimal CalculatTheRest(decimal TheNet, decimal PaidUp);
        
    }
}
