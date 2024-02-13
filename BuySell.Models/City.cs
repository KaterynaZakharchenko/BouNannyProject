using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class City
    {
        [Display(Name = "City ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Must enter State details.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        public virtual State State { get; set; }
    }
}
