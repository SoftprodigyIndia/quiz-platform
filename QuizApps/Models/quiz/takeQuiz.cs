using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApps.Models.quiz
{
    public class takeQuiz
    {
        [Required]
        public string Topic { get; set; }
        [Required]
        public string subTopic { get; set; }
        IList<question> questionlist { get; set; } 
        public string Options { get; set; }

    }
    public class question
    {
        IList<takeOption> optionList { get; set; }

    }
    public class takeOption
    {
        string option1 { get; set; }
        string option2 { get; set; }
        string option3 { get; set; }
        string option4 { get; set; }
    }
}
