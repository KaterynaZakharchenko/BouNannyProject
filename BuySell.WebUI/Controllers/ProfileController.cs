using BouNanny.DAL.Data;
using BouNanny.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BouNanny.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //instead of showing him bad request, why not show user, his own profile :D
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                string CurrentUserName = User.Identity.GetUserName();
                id = db.Clients.Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities.Where(s => s.StateID == client.StateID), "ID", "Name", client.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", client.CountryID);
            ViewBag.StateID = new SelectList(db.States.Where(s => s.CountryID == client.CountryID), "ID", "Name", client.StateID);
            return View(client);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Username,Name,CountryID,StateID,CityID,MobileNumber,JoinDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", client.CityID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", client.CountryID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", client.StateID);
            return View(client);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
