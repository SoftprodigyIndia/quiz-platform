using QuizApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace QuizApps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(typeof(CustomValidator),
            //  (metadata, controllerContext, attribute) => new CustomAttributes.RequiredAttributeAdapter(metadata,
            //    controllerContext, (CustomValidator)attribute)); 

        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string Roles = string.Empty;
                        using (mocktestEntities1 entities = new   mocktestEntities1())
                        {
                            var user = entities.users.SingleOrDefault(u => u.EmailId == userName);
                            Roles = user.Role.RoleName;
                        }
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userName, "Forms"), Roles.Split(';'));
                    }
                    catch (Exception) { }
                }
            }
        }

    }
}
