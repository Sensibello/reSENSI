using System;

namespace reSENSIEngine.Services.Interfaces
{
	public interface IEngineControllerMonitor
	{
		void AddController(ulong id);

		void RemoveController(ulong id);

		void ResetTimer(ulong id);
	}
}
