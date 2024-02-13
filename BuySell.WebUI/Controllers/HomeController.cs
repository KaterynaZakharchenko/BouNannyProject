using BouNanny.Contracts.Repositories;
using BouNanny.DAL.Data;
using BouNanny.DAL.Repository;
using BouNanny.Models;
using BouNanny.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BouNanny.WebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext myDataContext = new DataContext();

        IRepositoryBase<Ad> Ads;

        public HomeController()
        {
            Ads = new AdsRepository(myDataContext);
        }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            List<Ad> AdViewModels = new List<Ad>();

            //Get from DB - only send 3 items from all categories to Homepage
            List<Ad> AdsList = Ads.GetAll().OrderByDescending(b => b.PostingTime).Take(3).ToList();

            foreach (Ad Ad in AdsList)
            {
                //New AdViewModel Object
                Ad AdViewModel = new Ad();

                //populate this object from 
                AdViewModel.ID = Ad.ID;
                AdViewModel.Title = Ad.Title;
                AdViewModel.LevelID = Ad.LevelID;
                AdViewModel.Level = Ad.Level;
                AdViewModel.Age = Ad.Age;
                AdViewModel.TimePeriod = Ad.TimePeriod;
                AdViewModel.TimePeriodID = Ad.TimePeriodID;
                AdViewModel.Description = Ad.Description;
                AdViewModel.Price = Ad.Price;
                AdViewModel.Country = Ad.Country;
                AdViewModel.CountryID = Ad.CountryID;
                AdViewModel.StateID = Ad.StateID;
                AdViewModel.State = Ad.State;
                AdViewModel.CityID = Ad.CityID;
                AdViewModel.City = Ad.City;
                AdViewModel.ClientID = Ad.ClientID;
                AdViewModel.Client = Ad.Client;

                AdViewModel.Images = Ad.Images;

                AdViewModel.Reviews = Ad.Reviews;

                AdViewModels.Add(Ad);
            }

            homeViewModel.Ads = (ICollection<Ad>)AdViewModels;

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "BouNanny is a user-friendly and efficient platform designed to connect" +
                              " parents with qualified babysitters effortlessly. This application empowers" +
                              " users to create and post detailed advertisements, providing a comprehensive" +
                              " overview of their specific childcare needs. Whether you're a parent in search" +
                              " of a trustworthy babysitter for a delicate job or a caregiver looking for opportunities, BouNanny" +
                              " streamlines the process, fostering a secure and confidential environment for" +
                              " both parties. With a simple and intuitive interface, users can easily navigate," +
                              " explore listings of offers, and connect with potential clients for further communication.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you got any problems related to this application functionality" +
                              " while using it, you can contact us.";
            //ViewBag.Message = "https://commission.europa.eu/strategy-and-policy/policies/justice-and-fundamental-rights/criminal-justice/rights-suspects-and-accused_en";

            return View();
        }
    }
}