using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PortalAO.Attributes;
using PortalAO.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalAO.Controllers
{
    public class AdvertisementController : Controller
    {
        private ApplicationDbContext _dbContext;
        private ApplicationUserManager _userManager;
        private ModelsDbContext _dbModels;

        public AdvertisementController()
        {
            _dbContext = ApplicationDbContext.Create();
            _dbModels = new ModelsDbContext();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Advertisement
        public ActionResult Index()
        {
            return View(_dbModels.AdvertisementModels.ToList());
        }

        [HttpPost]
        public ActionResult Join(int id)
        {
            var advertisement = _dbModels.AdvertisementModels.Where(item => item.ID == id).FirstOrDefault();
            int userId = User.Identity.GetUserId<int>();

            if (advertisement.InterestedContractorStorage == string.Empty)
                advertisement.InterestedContractorStorage += userId;
            else if (!advertisement.InterestedContractorIDs.Contains(userId))
                advertisement.InterestedContractorStorage += ";" + userId;

            _dbModels.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Resign(int id)
        {
            var advertisement = _dbModels.AdvertisementModels.Where(item => item.ID == id).FirstOrDefault();
            int userId = User.Identity.GetUserId<int>();

            if (advertisement.InterestedContractorStorage.Contains(userId.ToString()))
                advertisement.InterestedContractorStorage = advertisement.InterestedContractorStorage.Remove(advertisement.InterestedContractorStorage.IndexOf(userId.ToString()), 1);

            _dbModels.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Advertisement/Create
        [AuthorizeUser(Roles = "User")]
        public ActionResult Create()
        {
            return View(new AdvertisementCreateEditViewModel());
        }

        [HttpPost]
        [AuthorizeUser(Roles = "User")]
        public ActionResult Create(AdvertisementCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId<int>());

                AdvertisementModel advertisement = new AdvertisementModel()
                {
                    Title = model.Title,
                    Details = model.Details,
                    ExecutionDate = model.ExecutionDate,
                    Location = model.Location,
                    ApplicationUser = user,
                    Payment = model.Payment
                };
                
                _dbModels.AdvertisementModels.Add(advertisement);
                _dbModels.SaveChanges();
            }
            return new RedirectResult("Index");
        }

        // GET: Advertisement/Edit/5
        [AuthorizeUser(Roles = "User")]
        public ActionResult Edit(int id)
        {
            var advertisement = _dbModels.AdvertisementModels.Where(item => item.ID == id).FirstOrDefault();

            if (advertisement == null)
                return new ViewResult { ViewName = "AdvertisementNotFound" };

            if (advertisement.ApplicationUser.Id == User.Identity.GetUserId<int>())
            {
                var model = new AdvertisementCreateEditViewModel()
                {
                    Title = advertisement.Title,
                    Details = advertisement.Details,
                    ExecutionDate = advertisement.ExecutionDate,
                    Payment = advertisement.Payment,
                    Location = advertisement.Location,
                    ID = advertisement.ID
                };

                return View(model);
            }

            return new ViewResult { ViewName = "Unauthorized" };
        }
    
        [HttpPost]
        [AuthorizeUser(Roles = "User")]
        public ActionResult Edit(AdvertisementCreateEditViewModel model)
        {
            var advertisement = _dbModels.AdvertisementModels.Where(item => item.ID == model.ID).First();

            if (advertisement == null)
                return new ViewResult { ViewName = "AdvertisementNotFound" };

            if (advertisement.ID == User.Identity.GetUserId<int>())
            {
                advertisement.Title = model.Title;
                advertisement.Location = model.Location;
                advertisement.Payment = model.Payment;
                advertisement.ExecutionDate = model.ExecutionDate;
                advertisement.Details = model.Details;

                _dbModels.SaveChanges();
            }

            return View(model);
        }

        // GET: Advertisement/Delete/5
        [AuthorizeUser(Roles = "User")]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
