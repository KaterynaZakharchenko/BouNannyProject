using BouNanny.DAL.Data;
using BouNanny.Models;
using System;

namespace BouNanny.DAL.Repository
{
    public class ReviewsRepository : RepositoryBase<Review>
    {
        public ReviewsRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
