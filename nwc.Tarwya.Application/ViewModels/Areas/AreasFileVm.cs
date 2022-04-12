using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.ViewModels.Areas
{
    public class AreasFileVm
    {
        public string type { get; set; }
        public List<AreaFeature> features { get; set; }
    }
    public class AreaFeature
    {
        public string type { get; set; }
        public AreaFeatureGeometry geometry { get; set; }
        public AreaFeatureProperties properties { get; set; }

    }
    public class AreaFeatureGeometry
    {
        public List<List<List<List<double>>>> coordinates { get; set; }
        public string type { get; set; }

    }
    public class AreaFeatureProperties
    {
        public string name { get; set; }

    }
}
