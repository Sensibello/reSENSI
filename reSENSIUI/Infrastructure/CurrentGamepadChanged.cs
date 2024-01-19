using System;
using Prism.Events;
using reSENSIUI.Infrastructure.Controller;

namespace reSENSIUI.Infrastructure
{
	public class CurrentGamepadChanged : PubSubEvent<BaseControllerVM>
	{
	}
}
