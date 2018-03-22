using BusinessLogic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private DataManager dataManager;

        public AccountController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (dataManager.MembershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неудачная попытка входа на сайт");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status = dataManager.MembershipProvider.CreateUser(
                    model.UserName, model.Password, model.Email, model.FirstName,
                    model.LastName, model.MiddleName);
                if (status == MembershipCreateStatus.Success)
                    return View("Success");
                ModelState.AddModelError("",GetMembershipCreateStatusResultText(status));
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        //создаем текст ошибки для MembershipCreateStatus
        public string GetMembershipCreateStatusResultText(MembershipCreateStatus status)
        {
            switch (status)
            { 
                case MembershipCreateStatus.Success : return "Операция выполнена успешно";
                case MembershipCreateStatus.DuplicateUserName : return "Пользователь с таким логином уже существует";
                case MembershipCreateStatus.DuplicateEmail: return "Пользователь с таким E-mail уже существует";
                default: return "Ошибка";
            }
        }
    }
}
