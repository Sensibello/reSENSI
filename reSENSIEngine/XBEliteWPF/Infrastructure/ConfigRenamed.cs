using System;
using Prism.Events;
using reSENSICommon.Network.HTTP.DataTransferObjects;

namespace XBEliteWPF.Infrastructure
{
	public class ConfigRenamed : PubSubEvent<RenameConfigParams>
	{
	}
}
