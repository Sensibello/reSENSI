using System;
using DiscSoftreSENSIServiceNamespace;
using reSENSICommon.Infrastructure.Enums;
using reSENSICommon.MacroCompilers;
using reSENSIEngine.Services.UdpServer.Input;
using reSENSIEngine.Services.UdpServer.Sensors;
using reSENSIEngine.Services.UdpServer.Utils;

namespace reSENSIEngine.Services.UdpServer
{
	internal class DataDecompressor
	{
		public static bool Decompress(byte[] data, out reSENSI_SET_CONTROLLER_STATE_REQUEST request, out bool isGamepadPresent, out bool isKeyboardPresent, out bool isMousePresent)
		{
			request = reSENSI_SET_CONTROLLER_STATE_REQUEST.CreateBlankInstance();
			isGamepadPresent = false;
			isKeyboardPresent = false;
			isMousePresent = false;
			int num = 0;
			int num2 = (int)data[num];
			num++;
			if (num2 != 1)
			{
				Console.WriteLine("DataDecompressor: Protocol version mismatch!");
				return false;
			}
			ulong @ulong = ByteUtils.GetULong(ByteUtils.GetPart(data, num, 8));
			num += 8;
			reSENSI_CONTROLLER_STATE_Gamepad reSENSI_CONTROLLER_STATE_Gamepad = default(reSENSI_CONTROLLER_STATE_Gamepad);
			reSENSI_CONTROLLER_STATE_Mouse reSENSI_CONTROLLER_STATE_Mouse = default(reSENSI_CONTROLLER_STATE_Mouse);
			reSENSI_CONTROLLER_STATE_Keyboard reSENSI_CONTROLLER_STATE_Keyboard = default(reSENSI_CONTROLLER_STATE_Keyboard);
			byte b = data[num];
			num++;
			try
			{
				if (DataDecompressor.IsBitSet(b, 1))
				{
					isGamepadPresent = true;
					int num3 = 12;
					Vector3SensorData vector3SensorData = Vector3SensorParser.Parse(ByteUtils.GetPart(data, num, num3));
					reSENSI_CONTROLLER_STATE_Gamepad.GyroXDelta = (long)((double)vector3SensorData.X * 683565275.5764316);
					reSENSI_CONTROLLER_STATE_Gamepad.GyroYDelta = (long)((double)vector3SensorData.Y * 683565275.5764316);
					reSENSI_CONTROLLER_STATE_Gamepad.GyroZDelta = (long)((double)vector3SensorData.Z * 683565275.5764316);
					num += num3;
				}
				if (DataDecompressor.IsBitSet(b, 2))
				{
					isGamepadPresent = true;
					int num4 = 16;
					Vector4SensorData vector4SensorData = Vector4SensorParser.Parse(ByteUtils.GetPart(data, num, num4));
					reSENSI_CONTROLLER_STATE_Gamepad.QuaternionW = vector4SensorData.W;
					reSENSI_CONTROLLER_STATE_Gamepad.QuaternionX = vector4SensorData.X;
					reSENSI_CONTROLLER_STATE_Gamepad.QuaternionY = vector4SensorData.Y;
					reSENSI_CONTROLLER_STATE_Gamepad.QuaternionZ = vector4SensorData.Z;
					num += num4;
				}
				if (DataDecompressor.IsBitSet(b, 4))
				{
					isGamepadPresent = true;
					int num5 = 12;
					Vector3SensorData vector3SensorData2 = Vector3SensorParser.Parse(ByteUtils.GetPart(data, num, num5));
					reSENSI_CONTROLLER_STATE_Gamepad.AccelXDelta = (long)((double)vector3SensorData2.X * 54745597.32426466);
					reSENSI_CONTROLLER_STATE_Gamepad.AccelYDelta = (long)((double)vector3SensorData2.Y * 54745597.32426466);
					reSENSI_CONTROLLER_STATE_Gamepad.AccelZDelta = (long)((double)vector3SensorData2.Z * 54745597.32426466);
					num += num5;
				}
				if (DataDecompressor.IsBitSet(b, 32))
				{
					isMousePresent = true;
					byte b2 = data[num];
					MouseData mouseData = MouseParser.Parse(ByteUtils.GetPart(data, num + 1, (int)b2));
					reSENSI_CONTROLLER_STATE_Mouse.Buttons = mouseData.Buttons;
					reSENSI_CONTROLLER_STATE_Mouse.MouseXDelta = mouseData.MouseXDelta;
					reSENSI_CONTROLLER_STATE_Mouse.MouseYDelta = mouseData.MouseYDelta;
					reSENSI_CONTROLLER_STATE_Mouse.WheelXDelta = mouseData.WheelXDelta;
					reSENSI_CONTROLLER_STATE_Mouse.WheelYDelta = mouseData.WheelYDelta;
					num += (int)(b2 + 1);
				}
				if (DataDecompressor.IsBitSet(b, 64))
				{
					isKeyboardPresent = true;
					byte b3 = data[num];
					KeyboardData keyboardData = KeyboardParser.Parse(ByteUtils.GetPart(data, num + 1, (int)b3));
					reSENSI_CONTROLLER_STATE_Keyboard.Keys = keyboardData.Keys;
					num += (int)(b3 + 1);
				}
				if (DataDecompressor.IsBitSet(b, 128))
				{
					isGamepadPresent = true;
					int num6 = data.Length - num;
					GamepadData gamepadData = GamepadParser.Parse(ByteUtils.GetPart(data, num, num6));
					reSENSI_CONTROLLER_STATE_Gamepad.LeftTrigger = gamepadData.LeftTrigger;
					reSENSI_CONTROLLER_STATE_Gamepad.RightTrigger = gamepadData.RightTrigger;
					reSENSI_CONTROLLER_STATE_Gamepad.LeftStickX = gamepadData.LeftStickX;
					reSENSI_CONTROLLER_STATE_Gamepad.LeftStickY = gamepadData.LeftStickY;
					reSENSI_CONTROLLER_STATE_Gamepad.RightStickX = gamepadData.RightStickX;
					reSENSI_CONTROLLER_STATE_Gamepad.RightStickY = gamepadData.RightStickY;
					reSENSI_CONTROLLER_STATE_Gamepad.LeftFinger = reSENSI_FINGER_DATA.CreateBlankInstance();
					reSENSI_CONTROLLER_STATE_Gamepad.LeftFinger.X = gamepadData.LeftFingerX;
					reSENSI_CONTROLLER_STATE_Gamepad.LeftFinger.Y = gamepadData.LeftFingerY;
					reSENSI_CONTROLLER_STATE_Gamepad.LeftFinger.Id = gamepadData.LeftFingerEventId;
					reSENSI_CONTROLLER_STATE_Gamepad.RightFinger = reSENSI_FINGER_DATA.CreateBlankInstance();
					reSENSI_CONTROLLER_STATE_Gamepad.RightFinger.X = gamepadData.RightFingerX;
					reSENSI_CONTROLLER_STATE_Gamepad.RightFinger.Y = gamepadData.RightFingerY;
					reSENSI_CONTROLLER_STATE_Gamepad.RightFinger.Id = gamepadData.RightFingerEventId;
					reSENSI_CONTROLLER_STATE_Gamepad.Buttons = new ulong[2];
					foreach (GamepadButton gamepadButton in gamepadData.PressedButtons)
					{
						int gamepadButtonIndex = MacroCompiler.GetGamepadButtonIndex(gamepadButton, 500, false, true, true);
						reSENSI_CONTROLLER_STATE_Gamepad.Buttons[0] |= 1UL << gamepadButtonIndex;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("DataDecompressor: Exception: " + ex.StackTrace);
				return false;
			}
			request.Capture = true;
			request.Id = @ulong;
			request.Type = 268435455U;
			request.State = reSENSI_CONTROLLER_STATE.CreateBlankInstance();
			request.State.Gamepad = reSENSI_CONTROLLER_STATE_Gamepad;
			request.State.Mouse = reSENSI_CONTROLLER_STATE_Mouse;
			request.State.Keyboard = reSENSI_CONTROLLER_STATE_Keyboard;
			return true;
		}

		private static bool IsBitSet(byte b, int bitVal)
		{
			return ((int)b & bitVal) != 0;
		}

		private const byte PROTOCOL_VERSION = 1;

		private const double GYROSCOPE_MULTIPLIER = 683565275.5764316;

		private const double ACCELEROMETER_MULTIPLIER = 54745597.32426466;
	}
}
