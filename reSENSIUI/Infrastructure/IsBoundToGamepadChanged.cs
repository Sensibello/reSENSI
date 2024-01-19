using System;
using Prism.Events;
using reSENSIUI.Infrastructure.KeyBindings;

namespace reSENSIUI.Infrastructure
{
	public class IsBoundToGamepadChanged : PubSubEvent<ConfigData>
	{
	}
}
