using System;
using DiscSoftreSENSIServiceNamespace;
using reSENSICommon.Infrastructure.Enums;
using XBEliteWPF.DataModels.GamepadActiveProfiles;
using XBEliteWPF.Infrastructure.Controller;

namespace XBEliteWPF.Infrastructure
{
	public delegate void ConfigAppliedToSlotHandler(BaseControllerVM controller, GamepadProfile gamepadProfile, Slot slot, reSENSI_CONTROLLER_PROFILE? profile);
}
