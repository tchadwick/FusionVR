using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController.DataPoint
{
    public class RollingAverageAccelPoint
    {
        private float RunningTotal = 0;
        private int RollingAverageSampleSize;
        private Queue<float> PastDataPoints;

        public RollingAverageAccelPoint(int sampleSize)
        {
            RollingAverageSampleSize = sampleSize;
            PastDataPoints = new Queue<float>(sampleSize);
        }
        

        public float Value
        {
            get
            {
                return RunningTotal / PastDataPoints.Count;
            }
            set
            {
                if (Saturated)
                {
                    float oldestValue = PastDataPoints.Dequeue();
                    RunningTotal -= oldestValue;
                }

                RunningTotal += value;

                PastDataPoints.Enqueue(value);
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
