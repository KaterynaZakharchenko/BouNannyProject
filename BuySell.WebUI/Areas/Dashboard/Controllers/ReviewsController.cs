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
    public class ReviewsController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Review> Reviews;

        public ReviewsController()
        {
            Reviews = new ReviewsRepository(myDataContext);
        }

        // GET: Dashboard/Reviews
        public ActionResult Index()
        {
            var reviews = Reviews.GetAll().OrderByDescending(r => r.PostingTime).Take(50).ToList();
            return View(reviews);
        }

        // GET: Dashboard/Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = Reviews.GetByID(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Dashboard/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = Reviews.GetByID(id);
            Reviews.Delete(review);
            Reviews.Commit();
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
