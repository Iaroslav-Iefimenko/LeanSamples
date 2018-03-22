using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using BusinessLogic;
using System.Web.Security;

namespace UnitTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test(Description = "Метод GetMembershipCreateStatusResultText() возвращает текст ошибки для DuplicateEmail")]
        public void GetMembershipCreateStatusResultTextReturnsDuplicateEmailErrorText()
        { 
            //Arrange
            AccountController controller = new AccountController(
                new DataManager(null, null, null, null, null, null, null));

            //Act
            String result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.DuplicateEmail);

            //Accert
            Assert.AreEqual("Пользователь с таким E-mail уже существует", result);
        }

        [Test(Description = "Метод GetMembershipCreateStatusResultText() возвращает текст ошибки для DuplicateUserName")]
        public void GetMembershipCreateStatusResultTextReturnsDuplicateUserNameErrorText()
        {
            //Arrange
            AccountController controller = new AccountController(
                new DataManager(null, null, null, null, null, null, null));

            //Act
            String result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.DuplicateUserName);

            //Accert
            Assert.AreEqual("Пользователь с таким логином уже существует", result);
        }

        [Test(Description = "Метод GetMembershipCreateStatusResultText() возвращает текст ошибки Ошибка")]
        public void GetMembershipCreateStatusResultTextReturnsOtherErrorText()
        {
            //Arrange
            AccountController controller = new AccountController(
                new DataManager(null, null, null, null, null, null, null));

            //Act
            String result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.InvalidEmail);

            //Accert
            Assert.AreEqual("Ошибка", result);
        }
    }
}
