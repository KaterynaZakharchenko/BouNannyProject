using BouNanny.Models;
using System.Collections.Generic;

namespace BouNanny.WebUI.Models
{
    public class AdminViewModel
    {
        public int ID { get; set; }

        public virtual ICollection<AdViewModel> Ads { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}