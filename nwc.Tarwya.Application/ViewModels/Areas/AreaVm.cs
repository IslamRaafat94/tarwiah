using nwc.Tarwya.Application.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.ViewModels.Areas
{
    public class AreaVm
    {
        public int Id { get; set; }
        public string DefaultAssetId { get; set; }
        public List<LocationPointVm> Coordinates { get; set; }
    }
}
