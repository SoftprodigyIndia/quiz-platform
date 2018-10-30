using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApps.Models.quiz
{
    public class Upload
    {
        public Upload()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public List<HttpPostedFileBase> Files { get; set; }
        public string upSubId { get; set; }
    }
}