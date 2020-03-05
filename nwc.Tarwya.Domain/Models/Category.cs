﻿using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }

        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public string NameFa { get; set; }
        public string NameId { get; set; }
        public string NameUr { get; set; }
        public string NameTr { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}