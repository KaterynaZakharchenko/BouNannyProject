using BouNanny.DAL.Data;
using BouNanny.Models;
using System;

namespace BouNanny.DAL.Repository
{
    public class TimePeriodsRepository : RepositoryBase<TimePeriod>
    {
        public TimePeriodsRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}