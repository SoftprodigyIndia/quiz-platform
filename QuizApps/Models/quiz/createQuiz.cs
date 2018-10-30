using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApps.Models.quiz
{
    public class createQuiz
    {
        [Required]
        public string Topic { get; set; }
        [Required]
        public string subTopic { get; set; }
        

    }
    public class createQuestion
    {
        
        [Required]
        [AllowHtml]
        [System.Web.Mvc.Remote("CheckExistingQuestion", "RemoteValidation", ErrorMessage = "Question already exists!")]
        [Display(Name="Question")]
        public string Question { get; set; }

        [Required]
        [Display(Name = "EditQuestion")]
        public string EditQuestion { get; set; }

        [Required]
        [Display(Name = "Option Correct")]
        public string optionCorrect { get; set; }

        public createQuiz quiz { get; set; }

    }
    public class createOption
    {
        public createOption()
        {
       
            Files = new List<HttpPostedFileBase>();
            question = new createQuestion();
        }
        
        [Required]
        [Display(Name = "Opiton One")]
        public string optionOne { get; set; }

        [Required]
        [Display(Name = "Option Two")]
        public string optionTwo { get; set; }

        [Required]
        [Display(Name = "Option Three")]
        public string optionThree { get; set; }

        [Required]
        [Display(Name = "Option Four")]
        public string optionFour { get; set; }

        public IEnumerable<SelectListItem> TopicList { get; set; }
        public IEnumerable<SelectListItem> SubList { get; set; }
        public IEnumerable<SelectListItem> topicDropdown { get; set; }
        public IEnumerable<ShowQuiz> QuestionGrid { get; set; }

        public Int32 Qid { get; set; }
        public Int32 SubId { get; set; }
        public int subTestId { get; set; }
        public string subCatId { get; set; }
        public string TopId { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }

       
        public createQuestion question { get; set; }

    }
}