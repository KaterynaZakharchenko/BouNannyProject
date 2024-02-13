using BouNanny.DAL.Data;
using BouNanny.Models;
using System;

namespace BouNanny.DAL.Repository
{
    public class CountriesRepository : RepositoryBase<Country>
    {
        public CountriesRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
