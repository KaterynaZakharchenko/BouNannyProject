using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Country
    {
        [Display(Name = "Country ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Country Name is must required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
