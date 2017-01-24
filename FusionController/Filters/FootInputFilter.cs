using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FusionController.DataPoint;

namespace FusionController.Filters
{
    public class FootInputFilter
    {
        private const int CurrentDataPointAverageCount = 5;
        private const int BaselineDataPointAverageCount = 15;
        private const double InitializationThreshold = 50;
        private const double DifferenceThreshold = 50;

        #region " Properties "

        RollingAverageAccelerationDataPoint CurrentDataPoint = new RollingAverageAccelerationDataPoint(CurrentDataPointAverageCount);

        RollingAverageAccelerationDataPoint BaselineDataPoint = new RollingAverageAccelerationDataPoint(BaselineDataPointAverageCount);

        private SensorState CurrentSensorState = SensorState.Initializing;

        public AccelerationDataPoint Output { get; private set; }

        #endregion

        #region " Methods "

        public void UpdateInputs(AccelerationDataPoint accelerationDataPoint)
        {
            CurrentDataPoint.DataPoint = accelerationDataPoint;
            UpdateBaseLine();
            UpdateState();
        }

        #endregion

        #region " Private "

        private void UpdateBaseLine()
        {
            if (CurrentSensorState == SensorState.Initializing)
            {
                if(BaselineDataPoint.DataPoint != null)
                {
                    AccelerationDataPoint previousBaseLine = BaselineDataPoint.DataPoint;

                    float accelXDiff = Math.Abs(previousBaseLine.AccelerationX - CurrentDataPoint.DataPoint.AccelerationX);
                    float accelYDiff = Math.Abs(previousBaseLine.AccelerationY - CurrentDataPoint.DataPoint.AccelerationY);
                    float accelZDiff = Math.Abs(previousBaseLine.AccelerationZ - CurrentDataPoint.DataPoint.AccelerationZ);

                    if (accelXDiff < InitializationThreshold &&
                        accelYDiff < InitializationThreshold &&
                        accelZDiff < InitializationThreshold)
                    {
                        CurrentSensorState = SensorState.Initialized;
                    }
                }

                BaselineDataPoint.DataPoint = CurrentDataPoint.DataPoint;
            }
        }

        private void UpdateState()
        {
            // TODO: Implement tracking forward-backward motion with reset trigger (may need overarching control to trigger reset)
            float accelXDiff = Math.Abs(BaselineDataPoint.DataPoint.AccelerationX - CurrentDataPoint.DataPoint.AccelerationX);
            float accelYDiff = Math.Abs(BaselineDataPoint.DataPoint.AccelerationY - CurrentDataPoint.DataPoint.AccelerationY);
            float accelZDiff = Math.Abs(BaselineDataPoint.DataPoint.AccelerationZ - CurrentDataPoint.DataPoint.AccelerationZ);
            if (accelXDiff > DifferenceThreshold ||
                accelYDiff > DifferenceThreshold ||
                accelZDiff > DifferenceThreshold)
            {
                CurrentSensorState = SensorState.Tracking;
            }
        }

        #endregion
    }
}