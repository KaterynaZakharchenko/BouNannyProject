using BouNanny.Contracts.Repositories;
using BouNanny.DAL.Data;
using BouNanny.DAL.Repository;
using BouNanny.Models;
//using BouNanny.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BouNanny.WebUI.Controllers
{
    public class AdsController : Controller
    {
        //Main DataContext object to be sent to Repository
        DataContext myDataContext = new DataContext();

        //Create Repositories for DataAccess
        IRepositoryBase<Client> Clients;
        IRepositoryBase<Ad> Ads;
        IRepositoryBase<Level> Levels;
        IRepositoryBase<TimePeriod> TimePeriods;
        IRepositoryBase<Country> Countries;
        IRepositoryBase<State> States;
        IRepositoryBase<City> Cities;
        IRepositoryBase<Image> Images;
        IRepositoryBase<Review> Reviews;

        public AdsController()
        {
            //Create Repositories for DataAccess
            Ads = new AdsRepository(myDataContext);
            Clients = new ClientsRepository(myDataContext);
            Images = new ImagesRepository(myDataContext);
            Levels = new LevelsRepository(myDataContext);
            TimePeriods = new TimePeriodsRepository(myDataContext);
            Countries = new CountriesRepository(myDataContext);
            States = new StatesRepository(myDataContext);
            Cities = new CitiesRepository(myDataContext);
            Reviews = new ReviewsRepository(myDataContext);
        }

        // GET: Ads
        public ActionResult Index()
        {
            //This is the main page for Ads.
            //we will get records from Database into this page.

            //Get All Ads as List
            //Aditionally I only want to get last 12 Ads in Descending Order so as not to bombard the Index Page
            List<Ad> AdsList = Ads.GetAll().OrderByDescending(b => b.PostingTime).Take(12).ToList();

            //There will be many Ads so we created a whole List of AdViewModel
            List<Ad> AdViewModels = new List<Ad>();

            //Now Iterate over this List of Ads to populate AdViewModels List
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
                AdViewModel.Sex = Ad.Sex;
                AdViewModel.TimePeriod = Ad.TimePeriod;
                AdViewModel.TimePeriodID = Ad.TimePeriodID;
                AdViewModel.StartingTime = Ad.StartingTime;
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

                //For images we will iterate through Images from Database & see which Images belong to this Ad
                //then we will send this Images List to AdViewModel.Images
                //-JUST LEAVE IT- bcz i think this will increase load times. 
                AdViewModel.Images = Ad.Images;

                //Get the reviews from Ads Ads Review relation
                AdViewModel.Reviews = Ad.Reviews;

                //now add this viewmodel in List
                AdViewModels.Add(AdViewModel);
            }

            //Now return this list of AdViewModel to View
            return View(AdViewModels);

        }
        // GET: Ads/Details/5
        public ActionResult Details(int? id)
        {
            //on this page user will be looking for details of a Ad by sending an ID

            //first check if there is any ID given with this page 
            if (id == null)
            {
                //return Bad Request if Ads/Details/ & No ID in URL
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get the Ad by Given ID
            Ad Ad = Ads.GetByID(id);

            //Check if Ad Exists
            if (Ad == null)
            {
                //if not exists then Show 404 Not found
                return HttpNotFound();
            }

            //if Ad is found then Populate the AdViewModel
            //create an object
            Ad AdViewModel = new Ad();

            //Populate it from Ad Details
            AdViewModel.ID = Ad.ID;
            AdViewModel.Title = Ad.Title;
            AdViewModel.Level = Ad.Level;
            AdViewModel.LevelID = Ad.LevelID;
            AdViewModel.Age = Ad.Age;
            AdViewModel.Sex = Ad.Sex;
            AdViewModel.TimePeriod = Ad.TimePeriod;
            AdViewModel.TimePeriodID = Ad.TimePeriodID;
            AdViewModel.Description = Ad.Description;
            AdViewModel.Price = Ad.Price;
            AdViewModel.Country = Ad.Country;
            AdViewModel.CountryID = Ad.CountryID;
            AdViewModel.State = Ad.State;
            AdViewModel.StateID = Ad.StateID;
            AdViewModel.City = Ad.City;
            AdViewModel.CityID = Ad.CityID;
            AdViewModel.Client = Ad.Client;
            AdViewModel.ClientID = Ad.ClientID;
            AdViewModel.PostingTime = Ad.PostingTime;

            //now for Image we will go back to Images Repository & match AdID there.
            AdViewModel.Images = Images.GetAll().Where(i => i.AdID == Ad.ID).ToList();

            //now for Reviews
            AdViewModel.Reviews = Reviews.GetAll().Where(i => i.AdID == Ad.ID).ToList();

            return View(AdViewModel);
        }

        // GET: Ads/Create
        [Authorize]
        public ActionResult Create()
        {
            //Displaying the form for user to create Ad
            //We have to populate the Create form for those DropDown & User Details like Country, State & City in their already.

            //First get Current User details so we can know about Client
            string CurrentUserName = User.Identity.GetUserName();
            Client client = Clients.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();

            //Create a Model & populate then send it to the View
            Ad AdViewModel = new Ad();
            AdViewModel.CountryID = client.CountryID;
            AdViewModel.Country = client.Country;
            AdViewModel.State = client.State;
            AdViewModel.StateID = client.StateID;
            AdViewModel.City = client.City;
            AdViewModel.CityID = client.CityID;
            AdViewModel.Client = client;
            AdViewModel.ClientID = client.ID;

            //Populate Lists in ViewModel to be shown on View
            AdViewModel.LevelsList = Levels.GetAll();
            AdViewModel.TimePeriodsList = TimePeriods.GetAll();
            AdViewModel.CountriesList = Countries.GetAll();
            AdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == client.CountryID);
            AdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == client.StateID);

            return View(AdViewModel);
        }
        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,LevelID,Age,Sex,StartingTime,SelectedLevelID,TimePeriodID,Description,Price,CountryID,StateID,CityID")] Ad AdViewModel, HttpPostedFileBase ImageFile)
        {
            //User has submitted the Create Form with AdViewModel details & maybe ImageFile
            //We have to get the details from AdViewModel into Ad, Ad & Image Models

            if (ModelState.IsValid)
            {
                //New Ad Object to be populated & then Added into Repository
                Ad ad = new Ad();

                //populate this Ad Object
                ad.Title = AdViewModel.Title;
                ad.TimePeriodID = AdViewModel.TimePeriodID;
                ad.Description = AdViewModel.Description;
                ad.Price = AdViewModel.Price;
                ad.CountryID = AdViewModel.CountryID;
                ad.StateID = AdViewModel.StateID;
                ad.CityID = AdViewModel.CityID;
                ad.LevelID = AdViewModel.LevelID;
                ad.Age = AdViewModel.Age;
                ad.Sex = AdViewModel.Sex;
                ad.ID = ad.ID;

                //Get Current User Details to be send as Client Details in Ad Object
                string CurrentUserName = User.Identity.GetUserName();
                ad.ClientID = Clients.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                ad.Slug = AdViewModel.Title.Replace(' ', '-');
                ad.PostingTime = DateTime.Now;

                //Now Add this ad object in our Ads Repository
                Ads.Insert(ad);
                Ads.Commit();

                //Now Check if user has submitted a legal Image file
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    //We have to store this image in our Database as Image Path & then save it in our Images table with AdID

                    //Get Image Path
                    var uploadDir = "~/images";
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    //Save this image in our images folder
                    ImageFile.SaveAs(ImagePath);

                    //Create Image object & add AdID from ad object
                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    //Insert this Image object in Database
                    Images.Insert(image);
                    Images.Commit();
                }

                //If Ad is added to Database we goto Ads/Index page to view all ads.
                //we can change it to show us the page of our add too - Recomended
                return RedirectToAction("Details", "Ads", new { id = ad.ID });
            }

            //if ModelState is not in good & Valid we come here
            //Populate Lists in ViewModel to be shown on View
            //ViewBag.LevelID = new SelectList(Levels, "ID", "Name", AdViewModel.LevelID);
            ViewBag.LevelsList = new List<SelectListItem> {new SelectListItem{Text="Option 1", Value="1"},new SelectListItem{Text="Option 2", Value="2"}};
            //ViewBag.LevelsList = new List<SelectListItem>{Levels.GetAll(), "ID", "Name", AdViewModel.LevelID};
            //AdViewModel.LevelsList = Levels.GetAll();
            AdViewModel.TimePeriodsList = TimePeriods.GetAll();
            AdViewModel.CountriesList = Countries.GetAll();
            AdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == AdViewModel.CountryID);
            AdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == AdViewModel.StateID);

            
            //return the same AdViewModel back
            return View(AdViewModel);
        }

        // GET: Ads/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
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
            Ad AdViewModel = new Ad();

            AdViewModel.ID = Ad.ID;
            AdViewModel.Title = Ad.Title;
            AdViewModel.Level = Ad.Level;
            AdViewModel.LevelID = Ad.LevelID;
            AdViewModel.Age = Ad.Age;
            AdViewModel.Sex = Ad.Sex;
            AdViewModel.TimePeriod = Ad.TimePeriod;
            AdViewModel.TimePeriodID = Ad.TimePeriodID;
            AdViewModel.Description = Ad.Description;
            AdViewModel.Price = Ad.Price;
            AdViewModel.Country = Ad.Country;
            AdViewModel.CountryID = Ad.CountryID;
            AdViewModel.State = Ad.State;
            AdViewModel.StateID = Ad.StateID;
            AdViewModel.City = Ad.City;
            AdViewModel.CityID = Ad.CityID;

            //Populate Lists in ViewModel to be shown on View
            AdViewModel.LevelsList = Levels.GetAll();
            AdViewModel.TimePeriodsList = TimePeriods.GetAll();
            AdViewModel.CountriesList = Countries.GetAll();
            AdViewModel.StatesList = States.GetAll().Where(c => c.CountryID == Ad.CountryID);
            AdViewModel.CitiesList = Cities.GetAll().Where(c => c.StateID == Ad.StateID);

            return View(AdViewModel);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,LevelID,Age,Sex,StartingTime,TimePeriodID,Description,Price,CountryID,StateID,CityID")] Ad AdViewModel, HttpPostedFileBase ImageFile)
        {
            //Now user has submitted the Edit page with his new details 
            if (ModelState.IsValid)
            {
                //since this Ad already exists we will get it by the ID from AdViewModel that we got in parameter
                Ad Ad = Ads.GetByID(AdViewModel.ID);

                //now update this Ad fields from the Model
                Ad.LevelID = AdViewModel.LevelID;
                Ad.Level = AdViewModel.Level; // <------------------------This might give error because the view may send a model that may not have this Level
                Ad.Age = AdViewModel.Age;
                Ad.Sex = AdViewModel.Sex;

                //Update this Ad in Database
                Ads.Update(Ad);
                Ads.Commit();

                //Now if the user updated any Ad fields
                //Get an Ad object from Databse since it already exists there.
                Ad ad = Ads.GetByID(Ad.ID);
                //update this object from the Model we got
                ad.Title = AdViewModel.Title;
                ad.TimePeriodID = AdViewModel.TimePeriodID;
                ad.TimePeriod = AdViewModel.TimePeriod;
                ad.Description = AdViewModel.Description;
                ad.Price = AdViewModel.Price;
                ad.CountryID = AdViewModel.CountryID;
                ad.Country = AdViewModel.Country;
                ad.StateID = AdViewModel.StateID;
                ad.State = AdViewModel.State;
                ad.CityID = AdViewModel.CityID;
                ad.City = AdViewModel.City;
                ad.Slug = AdViewModel.Title.Replace(' ', '-');

                //Now update this object in database
                Ads.Update(ad);
                Ads.Commit();

                //Now if there is new updated image with form, 
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    //we have to do 2 things here
                    //1- Delete the previous image record from image table & images folder
                    //2- Save the new image & its record in images table

                    var uploadDir = "~/images";

                    //check if the ad have any images
                    if (ad.Images != null && ad.Images.Count > 0)
                    {
                        //get the first image from this ad
                        //i am not getting a list here bcz the delete method that i have do not get a list
                        //Yes I think i can loop through the list if i get & dlete one by one --- but choro yar. esy he theek hy.
                        Image oldimage = ad.Images.First();

                        //1- Delete the previous record
                        Images.Delete(oldimage);
                        Images.Commit();

                        //Now delete the previous image from images directory
                        var OldImagePath = Path.Combine(Server.MapPath(uploadDir), oldimage.Path);

                        System.IO.File.Delete(OldImagePath);
                    }

                    //2 - Lets update the new image
                    var NewImageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(Server.MapPath(uploadDir), NewImageName);

                    //Upload the image in our images diretory
                    ImageFile.SaveAs(ImagePath);

                    //create new Image object
                    var image = new Image
                    {
                        Path = NewImageName, //I am saving NewImageName in path because we will use relative path in img tag like ~\images\Model.Images.First().Path etc 
                        AdID = ad.ID
                    };

                    //add this image object in Database
                    Images.Insert(image);
                    Images.Commit();
                }

                //get user back to the details page of this Ads
                return RedirectToAction("Details", "Ads", new { id = Ad.ID });
            }

            //ViewBag.LevelID = new SelectList(db.Levels, "ID", "Name", AdViewModel.LevelID);
            //ViewBag.CityID = new SelectList(db.Cities, "ID", "Name", AdViewModel.CityID);
            //ViewBag.TimePeriodID = new SelectList(db.TimePeriods, "ID", "TimePeriodType", AdViewModel.TimePeriodID);
            //ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", AdViewModel.CountryID);
            //ViewBag.StateID = new SelectList(db.States, "ID", "Name", AdViewModel.StateID);
            //IRepositoryBase<Level> Levels = new LevelsRepository(myDataContext);
            //IRepositoryBase<TimePeriod> TimePeriods = new TimePeriodsRepository(myDataContext);
            //IRepositoryBase<Country> Countries = new CountriesRepository(myDataContext);

            //ViewBag.LevelID = new SelectList(Levels.GetAll(), "ID", "Name", AdViewModel.LevelID);
            //ViewBag.TimePeriodID = new SelectList(TimePeriods.GetAll(), "ID", "TimePeriodType", AdViewModel.TimePeriodID);
            //ViewBag.CountryID = new SelectList(Countries.GetAll(), "ID", "Name", AdViewModel.CountryID);

            return View(AdViewModel);
        }

        // GET: Ads/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            //User is trying to delete this object

            //check if the ID supplied is not null
            if (id == null)
            {
                //return bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get te Ad from database by the supplied ID
            Ad Ad = Ads.GetByID(id);

            //see if this Ad exists
            if (Ad == null)
            {
                //if not then 404 Not Found
                return HttpNotFound();
            }

            //since the Ad exists now we populate the view model from it
            Ad AdViewModel = new Ad();

            AdViewModel.ID = Ad.ID;
            AdViewModel.Title = Ad.Title;
            AdViewModel.LevelID = Ad.LevelID;
            AdViewModel.Level = Ad.Level;
            AdViewModel.Age = Ad.Age;
            AdViewModel.Sex = Ad.Sex;
            AdViewModel.TimePeriodID = Ad.TimePeriodID;
            AdViewModel.TimePeriod = Ad.TimePeriod;
            AdViewModel.Description = Ad.Description;
            AdViewModel.Price = Ad.Price;
            AdViewModel.CountryID = Ad.CountryID;
            AdViewModel.Country = Ad.Country;
            AdViewModel.StateID = Ad.StateID;
            AdViewModel.State = Ad.State;
            AdViewModel.CityID = Ad.CityID;
            AdViewModel.City = Ad.City;
            AdViewModel.ClientID = Ad.ClientID;
            AdViewModel.Client = Ad.Client;

            //send this model to User for confirmation to delete
            //i think we should not have this view altogether. why show this whole form
            //we should show user a jquery confirmation & then we delete on the basis of result from there.
            return View(AdViewModel);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            //user has confirmed to delete a record. Delete it right now.

            //get the Ad by supplied ID
            Ad Ad = Ads.GetByID(id);

            //delete from database
            Ads.Delete(Ad);
            Ads.Commit();

            //return to main Ads page
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview(Ad AdViewModel, int? id, string Review)
        {
            if (Review.Length >= 50)
            {
                //Get the Ad Object from the ID by Ad Object
                Ad Ad = Ads.GetByID(id);

                if (Ad == null)
                {
                    return HttpNotFound();
                }

                //Get Current User Details to be send as Client Details in Review Object
                string CurrentUserName = User.Identity.GetUserName();

                Review newReview = new Review();

                newReview.Content = Review;
                newReview.AdID = Ad.ID;
                newReview.Ad = Ad;
                newReview.PostingTime = DateTime.Now;

                Client client = Clients.GetAll().Where(s => s.Username == CurrentUserName).FirstOrDefault();
                newReview.ClientID = client.ID;
                newReview.Client = client;

                newReview.ReviewStars = 5; //for now I dont want to implement this functionality but its good to keep for later.

                Reviews.Insert(newReview);
                Reviews.Commit();

                //Now return the user back to the details of this ad
                return RedirectToAction("Details", "Ads", new { id = id });
            }

            //review was not correct. return back to URL
            return RedirectToAction("Details", "Ads", new { id = id });
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