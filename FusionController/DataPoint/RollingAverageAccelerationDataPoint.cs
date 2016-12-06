using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController.DataPoint
{
    public class RollingAverageAccelerationDataPoint
    {
        private int RollingAverageSampleSize;
        private Queue<AccelerationDataPoint> PastDataPoints;
        private AccelerationDataPoint currentDataPoint;

        public RollingAverageAccelerationDataPoint(int rollingAverageSampleSize)
        {
            RollingAverageSampleSize = rollingAverageSampleSize;
            PastDataPoints = new Queue<AccelerationDataPoint>(rollingAverageSampleSize);
        }

        public AccelerationDataPoint DataPoint
        {
            get
            {
                return currentDataPoint;
            }
            set
            {
                currentDataPoint = value;
                if (PastDataPoints.Count >= RollingAverageSampleSize)
                {
                    PastDataPoints.Dequeue();
                }
                foreach (AccelerationDataPoint oldAccelPoint in PastDataPoints)
                {
                    currentDataPoint.AccelerationX += oldAccelPoint.AccelerationX;
                    currentDataPoint.AccelerationY += oldAccelPoint.AccelerationY;
                    currentDataPoint.AccelerationZ += oldAccelPoint.AccelerationZ;
                }

                PastDataPoints.Enqueue(currentDataPoint);

                currentDataPoint.AccelerationX = currentDataPoint.AccelerationX / PastDataPoints.Count;
                currentDataPoint.AccelerationY = currentDataPoint.AccelerationY / PastDataPoints.Count;
                currentDataPoint.AccelerationZ = currentDataPoint.AccelerationZ / PastDataPoints.Count;
            }
        }

        public bool Saturated
        {
            get
            {
                return PastDataPoints.Count >= RollingAverageSampleSize;
            }
        }
    }
}
