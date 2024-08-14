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
    public class SalesInvoiceRepoistroy : GenericRepoistory<SalesInvoice>, ISalesInvoiceRepoistory
    {
        private AppDbContext _db;
        public SalesInvoiceRepoistroy(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public decimal CalculatNetPrice(decimal Total, decimal Discount)
        {
           var result = Total - Discount;
            return result;
        }

        public decimal CalculatPrice(int quantity, decimal Price)
        {
           var result = quantity * Price;
            return result;

        }

        public decimal CalculatTheRest(decimal TheNet, decimal PaidUp)
        {
            if(TheNet == PaidUp || PaidUp > TheNet || PaidUp == 0)
            {
                return 0;
            }
            else
            {
                return TheNet - PaidUp;
            }

        }

        public int RandomNum()
        {
             Random  random = new Random();
            return random.Next(100000,999999);
        }

        public void Update(SalesInvoice salesInvoice)
        {
            var value = _db.salesInvoices.FirstOrDefault(x => x.Id == salesInvoice.Id);
           if (value != null)
            {
                value.ClientId = salesInvoice.ClientId;
                value.TableItemId = salesInvoice.TableItemId;
                value.Quintity = salesInvoice.Quintity;
                value.Discount = salesInvoice.Discount;
                value.PaidUp = salesInvoice.PaidUp;
            }
        }
    }
}
