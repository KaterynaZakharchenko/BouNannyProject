using BouNanny.DAL.Data;
using BouNanny.Models;
using System;

namespace BouNanny.DAL.Repository
{
    public class ImagesRepository : RepositoryBase<Image>
    {
        public ImagesRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
