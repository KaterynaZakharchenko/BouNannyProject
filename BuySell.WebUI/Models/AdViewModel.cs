using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BouNanny.Models;
using System.ComponentModel.DataAnnotations;

namespace BouNanny.WebUI.Models
{
    public class AdViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter an eye catching Title for your Ad.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Professional level is required.")]
        [Display(Name = "Level")]
        public int LevelID { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Starting date is required.")]
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

        [Required(ErrorMessage = "Client ID is a must required.")]
        [Display(Name = "Client ID")]
        public int ClientID { get; set; }

        [Display(Name = "Posting Time")]
        public System.DateTime PostingTime { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 50)]
        public string Review { get; set; }

        public virtual Level Level { get; set; }
        public virtual TimePeriod TimePeriod { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual Client Client { get; set; }

        //Lists for DropDowns
        public virtual IEnumerable<Level> LevelsList { get; set; }
        public virtual IEnumerable<TimePeriod> TimePeriodsList { get; set; }
        public virtual IEnumerable<Country> CountriesList { get; set; }
        public virtual IEnumerable<State> StatesList { get; set; }
        public virtual IEnumerable<City> CitiesList { get; set; }
    }
}