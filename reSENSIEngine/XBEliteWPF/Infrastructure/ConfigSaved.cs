using System;
using Prism.Events;
using reSENSICommon.Network.HTTP.DataTransferObjects.Events.Desktop;

namespace XBEliteWPF.Infrastructure
{
	public class ConfigSaved : PubSubEvent<ConfigSavedEvent>
	{
	}
}
