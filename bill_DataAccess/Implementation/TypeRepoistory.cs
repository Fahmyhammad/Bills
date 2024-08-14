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
    public class TypeRepoistory : GenericRepoistory<Types>, ITypeRepoistory
    {
        private AppDbContext _db;
        public TypeRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public bool TypeName(Types types)
        {
            bool result = _db.Types.Any(x => x.TypeName == types.TypeName && x.Id != types.Id);
            return result;
        }

        public async void UpDate(Types types)
        {
           var value = await _db.Types.FirstOrDefaultAsync(x=>x.Id == types.Id);
            if (value != null)
            {
                value.TypeName = types.TypeName;
                value.Notes = types.Notes;
                value.CompanyId = types.CompanyId;
            }
        }
    }
}
