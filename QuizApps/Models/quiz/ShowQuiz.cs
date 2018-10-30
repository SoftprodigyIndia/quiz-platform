using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApps.Models.quiz
{
    public class ShowQuiz
    {
        public int Qid { get; set; }
        public int OpId { get; set; }
        public string QuestionName { get; set; }
        public string OpOne { get; set; }
        public string OpTwo { get; set; }
        public string OpThree { get; set; }
        public string OpFour { get; set; }
        public string Correct { get; set; }
    }
}