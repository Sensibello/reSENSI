using System;
using System.Collections.Generic;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSIEngine.Services.OverlayAPI
{
	public class ButtonsInMessageAllDevices
	{
		public int Index { get; set; }

		public List<ButtonsInMessage> Buttons { get; set; }

		public ControllerTypeEnum? CurrentGamepadType;
	}
}
