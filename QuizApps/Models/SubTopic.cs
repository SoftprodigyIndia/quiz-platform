using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QuizApps.Models
{
    public class SubTopic
    {
        public int SubTopicId { get; set; }

        [Required]
        [Display(Name = "Sub Topic")]
        public string Name { get; set; }
        public int TopicId { get; set; }
        public bool Active { get; set; }
    }
}
