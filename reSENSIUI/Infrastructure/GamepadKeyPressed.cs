using System;
using System.Collections.Generic;
using Prism.Events;
using XBEliteWPF.Infrastructure;

namespace reSENSIUI.Infrastructure
{
	public class GamepadKeyPressed : PubSubEvent<List<GamepadButtonDescription>>
	{
	}
}
