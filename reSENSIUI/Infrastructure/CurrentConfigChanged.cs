using System;
using Prism.Events;
using reSENSIUI.DataModels;

namespace reSENSIUI.Infrastructure
{
	public class CurrentConfigChanged : PubSubEvent<ConfigVM>
	{
	}
}
