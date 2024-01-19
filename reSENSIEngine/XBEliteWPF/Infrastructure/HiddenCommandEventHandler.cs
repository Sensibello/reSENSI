using System;
using reSENSICommon.Infrastructure.Enums;
using XBEliteWPF.DataModels.GamepadActiveProfiles;
using XBEliteWPF.Infrastructure.Controller;

namespace XBEliteWPF.Infrastructure
{
	public delegate void HiddenCommandEventHandler(reSENSIHiddenServiceCommand reSENSIHiddenServiceCommand, GamepadProfile gamepadProfile, string name, BaseControllerVM controller, bool toggle);
}
