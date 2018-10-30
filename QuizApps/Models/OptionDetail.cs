using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    public class OptionDetail
    {
        public int OptionDetailsId { get; set; }

        public int QuesDetailId { get; set; }

        [Required]
        [Display(Name = "Opiton One")]
        public string OpOne { get; set; }

        [Display(Name = "Option Two")]
        public string OpTwo { get; set; }

        [Display(Name = "Option Three")]
        public string OpThree { get; set; }

        [Display(Name = "Option Four")]
        public string OpFour { get; set; }

        public bool Active { get; set; }
    }
}
