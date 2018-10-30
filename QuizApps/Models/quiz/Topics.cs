using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApps.Models.quiz
{
    public class Topics
    {
       public Int32 TopId { get; set; }
       [Required]
       [System.Web.Mvc.Remote("CheckExistingTopic", "RemoteValidation", ErrorMessage = "Topic already exists!")]
       public string TopicName { get; set; }
       [Required]
       [System.Web.Mvc.Remote("CheckExistingSubTopic", "RemoteValidation", AdditionalFields = "TopId", ErrorMessage = "SubTopic already exists!")]
       public string SubTopicName { get; set; }
    }
}