using System;
using Prism.Events;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSIUI.Infrastructure
{
	public class CurrentGamepadSlotChanged : PubSubEvent<Slot>
	{
	}
}
