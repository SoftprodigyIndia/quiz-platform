using Excel;
using QuizApps.Models.quiz;
using QuizApps.Models.Score;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuizApps.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult TakeQuiz()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Instruction(Int32 subId)
        {
            TempData["SubId"] = subId;
            TempData.Keep("SubId");
            return PartialView("~/Views/Home/Instruction.cshtml");
        }


        [HttpGet]
        public ActionResult StartQuiz()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult StartQuiz(takeQuiz quiz)
        {
            return View();
        }

        public ActionResult GetScore(Int32 correctAnswers, string totalTime, DateTime today, Int32 currentQuestion, Int32 quesId)
        {
            Int32 uid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"]);
                mocktestEntities1 db = new mocktestEntities1();
                ScoreDetail newscore = new ScoreDetail();
                newscore.QuesDetailId = quesId;
                newscore.UserId = uid;
                newscore.Corrected = correctAnswers;
                newscore.date = today;
                newscore.Duration = totalTime;
                newscore.Score = correctAnswers;
                newscore.Attempted = currentQuestion;
                newscore.Active = true;

                db.ScoreDetails.Add(newscore);
                db.SaveChanges();
            }
            return PartialView("~/Views/Home/GetScore.cshtml");
        }
        [HttpGet]
        public ActionResult UserList()
        {
          
            //int pageSize = 20;
            //int pageNo = pageSize * (no - 1);
            return View();
           
        }
        
        [HttpGet]
        public ActionResult UserArea()
        {
            scoreDetails obj = new scoreDetails();
            using (mocktestEntities1 selectStatement = new mocktestEntities1())
            {
                var userList = selectStatement.QuesDetails.Join(selectStatement.ScoreDetails, ques => ques.QuesDetailId, score => score.QuesDetailId, (ques, score) => new { ques, score }).Join(selectStatement.SubTopics, a => a.ques.SubTopicId, sub => sub.SubTopicId, (a, sub) => new { a, sub }).Join(selectStatement.Topics, b => b.sub.TopicId, top => top.TopicId, (b, top) => new { b, top }).Join(selectStatement.users, c => c.b.a.score.UserId, us => us.UserId, (c, us) => new { c, us }).Where(a => a.c.b.a.ques.Active == true).ToList();
                obj.scoreGrid = userList.Select(c => new GetScore
                {
                    today = c.c.b.a.score.date,
                    user = c.us.Name,
                    RollNo = c.us.RollNo,
                    Branch = c.us.Branch,
                    topicname = c.c.top.Name,
                    subname = c.c.b.sub.Name,
                    score = Convert.ToInt32(c.c.b.a.score.Score),
                    Attempted = c.c.b.a.score.Attempted,
                    correctAnswers = c.c.b.a.score.Corrected,
                    totalTime = c.c.b.a.score.Duration,
                }).ToList();
                obj.scoreGrid = obj.scoreGrid.Reverse();
                //interval = interval + 20;
                
                return Json(obj.scoreGrid,JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateQuiz()
        {
            

            //createOption obj = new createOption();

            //IEnumerable<Topic> topicName = new List<Topic>();
            //IEnumerable<SubTopic> subName = new List<SubTopic>();
            //topicName = getTopic();
            //subName = getSub();

            //obj.TopicList = new SelectList(topicName, "TopicId", "Name");

            //return View(obj);
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateQuiz(createOption ques)
        {
            mocktestEntities1 db = new mocktestEntities1();

            List<Topic> topicName = new List<Topic>();
            List<SubTopic> subName = new List<SubTopic>();
            List<QuesDetail> questList = new List<QuesDetail>();
            bool Active = true;

            QuesDetail newQuestion = new QuesDetail();
            OptionDetail newOption = new OptionDetail();
            int quesId = 0;

            topicName = db.Topics.ToList();
            subName = db.SubTopics.ToList();
            if (QuestionIsVaild(ques.question.Question,ques.SubId))
            {
               
                Int32 Id = ques.SubId;
                newQuestion.SubTopicId = Id;
                string Queval=ques.question.Question.Replace("&lt", "<");
                string Queval1= Queval.Replace("&gt", ">");
                newQuestion.Question = Queval1;
                newQuestion.OpCorrect = ques.question.optionCorrect;
                newQuestion.Active = Active;

                newOption.OpOne = ques.optionOne;
                newOption.OpTwo = ques.optionTwo;
                newOption.OpThree = ques.optionThree;
                newOption.OpFour = ques.optionFour;
                db.QuesDetails.Add(newQuestion);

                db.SaveChanges();
                var QSTID = newQuestion.QuesDetailId;

                //using (mocktestEntities1 select = new mocktestEntities1())
                //{
                //    var quid = select.QuesDetails.Where(a => a.Question == Queval1 & a.Active == true).FirstOrDefault();
                //    quesId = quid.QuesDetailId;
                //}
                newOption.QuesDetailId = QSTID;
                newOption.Active = Active;
                db.OptionDetails.Add(newOption);
                db.SaveChanges();

                //return View();
                return Json(new { result = 0, message = "Success" });
            }
            //TempData["alertMessage"] = "Question Already Exist!";
            //return View();
            return Json(new { result = 1, message = "Question Already Exist!" });

        }
        [HttpGet]
        public ActionResult AddTopic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTopic(string TopicName)
        {
            mocktestEntities1 mock = new mocktestEntities1();

            var trim = TopicName.TrimStart();
            Topic newTopic = new Topic();
            using (mocktestEntities1 select = new mocktestEntities1())
            {
                var count = select.Topics.Where(a => a.Name == trim);
                if (count.Count() < 1)
                {
                    newTopic.Name = TopicName;
                    newTopic.Active = true;
                    mock.Topics.Add(newTopic);
                    mock.SaveChanges();
                }
                else
                {

                }
            }

            return View();
        }

        public ActionResult DeleteTopic(Int32 TopicDelId)
        {
            Topic Active;
            bool result = false;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Active = db.Topics.Where(a => a.TopicId == TopicDelId).FirstOrDefault<Topic>();

            }
            if (Active != null)
            {
                Active.Active = false;
            }
            using (mocktestEntities1 db2 = new mocktestEntities1())
            {
                db2.Entry(Active).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditConfTop(Int32 TopicEditId, string TopicName)
        {
            bool result = false;
            Topic Name;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Name = db.Topics.Where(a => a.TopicId == TopicEditId).FirstOrDefault<Topic>();
            }
            if (Name != null)
            {
                Name.Name = TopicName;
            }
            using (mocktestEntities1 db2 = new mocktestEntities1())
            {
                db2.Entry(Name).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTopic(Int32 TopicEditId)
        {
            string resultName;
            Topic Name;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Name = db.Topics.Where(a => a.TopicId == TopicEditId).FirstOrDefault<Topic>();
                resultName = Name.Name;
                return Json(resultName, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult AddSub()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSub(Int32 TopId, string SubTopicName)
        {
            mocktestEntities1 mock = new mocktestEntities1();
            List<Topic> Tp = new List<Topic>();
            SubTopic newsubT = new SubTopic();
            var trim = SubTopicName.TrimStart();
            using (mocktestEntities1 select = new mocktestEntities1())
            {
                var count = select.SubTopics.Where(a => a.Name == trim);
                if (count.Count() < 1)
                {
                    newsubT.Name = SubTopicName;
                    newsubT.Active = true;
                    newsubT.TopicId = TopId;
                    mock.SubTopics.Add(newsubT);
                    mock.SaveChanges();
                }
                else
                {

                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSub(Int32 SubDelId)
        {
            SubTopic Active;
            bool result = false;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Active = db.SubTopics.Where(a => a.SubTopicId == SubDelId).FirstOrDefault<SubTopic>();

            }
            if (Active != null)
            {
                Active.Active = false;
            }
            using (mocktestEntities1 db2 = new mocktestEntities1())
            {
                db2.Entry(Active).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditConfSub(Int32 SubEditId, string SubName)
        {
            bool result = false;
            SubTopic Name;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Name = db.SubTopics.Where(a => a.SubTopicId == SubEditId).FirstOrDefault<SubTopic>();
            }
            if (Name != null)
            {
                Name.Name = SubName;
            }
            using (mocktestEntities1 db2 = new mocktestEntities1())
            {
                db2.Entry(Name).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSub(Int32 SubEditId)
        {
            string resultName;
            SubTopic Name;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Name = db.SubTopics.Where(a => a.SubTopicId == SubEditId).FirstOrDefault<SubTopic>();
                resultName = Name.Name;
                return Json(resultName, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GettopicList()
        {
            if (Session["EmailId"] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            createOption obj = new createOption();
            IEnumerable<Topic> TopicName = new List<Topic>();
            TopicName = getTopic();
            obj.topicDropdown = TopicName.Select(b => new SelectListItem
                {
                    Value = b.TopicId.ToString(),
                    Text = b.Name
                }).ToList();
            return Json(obj.topicDropdown, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetsubList(int TopicId)
        {
            if (Session["EmailId"] == null)
            {
                return Json(null,JsonRequestBehavior.AllowGet);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            createOption obj = new createOption();
            List<SubTopic> SubName = new List<SubTopic>();

            List<string> subList = new List<string>();
            int topiciD = Convert.ToInt32(TopicId);

            if (Session["RoleId"] != null)
            {
                using (mocktestEntities1 selectStatement = new mocktestEntities1())
                {
                    SubName = (selectStatement.SubTopics.Where(x => x.TopicId == topiciD & x.Active == true)).ToList();
                    obj.SubList = SubName.Select(a => new SelectListItem
                    {
                        Value = a.SubTopicId.ToString(),
                        Text = a.Name
                    }).ToList();
                    return Json(obj.SubList, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                using (mocktestEntities1 mkt = new mocktestEntities1())
                {
                    SubName = (from t in mkt.Topics join s in mkt.SubTopics on t.TopicId equals s.TopicId join q in mkt.QuesDetails on s.SubTopicId equals q.SubTopicId where (from r in mkt.QuesDetails where r.SubTopicId == q.SubTopicId select r).Count() > 19 & t.TopicId == topiciD & s.Active == true select s).Distinct().ToList();
                    obj.SubList = SubName.Select(a => new SelectListItem
                    {
                        Value = a.SubTopicId.ToString(),
                        Text = a.Name
                    }).ToList();
                    return Json(obj.SubList, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult GetQuestions(Int32 subId, int? Page)
        {
            createOption obj = new createOption();
            List<QuesDetail> QuestionName = new List<QuesDetail>();
            //List<OptionDetail> OptionName = new List<OptionDetail>();
            Int32 subiD = subId;
            int pageSize = 8;
            bool Active = true;
            int pageNumber = (Page ?? 1);
            if (Session["RoleId"] != null)
            {
                using (mocktestEntities1 selectStatement = new mocktestEntities1())
                {
                    var quesdetail = selectStatement.QuesDetails.Join(selectStatement.OptionDetails, a => a.QuesDetailId, b => b.QuesDetailId, (a, b) => new { a, b })
        .Join(selectStatement.SubTopics, a => a.a.SubTopicId, b => b.SubTopicId, (a, b) => new { a, b }).Where(a => a.a.a.SubTopicId == subId & a.a.a.Active == Active).ToList();
                    obj.QuestionGrid = quesdetail.Select(c => new ShowQuiz
                        {
                            Qid = c.a.a.QuesDetailId,
                            OpId = c.a.b.OptionDetailsId,
                            QuestionName = c.a.a.Question.ToString(),
                            OpOne = c.a.b.OpOne.ToString(),
                            OpTwo = c.a.b.OpTwo.ToString(),
                            OpThree = c.a.b.OpThree.ToString(),
                            OpFour = c.a.b.OpFour.ToString(),
                            Correct = c.a.a.OpCorrect.ToString()

                        }).ToList();
                    return Json(obj.QuestionGrid, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                using (mocktestEntities1 selectStatement = new mocktestEntities1())
                {
                    if (TempData["SubId"] != null)
                    {
                        subId = Convert.ToInt32(TempData["SubId"]);
                    }
                    var quesdetail = selectStatement.QuesDetails.Join(selectStatement.OptionDetails, a => a.QuesDetailId, b => b.QuesDetailId, (a, b) => new { a, b })
       .Join(selectStatement.SubTopics, a => a.a.SubTopicId, b => b.SubTopicId, (a, b) => new { a, b }).Where(a => a.a.a.SubTopicId == subId & a.a.a.Active == Active).OrderBy(r => Guid.NewGuid()).Take(20).ToList();
                    obj.QuestionGrid = quesdetail.Select(c => new ShowQuiz
                    {
                        Qid = c.a.a.QuesDetailId,
                        OpId = c.a.b.OptionDetailsId,
                        QuestionName = c.a.a.Question.ToString(),
                        OpOne = c.a.b.OpOne.ToString(),
                        OpTwo = c.a.b.OpTwo.ToString(),
                        OpThree = c.a.b.OpThree.ToString(),
                        OpFour = c.a.b.OpFour.ToString(),
                        Correct = c.a.a.OpCorrect.ToString()

                    }).ToList();
                    return Json(obj.QuestionGrid, JsonRequestBehavior.AllowGet);

                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ShowQuiz()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int QId)
        {
            createOption objcreateOption = new createOption();


            using (mocktestEntities1 db = new mocktestEntities1())
            {
                var option = db.OptionDetails.FirstOrDefault(a => a.QuesDetailId == QId);
                if (option != null)
                {
                    objcreateOption.question.EditQuestion = option.QuesDetail.Question;
                    objcreateOption.question.optionCorrect = option.QuesDetail.OpCorrect;
                    objcreateOption.optionOne = option.OpOne;
                    objcreateOption.optionTwo = option.OpTwo;
                    objcreateOption.optionThree = option.OpThree;
                    objcreateOption.optionFour = option.OpFour;
                    objcreateOption.Qid = QId;
                }
            }
            return Json(objcreateOption, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(createOption option)
        {
            OptionDetail Op = new OptionDetail();
            QuesDetail ques = new QuesDetail();
            using (var ctx = new mocktestEntities1())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Op = ctx.OptionDetails.Where(s => s.QuesDetailId == option.Qid).FirstOrDefault<OptionDetail>();
                ques = ctx.QuesDetails.Where(a => a.QuesDetailId == option.Qid).FirstOrDefault<QuesDetail>();
            }
            ques.Question = option.question.EditQuestion;
            ques.OpCorrect = option.question.optionCorrect;
            Op.OpOne = option.optionOne;
            Op.OpTwo = option.optionTwo;
            Op.OpThree = option.optionThree;
            Op.OpFour = option.optionFour;

            using (mocktestEntities1 selectStatement = new mocktestEntities1())
            {
                selectStatement.Entry(ques).State = System.Data.Entity.EntityState.Modified;
                selectStatement.Entry(Op).State = System.Data.Entity.EntityState.Modified;
                selectStatement.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int Qid)
        {
            bool result = false;
            QuesDetail Active1;
            OptionDetail Active2;
            using (mocktestEntities1 db = new mocktestEntities1())
            {
                Active1 = db.QuesDetails.Where(a => a.QuesDetailId == Qid).FirstOrDefault<QuesDetail>();
                Active2 = db.OptionDetails.Where(a => a.QuesDetailId == Qid).FirstOrDefault<OptionDetail>();
            }
            if (Active1 != null && Active2 != null)
            {
                Active1.Active = false;
                Active2.Active = false;
            }
            using (mocktestEntities1 db2 = new mocktestEntities1())
            {
                db2.Entry(Active1).State = System.Data.Entity.EntityState.Modified;
                db2.Entry(Active2).State = System.Data.Entity.EntityState.Modified;
                db2.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadExcel()
        {
            string path = Server.MapPath("/ExcelFile/QuestionDetails.xlsx");
            return File(path, "application/vnd.ms-excel", "QuestionDetails.xlsx");
        }

        public IEnumerable<Topic> getTopic()
        {
            mocktestEntities1 db = new mocktestEntities1();
            List<Topic> topicName = new List<Topic>();
            if (Session["RoleId"] != null)
            {
                topicName = db.Topics.Where(x => x.Active == true).ToList();
                return topicName;
            }
            else
            {
                using (mocktestEntities1 mkt = new mocktestEntities1())
                {

                    topicName = (from t in mkt.Topics join s in mkt.SubTopics on t.TopicId equals s.TopicId join q in mkt.QuesDetails on s.SubTopicId equals q.SubTopicId where q.QuesDetailId != 0 & t.Active == true select t).Distinct().ToList();
                    return topicName;
                }

            }

        }
        public IEnumerable<SubTopic> getSub()
        {
            mocktestEntities1 db = new mocktestEntities1();
            List<SubTopic> subName = new List<SubTopic>();
            subName = db.SubTopics.ToList();
            return subName;
        }
        public List<OptionDetail> getOption(int QuesId)
        {
            int quesiD = Convert.ToInt32(QuesId);
            using (mocktestEntities1 selectStatement = new mocktestEntities1())
            {
                List<OptionDetail> OptionName = new List<OptionDetail>();
                OptionName = (selectStatement.OptionDetails.Where(x => x.QuesDetailId == QuesId)).ToList();
                return OptionName;
            }

        }

        public bool QuestionIsVaild(string name, Int32 SubID)
        {

            List<QuesDetail> quesname = new List<QuesDetail>();
            using (mocktestEntities1 mk = new mocktestEntities1())
            {
                quesname = mk.QuesDetails.Where(x => x.Question == name & x.Active == true & x.SubTopicId == SubID).ToList();
                if (quesname.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}