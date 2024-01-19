using System;
using Prism.Events;
using reSENSIUI.DataModels;

namespace reSENSIUI.Infrastructure
{
	public class ConfigCreatedByUI : PubSubEvent<ConfigVM>
	{
	}
}
