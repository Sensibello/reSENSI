using System;
using Prism.Events;
using reSENSICommon.Network.HTTP.DataTransferObjects.Events.Desktop;

namespace reSENSIUI.Infrastructure
{
	public class RemapStateChanged : PubSubEvent<RemapStateChangedEvent>
	{
	}
}
