using System;
using System.ComponentModel.DataAnnotations;

namespace BouNanny.Models
{
    public class Review
    {
        public int ID { get; set; }

        [MinLength(50)]
        [RegularExpression(@"^[\s\S]{50,}$", ErrorMessage = "Minimum 50 Characters required for the review.")]
        [Required]
        [Display(Name = "Review.")]
        public string Content { get; set; }

        [Required]
        public DateTime PostingTime { get; set; }

        [Display(Name = "Review Stars.")]
        public int ReviewStars { get; set; }

        [Display(Name = "Ad Details are Required.")]
        public int AdID { get; set; }

        [Display(Name = "Reviewer is Required.")]
        public int ClientID { get; set; }
        //I know it should be UserID but on our website whoever is registered is a Client

        public virtual Ad Ad { get; set; }
        public virtual Client Client { get; set; }
    }
}
