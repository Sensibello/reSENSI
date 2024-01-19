using System;
using Prism.Events;
using reSENSIUI.Infrastructure.Controller;

namespace reSENSIUI.Infrastructure
{
	public class CurrentControllerDataChanged : PubSubEvent<BaseControllerVM>
	{
	}
}
