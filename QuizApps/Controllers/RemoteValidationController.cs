using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApps.Controllers
{
    public class RemoteValidationController : Controller
    {
        // GET: RemoteValidation
        public ActionResult CheckExistingEmail(String EmailId)
        {
            mocktestEntities1 db = new mocktestEntities1();
            user newUser = new user();
            bool IfEmailExist = false;
            List<user> userList = new List<user>();
            int count = 0;
            userList = db.users.ToList();
            try
            {
                if (userList.Count > 0)
                {
                    foreach (var item in userList)
                    {
                        if (item.EmailId.Equals(EmailId))
                        {
                            count++;
                        }
                    }
                }
                if (count != 0)
                {
                    IfEmailExist = true;
                }
                return Json(!IfEmailExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckExistingQuestion([Bind(Prefix = "question.Question")] string Question)
        {
            mocktestEntities1 db = new mocktestEntities1();
            bool IfquestionExist = false;
            int count = 0;
            List<QuesDetail> quesList = new List<QuesDetail>();
            quesList = db.QuesDetails.Where(a => a.Question == Question & a.Active == true).ToList();
            try
            {
                if (quesList.Count > 0)
                {
                           count++;
                       
                }


                if (count != 0)
                {
                    IfquestionExist = true;
                }
                return Json(!IfquestionExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CheckExistingTopic(string TopicName)
        {
            bool IfTopicExist = false;
            using (mocktestEntities1 mock = new mocktestEntities1())
            {
                var validtopic = mock.Topics.Where(a => a.Name == TopicName).ToList();
                if (validtopic.Count != 0)
                {
                    IfTopicExist = true;
                }
                else
                {
                    IfTopicExist = false;
                }
                return Json(!IfTopicExist, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckExistingSubTopic(string SubTopicName, Int32 TopId)
        {
            bool IfTopicExist = false;
            using (mocktestEntities1 mock = new mocktestEntities1())
            {
                var validtopic = mock.SubTopics.Where(a => a.Name == SubTopicName & a.TopicId == TopId).ToList();
                if (validtopic.Count != 0)
                {
                    IfTopicExist = true;
                }
                else
                {
                    IfTopicExist = false;
                }
                return Json(!IfTopicExist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}