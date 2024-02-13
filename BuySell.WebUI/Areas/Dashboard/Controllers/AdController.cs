using BouNanny.Contracts.Repositories;
using BouNanny.DAL.Data;
using BouNanny.DAL.Repository;
using BouNanny.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BouNanny.WebUI.Areas.Dashboard.Controllers
{
    public class AdsController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Ad> Ads;

        public AdsController()
        {
            Ads = new AdsRepository(myDataContext);
        }

        // GET: Dashboard/Ads
        public ActionResult Index()
        {
            var AllAds = Ads.GetAll().OrderByDescending(l => l.PostingTime).Take(50).ToList();

            return View(AllAds);
        }

        // GET: Dashboard/Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad Ad = Ads.GetByID(id);
            if (Ad == null)
            {
                return HttpNotFound();
            }
            return View(Ad);
        }

        // POST: Dashboard/Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad Ad = Ads.GetByID(id);
            Ads.Delete(Ad);
            Ads.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                myDataContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}