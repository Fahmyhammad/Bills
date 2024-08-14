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
    public class ClientRepoistory : GenericRepoistory<Client>, IClientRepoistory
    {
        private AppDbContext _db;
        public ClientRepoistory(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public bool ClientName(Client client)
        {
            var result = _db.Clients.Any(x => x.ClientName == client.ClientName && x.Id != client.Id);
            return result;
        }

        public int RandomNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public void UpDate(Client client)
        {
            var result = _db.Clients.FirstOrDefault(x=>x.Id == client.Id);
            if(result != null)
            {
                result.ClientName = client.ClientName;
                result.Address = client.Address;
                result.Number = client.Number;
                result.Phone = client.Phone;
            }
        }
    }
}
