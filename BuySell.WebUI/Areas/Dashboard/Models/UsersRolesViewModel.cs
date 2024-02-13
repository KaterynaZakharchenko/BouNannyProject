using BouNanny.WebUI.Models;
using System.Collections.Generic;

namespace BouNanny.WebUI.Areas.Dashboard.Models
{
    public class UsersRolesViewModel
    {
        public ICollection<ApplicationUser> Adminstrators { get; set; }
        public ICollection<ApplicationUser> Managers { get; set; }
        public ICollection<ApplicationUser> Clients { get; set; }
    }
}