﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionController
{
    public enum SensorState
    {
        Initializing,
        Initialized,
        Tracking,
    }

    public enum AccelerationState
    {
        WithinThreshold,
        TrackingEvent,
        WithinThresholdTracking,
        IgnoringEvent,
    }
}
