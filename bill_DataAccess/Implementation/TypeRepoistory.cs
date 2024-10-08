﻿using bill_DataAccess.Data;
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
    public class TypeRepoistory : GenericRepoistory<Types>, ITypeRepoistory
    {
        private AppDbContext _db;
        public TypeRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public bool TypeName(TypeViewModel types)
        {
            bool result = _db.Types.Any(x => x.TypeName == types.TypeName && x.Id != types.Id);
            return result;
        }

        public void UpDate(Types types)
        {
           var value =  _db.Types.FirstOrDefault(x=>x.Id == types.Id);
            if (value != null)
            {
                value.TypeName = types.TypeName;
                value.Notes = types.Notes;
                value.CompanyId = types.CompanyId;

                _db.SaveChanges();
            }
        }
    }
}
