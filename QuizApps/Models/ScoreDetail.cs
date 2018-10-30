using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    public class ScoreDetail
    {
        public int ScoreDetailsId { get; set; }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "Attempted Questions.")]
        public int Attempted { get; set; }

        [Display(Name = "Corrected Answers.")]
        public int Corrected { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime? date { get; set; }

        [Display(Name = "Duration")]
        [DataType(DataType.Duration)]
        public int Duration { get; set; }

        [Display(Name = "Score")]
        public Decimal Score { get; set; }

        public bool Active { get; set; }
    }
}
