using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Level
    {
        [Display(Name = "Level ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
