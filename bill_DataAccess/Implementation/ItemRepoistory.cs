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
    public class ItemRepoistory : GenericRepoistory<tableItem>, IItemRepoistory
    {
        private AppDbContext _db;
        public ItemRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public bool ItemName(ItemViewModel item)
        {
            bool result = _db.Items.Any(x => x.ItemName == item.ItemName && x.Id != item.Id);
            return result;
        }

        public void UpDate(tableItem item)
        {
            var result = _db.Items.FirstOrDefault(x => x.Id == item.Id);
            if(result !=  null)
            {
                result.ItemName = item.ItemName;
                result.SellingPrice = item.SellingPrice;
                result.BuyingPrice = item.BuyingPrice;
                result.CompanyId = item.CompanyId;
                result.TypeId = item.TypeId;

                _db.SaveChanges();
            }
        }
    }
}
