using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Models;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //заменяем стандартную фабрику контроллеров на NinjectControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(PhotoViewModel), new ImageModelBinder("Image"));
            //ModelBinders.Binders.Add(typeof(PhotoViewModel), new ImageModelBinder("Thumbnail"));
        }
    }
}