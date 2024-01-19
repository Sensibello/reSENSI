using System;
using reSENSICommon.Infrastructure.Enums;
using reSENSIUI.Services;

namespace reSENSIUI.Infrastructure
{
	public interface ISetSlotModel
	{
		Slot SelectedSlot { get; set; }

		GamepadService GamepadService { get; }

		LicensingService LicensingService { get; }
	}
}
