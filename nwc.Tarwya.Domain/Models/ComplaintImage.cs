using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class ComplaintImage
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public string LocalName { get; set; }
        public string EamPath { get; set; }

        public virtual Complaint Complaint { get; set; }
    }
}