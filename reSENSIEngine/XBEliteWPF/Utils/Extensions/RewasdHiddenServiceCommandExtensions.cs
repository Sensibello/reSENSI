using System;
using reSENSICommon.Infrastructure.Enums;

namespace XBEliteWPF.Utils.Extensions
{
	public static class reSENSIHiddenServiceCommandExtensions
	{
		public static bool IsLEDReactionRequired(this reSENSIHiddenServiceCommand command)
		{
			return (command >= 0 && command <= 21) || (command >= 30 && command <= 50) || (command >= 22 && command <= 25);
		}
	}
}
