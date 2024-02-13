using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Image
    {
        public int ID { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Image Path is Required.")]
        public string Path { get; set; }
        public int AdID { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
