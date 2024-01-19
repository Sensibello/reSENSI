using System;
using reSENSICommon.Network.HTTP.DataTransferObjects;
using XBEliteWPF.Infrastructure.Controller;

namespace XBEliteWPF.Infrastructure
{
	public delegate void ShiftChangedHandler(BaseControllerVM controller, ShiftInfo shift, bool toggle);
}
