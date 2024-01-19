using System;

namespace reSENSIEngine.Services.UdpServer
{
	internal enum DataType
	{
		Gyroscope = 1,
		RotationVector,
		Accelerometer = 4,
		Mouse = 32,
		Keyboard = 64,
		Gamepad = 128
	}
}
