using BusinessLogic;
using Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;

            GameResultViewModel model = new GameResultViewModel
            {
                UserId = currentId,
                UserName = dataManager.Users.GetFirstNameById(currentId) + " " + dataManager.Users.GetLastNameById(currentId),
                Settings = new GameSettings
                {
                    MaxFinishedZombies = dataManager.GameSettings.GetMaxFinishedZombies(),
                    MoneyOnStart = dataManager.GameSettings.GetMoneyOnStart(),
                    PeaStepWidth = dataManager.GameSettings.GetPeaStepWidth(),
                    PeaDamage = dataManager.GameSettings.GetPeaDamage(),
                    ZombieCostOfDestroyed = dataManager.GameSettings.GetZombieCostOfDestroyed(),
                    ZombieStepWidth = dataManager.GameSettings.GetZombieStepWidth(),
                    TopShift = dataManager.GameSettings.GetTopShift(),
                    BorderWidth = dataManager.GameSettings.GetBorderWidth(),
                    SquareWidth = dataManager.GameSettings.GetSquareWidth(),
                    SquareHeight = dataManager.GameSettings.GetSquareHeight(),
                    LinesQuantity = dataManager.GameSettings.GetLinesQuantity(),
                    VLinesQuantity = dataManager.GameSettings.GetVLinesQuantity(),
                    MainInterval = dataManager.GameSettings.GetMainInterval(),
                    ZombiesCreateInterval = dataManager.GameSettings.GetZombiesCreateInterval(),
                    BackgroundColor = dataManager.GameSettings.GetBackgroundColor(),
                    BorderColor = dataManager.GameSettings.GetBorderColor(),
                    TextColor = dataManager.GameSettings.GetTextColor(),
                    FontName = dataManager.GameSettings.GetFontName(),
                    DeskColor1 = dataManager.GameSettings.GetDeskColor1(),
                    DeskColor2 = dataManager.GameSettings.GetDeskColor2(),
                    StartText = dataManager.GameSettings.GetStartText(),
                    StartedText = dataManager.GameSettings.GetStartedText(),
                    StartCaptionX = dataManager.GameSettings.GetStartCaptionX(),
                    StartCaptionY = dataManager.GameSettings.GetStartCaptionY(),
                    StartCaptionWidth = dataManager.GameSettings.GetStartCaptionWidth(),
                    StartCaptionHeight = dataManager.GameSettings.GetStartCaptionHeight(),
                    ZombiesFinishedText = dataManager.GameSettings.GetZombiesFinishedText(),
                    ZombiesFinishedCaptionX = dataManager.GameSettings.GetZombiesFinishedCaptionX(),
                    ZombiesFinishedCaptionY = dataManager.GameSettings.GetZombiesFinishedCaptionY(),
                    ZombiesFinishedX = dataManager.GameSettings.GetZombiesFinishedX(),
                    ZombiesFinishedY = dataManager.GameSettings.GetZombiesFinishedY(),
                    MoneyText = dataManager.GameSettings.GetMoneyText(),
                    MoneyCaptionX = dataManager.GameSettings.GetMoneyCaptionX(),
                    MoneyCaptionY = dataManager.GameSettings.GetMoneyCaptionY(),
                    MoneyX = dataManager.GameSettings.GetMoneyX(),
                    MoneyY = dataManager.GameSettings.GetMoneyY(),
                    ZombiesDestroyedText = dataManager.GameSettings.GetZombiesDestroyedText(),
                    ZombiesDestroyedCaptionX = dataManager.GameSettings.GetZombiesDestroyedCaptionX(),
                    ZombiesDestroyedCaptionY = dataManager.GameSettings.GetZombiesDestroyedCaptionY(),
                    ZombiesDestroyedX = dataManager.GameSettings.GetZombiesDestroyedX(),
                    ZombiesDestroyedY = dataManager.GameSettings.GetZombiesDestroyedY(),
                    ZombieImgSrc = dataManager.GameSettings.GetZombieImgSrc(),
                    PeaCanonImgSrc = dataManager.GameSettings.GetPeaCanonImgSrc(),
                    PeaImgSrc = dataManager.GameSettings.GetPeaImgSrc()
                }
            };
            return View(model);
        }

        public ActionResult AddResult(Int32 destroyedZombies)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;
            
            if (ModelState.IsValid)
                dataManager.GameResults.AddGameResult(currentId, destroyedZombies, DateTime.Now);

            return RedirectToAction("GameOver", "Home", new { destroyedZombies });
        }

        public ActionResult BestResults()
        {
            List<GameResultViewModel> model = new List<GameResultViewModel>();

            foreach (Int32 id in dataManager.GameResults.GetTopResults(10))
            {
                Int32 userId = dataManager.GameResults.GetUserIdById(id);
                model.Add(new GameResultViewModel
                {
                    Id = id,
                    DestroyedZombies = dataManager.GameResults.GetDestroyedZombiesById(id),
                    GameDate = dataManager.GameResults.GetDateTimeById(id),
                    UserId = userId,
                    UserName = dataManager.Users.GetFirstNameById(userId) + " " + dataManager.Users.GetLastNameById(userId)
                });
            }

            return View(model); 
        }

        public ActionResult MyBestResults()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;

            List<GameResultViewModel> model = new List<GameResultViewModel>();
            
            foreach (Int32 id in dataManager.GameResults.GetTopResultsForUser(10, currentId))
            {
                model.Add(new GameResultViewModel
                {
                    Id = id,
                    DestroyedZombies = dataManager.GameResults.GetDestroyedZombiesById(id),
                    GameDate = dataManager.GameResults.GetDateTimeById(id),
                    UserId = currentId,
                    UserName = dataManager.Users.GetFirstNameById(currentId) + " " + dataManager.Users.GetLastNameById(currentId)
                });
            }

            return View(model); 
        }

        public ActionResult GameOver(Int32 destroyedZombies)
        {
            return View(destroyedZombies); 
        }
    }
}
