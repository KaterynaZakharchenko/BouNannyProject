using System.Collections.Generic;
using BouNanny.Models;

namespace BouNanny.WebUI.Models
{
    public class HomeViewModel
    {
        public int ID { get; set; }

        //Lists of Ads to be shown on HomePage
        public virtual ICollection<Ad> Ads { get; set; }

        //public virtual IEnumerable<Ad> Ads { get; set; }
    }
}