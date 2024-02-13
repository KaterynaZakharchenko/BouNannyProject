using BouNanny.Contracts.Repositories;
using BouNanny.DAL.Data;
using BouNanny.DAL.Repository;
using BouNanny.Models;
using System.Linq;
using System.Web.Mvc;

namespace BouNanny.WebUI.Controllers
{
    public class AJAXController : Controller
    {
        // GET: AJAX
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FillStates(int Country)
        {
            IRepositoryBase<State> statesRepo = new StatesRepository(new DataContext());
            var states = statesRepo.GetAll().ToList().Where(s => s.CountryID == Country);

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillCities(int State)
        {
            IRepositoryBase<City> citiesRepo = new CitiesRepository(new DataContext());
            var cities = citiesRepo.GetAll().ToList().Where(s => s.StateID == State);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

    }
}