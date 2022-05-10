using System;
using nwc.Tarwya.Infra.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.ViewModels.Seasons
{
    public class SeasonVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Required")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "Required")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
