using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BouNanny.Models
{
    public class Ad
    {
        [Display(Name = "Ad ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        [Display(Name = "Level")]
        public int LevelID { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex ID is required.")]
        [Display(Name = "Sex ID")]
        public string Sex { get; set; }

        public virtual Level Level { get; set; }

        [Required(ErrorMessage = "Enter an eye catching Title for your Ad.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter Starting date.")]
        [Display(Name = "Starting Date")]
        public DateTime StartingTime { get; set; }

        [Required(ErrorMessage = "Details about time period are required.")]
        [Display(Name = "Time Period")]
        public int TimePeriodID { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Price.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter Country details.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select a Country State.")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Select a City.")]
        [Display(Name = "City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Client Details are must required.")]
        [Display(Name = "Client")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Slug is shown in URL of Browser.")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Posting Time is required.")]
        [Display(Name = "Posting Time")]
        public System.DateTime PostingTime { get; set; }

        public virtual TimePeriod TimePeriod { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Client Client { get; set; }

        //Lists for DropDowns
        public virtual IQueryable<Level> LevelsList { get; set; }
        public virtual IEnumerable<TimePeriod> TimePeriodsList { get; set; }
        public virtual IEnumerable<Country> CountriesList { get; set; }
        public virtual IEnumerable<State> StatesList { get; set; }
        public virtual IEnumerable<City> CitiesList { get; set; }
    }
}
