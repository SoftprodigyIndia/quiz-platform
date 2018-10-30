using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    public class Topic
    {
        public int TopicId { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
