using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private DataManager dataManager;

        public SearchController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String firstName, String lastName, String middleName)
        {
            List<SearchUserData> model = new List<SearchUserData>();

            foreach (Int32 id in dataManager.Users.SearchUsers(firstName, lastName, middleName))
            {
                model.Add( new SearchUserData
                {
                    Id = id,
                    FirstName = dataManager.Users.GetFirstNameById(id),
                    LastName = dataManager.Users.GetLastNameById(id),
                    MiddleName = dataManager.Users.GetMiddleNameById(id)
                });
            }

            return View(model);
        }

    }
}
