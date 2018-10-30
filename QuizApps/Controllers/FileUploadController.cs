using Excel;
using QuizApps.Models.quiz;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApps.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        [HttpGet]
        public ActionResult UploadExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadExcel(List<HttpPostedFileBase> files, Int32 SubId)
        {
            mocktestEntities1 db = new mocktestEntities1();
            var uploadFile = Request.Files[0];
            List<QuesDetail> questList = new List<QuesDetail>();
            HomeController home = new HomeController();
            int setCount = 0;
            int Qid = 0;
            bool check = false;
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                Stream stream = uploadFile.InputStream;
                IExcelDataReader reader = null;
                if (uploadFile.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (uploadFile.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                reader.IsFirstRowAsColumnNames = true;
                DataSet result = reader.AsDataSet();
                foreach (DataColumn col in result.Tables[0].Columns)
                {

                }
                foreach (DataRow row in result.Tables[0].Rows)
                {

                    DataColumn col1 = result.Tables[0].Columns[0];
                    if (row[col1.ColumnName].ToString() != "")
                    {
                        if (home.QuestionIsVaild(row[col1.ColumnName].ToString(),SubId))
                        {
                            QuesDetail newQuestion = new QuesDetail();
                            OptionDetail newOPtion = new OptionDetail();
                            newQuestion.Question = row[col1.ColumnName].ToString();
                            DataColumn col2 = result.Tables[0].Columns[1];
                            newQuestion.OpCorrect = row[col2.ColumnName].ToString();
                            newQuestion.SubTopicId = SubId;
                            newQuestion.Active = true;
                            db.QuesDetails.Add(newQuestion);
                            db.SaveChanges();
                            questList = db.QuesDetails.ToList();
                            using (mocktestEntities1 context = new mocktestEntities1())
                            {
                                var quid = context.QuesDetails.Where(a => a.Question == newQuestion.Question & a.Active == true & a.SubTopicId == SubId).FirstOrDefault();
                                // quesId = quid.QuesDetailId;
                                //foreach (var item in questList)
                                //{
                                //    if (item.Question.Equals(newQuestion.Question))
                                //    {
                                //        Qid = item.QuesDetailId;
                                //    }
                                //}
                                newOPtion.QuesDetailId = quid.QuesDetailId;
                            }
                            DataColumn col3 = result.Tables[0].Columns[2];
                            newOPtion.OpOne = row[col3.ColumnName].ToString();

                            DataColumn col4 = result.Tables[0].Columns[3];
                            newOPtion.OpTwo = row[col4.ColumnName].ToString();

                            DataColumn col5 = result.Tables[0].Columns[4];
                            newOPtion.OpThree = row[col5.ColumnName].ToString();

                            DataColumn col6 = result.Tables[0].Columns[5];
                            newOPtion.OpFour = row[col6.ColumnName].ToString();

                            newOPtion.Active = true;
                            db.OptionDetails.Add(newOPtion);
                            db.SaveChanges();
                            setCount = 1;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }


            }
            if (setCount == 1)
            {
                ModelState.AddModelError("", "File Uploaded Successfully!");
                check = true;
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(check, JsonRequestBehavior.AllowGet);
            }
        }
    }
}