﻿using bill_Entities.Models;
using bill_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Repoistory
{
    public interface IClientRepoistory : IGenericRepoistory<Client>
    {
        void UpDate(Client client);
        bool ClientName(ClientViewModel client);
        int RandomNumber();
    }
}
