using System;
using System.Collections.Generic;
using reSENSICommon.Infrastructure.Enums;
using XBEliteWPF.Infrastructure.KeyBindingsModel;

namespace reSENSIEngine.Services.OverlayAPI
{
	public class GroupFromSettings
	{
		public List<AssociatedControllerButton> groupButtons;

		public ControllerTypeEnum? CurrentGamepadType;
	}
}
