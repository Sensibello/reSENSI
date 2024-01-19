using System;
using reSENSIUI.Utils;

namespace reSENSIUI.Infrastructure
{
	public interface ILedOperationsDeciderContainer
	{
		LedOperationsDecider LedOperationsDecider { get; set; }
	}
}
