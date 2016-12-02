using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController.Filters
{
    public class FootInputFilter
    {
        #region " Properties "
        private List<double> AccelerationXValues = new List<double>();
        private List<double> AccelerationYValues = new List<double>();
        private List<double> AccelerationZValues = new List<double>();

        private SensorState CurrentSensorState { get; set; }

        public double OutputX { get; private set; }

        public double OutputY { get; private set; }

        public double OutputZ { get; private set; }

        #endregion

        #region " Methods "

        public void UpdateInputs(double accelX, double accelY, double accelZ)
        {
            AccelerationXValues.Add(accelX);
            AccelerationYValues.Add(accelY);
            AccelerationZValues.Add(accelZ);
        }

        #endregion
    }
}
