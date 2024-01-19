using System;
using reSENSICommon.Infrastructure.Enums;
using reSENSIUI.Infrastructure.Controller;

namespace reSENSIUI.Infrastructure
{
	public delegate void BatteryLevelChangedHandler(BaseControllerVM controller, BatteryLevel batteryLevel);
}
