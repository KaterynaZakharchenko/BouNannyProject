using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Year
    {
        [Display(Name = "Year ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Display(Name = "Year")]
        public string YearNo { get; set; }
    }
}
