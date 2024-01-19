using System;

namespace reSENSIUI.Services.Interfaces
{
	public interface ISSEProcessor
	{
		void InitAndRun();

		void Restart();

		void StopAndClose();
	}
}
