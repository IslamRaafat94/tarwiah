using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class FeedbackQuestion
    {
        public FeedbackQuestion()
        {
            FeedbackQuestionAnswers = new HashSet<FeedbackQuestionAnswer>();
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public string NameFa { get; set; }
        public string NameId { get; set; }
        public string NameUr { get; set; }
        public string NameTr { get; set; }

        public virtual ICollection<FeedbackQuestionAnswer> FeedbackQuestionAnswers { get; set; }
    }
}