﻿using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class CategoryItem
    {
        public CategoryItem()
        {
            Complaints = new HashSet<Complaint>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string ServerName { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public string NameFa { get; set; }
        public string NameId { get; set; }
        public string NameUr { get; set; }
        public string NameTr { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}