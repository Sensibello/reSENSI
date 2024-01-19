using System;
using System.Linq;
using DiscSoftreSENSIServiceNamespace;
using XBEliteWPF.Infrastructure.Controller;
using XBEliteWPF.Utils.XBUtilModel;

namespace reSENSIEngine.Utils
{
	public static class UtilsDebugMenu
	{
		public static ControllerVM CreatePromoGamepadVM(uint controllerType, string name)
		{
			reSENSI_CONTROLLER_INFO reSENSI_CONTROLLER_INFO = reSENSI_CONTROLLER_INFO.CreateBlankInstance();
			reSENSI_CONTROLLER_INFO.Type = controllerType;
			reSENSI_CONTROLLER_INFO.BatteryLevel = 3;
			reSENSI_CONTROLLER_INFO.BatteryKind = 0;
			reSENSI_CONTROLLER_INFO.ContainerId = Guid.NewGuid();
			reSENSI_CONTROLLER_INFO.Id = XBUtils.RNGRandomULong();
			ControllerVM controllerVM = new ControllerVM(reSENSI_CONTROLLER_INFO, true);
			controllerVM.IsPromoController = true;
			controllerVM.ControllerFriendlyName = name;
			controllerVM.SetIsInitialized(false);
			return controllerVM;
		}

		internal static void DeletePromoController(string id)
		{
			BaseControllerVM baseControllerVM = Engine.GamepadService.GamepadCollection.FirstOrDefault((BaseControllerVM x) => x.ID == id && x.IsPromoController);
			Engine.GamepadService.GamepadCollection.Remove(baseControllerVM);
		}
	}
}
