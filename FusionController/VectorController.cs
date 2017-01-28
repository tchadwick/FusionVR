using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionController.Filters;

namespace FusionController
{
    public class VectorController
    {
        public AccelerationFusionFilter FilterX { get; private set; }
        public AccelerationFusionFilter FilterY { get; private set; }
        public AccelerationFusionFilter FilterZ { get; private set; }

        public VectorController(AccelerationFusionFilter xFilter, AccelerationFusionFilter yFilter, AccelerationFusionFilter zFilter)
        {
            FilterX = xFilter;
            FilterY = yFilter;
            FilterZ = zFilter;
        }
    }
}
