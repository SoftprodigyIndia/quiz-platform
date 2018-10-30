using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    public class QuesDetail
    {
        public int QuesDetailId { get; set; }

        public int SubTopicId { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [Display(Name = "Option Correct")]
        public int OpCorrect { get; set; }

        public bool Active { get; set; }
    }
}
