using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Domain.Models.Models
{
    public partial class Toilet
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
    }
}