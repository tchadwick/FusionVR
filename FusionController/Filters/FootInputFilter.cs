using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController.Filters
{
    public class FootInputFilter
    {
        private const int rollingAverageSampleSize = 5;

        #region " Properties "
        private Queue<AccelerationDataPoint> SensorDataPoints = new Queue<AccelerationDataPoint>(rollingAverageSampleSize);

        AccelerationDataPoint CurrentDataPoint;

        private SensorState CurrentSensorState = SensorState.Initializing;

        public AccelerationDataPoint Output { get; private set; }

        #endregion

        #region " Methods "

        public void UpdateInputs(AccelerationDataPoint accelerationDataPoint)
        {
            if(SensorDataPoints.Count >= rollingAverageSampleSize &&
               CurrentSensorState == SensorState.Initializing)
            {
                // TODO: Establish baseline for no movement
            }

            if(SensorDataPoints.Count >= rollingAverageSampleSize)
            {
                SensorDataPoints.Dequeue();
            }

            foreach (AccelerationDataPoint oldAccelPoint in SensorDataPoints)
            {
                accelerationDataPoint.AccelerationX += oldAccelPoint.AccelerationX;
                accelerationDataPoint.AccelerationY += oldAccelPoint.AccelerationY;
                accelerationDataPoint.AccelerationZ += oldAccelPoint.AccelerationZ;
            }

            SensorDataPoints.Enqueue(accelerationDataPoint);

            accelerationDataPoint.AccelerationX = accelerationDataPoint.AccelerationX / SensorDataPoints.Count;
            accelerationDataPoint.AccelerationY = accelerationDataPoint.AccelerationY / SensorDataPoints.Count;
            accelerationDataPoint.AccelerationZ = accelerationDataPoint.AccelerationZ / SensorDataPoints.Count;
        }

        #endregion
    }
}
