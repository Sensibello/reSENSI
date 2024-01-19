using System;

namespace reSENSIEngine.Services.UdpServer.Input
{
	internal struct MouseData
	{
		public byte Buttons;

		public long MouseXDelta;

		public long MouseYDelta;

		public long WheelXDelta;

		public long WheelYDelta;
	}
}
