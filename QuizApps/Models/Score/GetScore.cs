using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApps.Models.Score
{
    public class GetScore
    {
        public string user { get; set; }
        public string RollNo { get; set; }
        public string Branch { get; set; }
        public string topicname { get; set; }
        public string subname { get; set; }
        public Int32 score { get; set; }
        public Int32 correctAnswers { get; set; }
        public string totalTime { get; set; }
        public Int32 Attempted { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? today { get; set; }
    }
    public class scoreDetails
    {
        public IEnumerable<GetScore> scoreGrid { get; set; }
    }
}