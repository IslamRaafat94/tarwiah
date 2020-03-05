using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedbackQuestionAnswers = new HashSet<FeedbackQuestionAnswer>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<FeedbackQuestionAnswer> FeedbackQuestionAnswers { get; set; }
    }
}