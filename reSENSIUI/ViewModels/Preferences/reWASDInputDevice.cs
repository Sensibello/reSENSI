using System;

namespace reSENSIUI.ViewModels.Preferences
{
	public class reSENSIInputDevice
	{
		public string DeviceID { get; set; }

		public string DeviceName { get; set; }

		public bool IsVirtual { get; set; }

		public override string ToString()
		{
			return this.DeviceName;
		}
	}
}
