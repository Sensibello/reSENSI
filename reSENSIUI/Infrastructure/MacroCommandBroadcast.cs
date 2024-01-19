using System;
using DiscSoft.NET.Common.Utils.Clases;
using Prism.Events;

namespace reSENSIUI.Infrastructure
{
	public class MacroCommandBroadcast : PubSubEvent<WindowMessageEvent>
	{
	}
}
