using System;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSIEngine.Services.OverlayAPI
{
	public class ButtonsInMessage
	{
		public ControllerTypeEnum? CurrentGamepadType { get; set; }

		public MessageButtonInfo Button { get; set; }
	}
}
