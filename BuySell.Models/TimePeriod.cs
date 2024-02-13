using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class TimePeriod
    {
        [Display(Name = "Time Period ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Time Period Type is required.")]
        [Display(Name = "Time Period Type")]
        public string TimePeriodType { get; set; }
    }
}
