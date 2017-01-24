using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController.DataPoint
{
    public class AccelPointTrackingPoint
    {
        private const float DifferenceThreshold = 50;
        private float BaseLine = 0;
        private float EventMaximum = 0;

        public AccelPointTrackingPoint(float baseLine, int rollingAverageCount)
        {
            BaseLine = baseLine;
            point = new RollingAverageAccelPoint(rollingAverageCount);
        }

        RollingAverageAccelPoint point;
        
        public float Value
        {
            get
            {
                return point.Value;
            }
            set
            {
                point.Value = value;
                UpdateState();
            }
        }

        private AccelerationState state = AccelerationState.WithinThreshold;
        public AccelerationState State
        {
            get
            {
                return state;
            }
        }

        private void UpdateState()
        {
            float eventValue = point.Value;
            float accelDiff = Math.Abs(BaseLine - eventValue);
            if (accelDiff > DifferenceThreshold)
            {
                state = state == AccelerationState.WithinThresholdTracking ? AccelerationState.IgnoringEvent : AccelerationState.TrackingEvent;
            }
            else
            {
                if (state == AccelerationState.TrackingEvent)
                    state = AccelerationState.WithinThresholdTracking;
                else
                    state = AccelerationState.WithinThreshold;
            }

            if(state == AccelerationState.TrackingEvent)
            {
                if(eventValue > 0 && eventValue > EventMaximum)
                {
                    EventMaximum = eventValue;
                }
                else if(eventValue < 0 && eventValue < EventMaximum)
                {
                    EventMaximum = eventValue;
                }
            }
        }
    }
}
