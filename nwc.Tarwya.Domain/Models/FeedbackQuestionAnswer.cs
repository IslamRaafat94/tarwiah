using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class FeedbackQuestionAnswer
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int QuestionId { get; set; }
        public int Value { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual FeedbackQuestion Question { get; set; }
    }
}