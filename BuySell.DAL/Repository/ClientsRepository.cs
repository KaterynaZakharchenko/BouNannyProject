using BouNanny.DAL.Data;
using BouNanny.Models;
using System;

namespace BouNanny.DAL.Repository
{
    public class ClientsRepository : RepositoryBase<Client>
    {
        public ClientsRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
