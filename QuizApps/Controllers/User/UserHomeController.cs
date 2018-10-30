using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApps.Controllers.User
{
    public class userHomeController : Controller
    {
        // GET: UserHome
        public ActionResult SelectQuiz()
        {
            return View();
        }
    }
}