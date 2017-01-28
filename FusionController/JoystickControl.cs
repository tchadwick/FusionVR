using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace FusionController
{
        public class JoystickControl
        {
            private const int JoystickId = 1;
            private const double maxDirectionVectorValue = 3000;

            private vJoy joystick;
            private long maxValue = 0;
            private double inputMultiplyer = 0;

            private static JoystickControl instance;
            public static JoystickControl Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new JoystickControl();
                    }
                    return instance;
                }
            }

            private JoystickControl()
            {
                ConnectToVirtualJoystick();
            }

            private void ConnectToVirtualJoystick()
            {
                joystick = new vJoy();
                if (!joystick.vJoyEnabled())
                    throw new Exception("Unable to connect to virtual joystick");
                VjdStat joystickStatus = joystick.GetVJDStatus(JoystickId);
                if (joystickStatus != VjdStat.VJD_STAT_FREE)
                    throw new Exception("State of Joystick is not free");
                if (!joystick.AcquireVJD(JoystickId))
                    throw new Exception("Unable to acquire joystick");
                joystick.GetVJDAxisMax(JoystickId, HID_USAGES.HID_USAGE_X, ref maxValue);
                joystick.ResetVJD(JoystickId);
                inputMultiplyer = maxValue / (maxDirectionVectorValue * 2d); // Max direction value
            }

            public void SetNewPosition(float newPositionX, float newPositionY)
            {
                int xPosition = (int)((newPositionX + maxDirectionVectorValue) * inputMultiplyer);
                int yPosition = (int)((newPositionY + maxDirectionVectorValue) * inputMultiplyer);
                joystick.SetAxis(xPosition, JoystickId, HID_USAGES.HID_USAGE_X);
                joystick.SetAxis(yPosition, JoystickId, HID_USAGES.HID_USAGE_Y);
            }
        }
    }
}
