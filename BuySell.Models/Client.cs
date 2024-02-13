using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Client
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Client Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Client Username is required to differentiate.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Client Name is a must required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Country where this client lives.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select State from the selected Country.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Select City where this client lives.")]
        [Display(Name = "City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Mobile number is required to contact the client for their Ads.")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Joining Date of Client is required to keep record.")]
        [Display(Name = "Joining Date")]
        public System.DateTime JoinDate { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
