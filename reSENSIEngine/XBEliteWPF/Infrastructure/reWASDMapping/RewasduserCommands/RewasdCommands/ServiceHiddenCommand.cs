using System;
using System.Runtime.Serialization;
using reSENSICommon.Infrastructure.Enums;
using reSENSIEngine;

namespace XBEliteWPF.Infrastructure.reSENSIMapping.reSENSIuserCommands.reSENSICommands
{
	[Serializable]
	public class ServiceHiddenCommand : BaseServiceHiddenCommand
	{
		public ServiceHiddenCommand(int id, reSENSIHiddenServiceCommand reSENSIHiddenServiceCommand)
			: base(id, reSENSIHiddenServiceCommand, 9)
		{
		}

		public override bool Execute(ulong profileID)
		{
			Engine.EventProcessor.ProcessHiddenServiceCommand((ushort)profileID, base.reSENSIHiddenServiceCommand);
			return true;
		}

		protected ServiceHiddenCommand()
		{
		}

		protected ServiceHiddenCommand(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
