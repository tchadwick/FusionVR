using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionController.DataPoint;

namespace FusionController.Filters
{
    public class AccelerationFusionFilter
    {
        private TrackingAccelerationPoint trackingPoint1;
        private TrackingAccelerationPoint trackingPoint2;

        private float currentPoint = 0;
        public float CurrentPoint
        {
            get
            {
                return currentPoint;
            }
        }

        public AccelerationFusionFilter(float baseline1, float baseline2)
        {
            trackingPoint1 = new TrackingAccelerationPoint(baseline1, ;
            trackingPoint2 = _trackingPoint2;
        }

        public void UpdatePoints(float newPoint1, float newPoint2)
        {
            trackingPoint1.Value = newPoint1;
            trackingPoint2.Value = newPoint2;
            currentPoint = trackingPoint1.Value + trackingPoint2.Value;
        }
    }
}
