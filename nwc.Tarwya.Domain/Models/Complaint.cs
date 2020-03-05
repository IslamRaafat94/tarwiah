﻿using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class Complaint
    {
        public Complaint()
        {
            ComplaintImages = new HashSet<ComplaintImage>();
        }

        public int Id { get; set; }
        public string IssuerName { get; set; }
        public string IssuerMobileNumber { get; set; }
        public string Description { get; set; }
        public string AssetId { get; set; }
        public int SubCategoryId { get; set; }
        public string MantinanceArea { get; set; }
        public string AgetLanguage { get; set; }
        public string AgetOs { get; set; }
        public string AgentLocation { get; set; }
        public string Coordintes { get; set; }
        public string DefaultAssetId { get; set; }
        public bool IsSyncedToCcb { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual CategoryItem SubCategory { get; set; }
        public virtual ICollection<ComplaintImage> ComplaintImages { get; set; }
    }
}