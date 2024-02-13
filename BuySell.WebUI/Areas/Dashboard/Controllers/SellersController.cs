using BouNanny.Contracts.Repositories;
using BouNanny.DAL.Data;
using BouNanny.DAL.Repository;
using BouNanny.Models;
using BouNanny.WebUI.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BouNanny.WebUI.Areas.Dashboard.Controllers
{
    public class ClientsController : Controller
    {
        private DataContext myDataContext = new DataContext();
        IRepositoryBase<Client> Clients;

        public ClientsController()
        {
            Clients = new ClientsRepository(myDataContext);
        }

        // GET: Dashboard/Clients
        public ActionResult Index()
        {
            var clients = Clients.GetAll().OrderByDescending(s => s.JoinDate).Take(50).ToList();
            return View(clients);
        }

        // GET: Dashboard/Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = Clients.GetByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Dashboard/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = Clients.GetByID(id);
            Clients.Delete(client);
            Clients.Commit();

            // Now also delete the corresponding user from Users table.
            //because we have a shit of 2 different tables for users & clients.

            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            var user = applicationDbContext.Users.Where(u => u.UserName == client.Username).FirstOrDefault();
            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();

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
