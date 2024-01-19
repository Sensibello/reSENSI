using System;
using System.Collections.Generic;
using System.Linq;
using DiscSoft.NET.Common.Localization;
using Microsoft.AspNetCore.Mvc;
using reSENSICommon.Infrastructure.SupportedControllers;
using reSENSICommon.Network.HTTP.DataTransferObjects;
using reSENSICommon.Network.HTTP.DataTransferObjects.Activators;
using reSENSICommon.Services.HttpServer;
using reSENSICommon.Utils;
using XBEliteWPF.Infrastructure.reSENSIMapping;
using XBEliteWPF.Infrastructure.reSENSIMapping.KeyScanCodes;
using XBEliteWPF.Infrastructure.reSENSIMapping.reSENSIuserCommands;
using XBEliteWPF.Infrastructure.reSENSIMapping.reSENSIuserCommands.reSENSICommands;

namespace reSENSIEngine.Services.HttpServer
{
	[Route("v1.7/Info")]
	public class InfoController : ControllerBase
	{
		[HttpGet]
		[Route("Connection")]
		public IActionResult Get()
		{
			CrossPlatformLib.ConnDeviceInfo connDeviceInfo = default(CrossPlatformLib.ConnDeviceInfo);
			connDeviceInfo.devId = HttpServerSettings.GetDevId();
			connDeviceInfo.ip = HttpServerSettings.GetInterface();
			connDeviceInfo.port = HttpServerSettings.GetPort();
			connDeviceInfo.emulatorPort = HttpServerSettings.GetEmulatorPort();
			connDeviceInfo.devType = 1;
			connDeviceInfo.name = HttpServerSettings.GetDeviceName();
			return this.Ok(connDeviceInfo);
		}

		[HttpGet]
		[Route("SupportedControllers")]
		public IActionResult GetSupportedControllers()
		{
			return this.Ok(ControllersHelper.SupportedControllers);
		}

		[HttpGet]
		[Route("ActivatorsInfo")]
		public IActionResult GetActivatorsInfo()
		{
			List<ActivatorInfo> list = new List<ActivatorInfo>
			{
				new ActivatorInfo(0, DTLocalization.GetString(12334), DTLocalization.GetString(11553)),
				new ActivatorInfo(1, DTLocalization.GetString(12335), DTLocalization.GetString(11554)),
				new ActivatorInfo(2, DTLocalization.GetString(11879), DTLocalization.GetString(11555)),
				new ActivatorInfo(3, DTLocalization.GetString(11880), DTLocalization.GetString(11556)),
				new ActivatorInfo(4, DTLocalization.GetString(12336), DTLocalization.GetString(11557)),
				new ActivatorInfo(5, DTLocalization.GetString(12337), DTLocalization.GetString(11558))
			};
			return this.Ok(list);
		}

		[HttpGet]
		[Route("AvailableMappings")]
		public IActionResult GetAvailableKeyScanCodes()
		{
			List<KeyScanCodeV2> list = KeyScanCodeV2.SCAN_CODE_TABLE.Where((KeyScanCodeV2 ksc) => ksc == KeyScanCodeV2.NoMap || !string.IsNullOrWhiteSpace(ksc.Description) || !string.IsNullOrWhiteSpace(ksc.AltDescription)).ToList<KeyScanCodeV2>();
			IEnumerable<BasereSENSIUserCommand> enumerable = BasereSENSIUserCommand.reSENSI_USER_COMMAND_TABLE.Where((BasereSENSIUserCommand c) => !(c is ServiceHiddenCommand));
			List<BasereSENSIMapping> list2 = new List<BasereSENSIMapping>();
			list2.AddRange(list);
			list2.AddRange(enumerable);
			return this.Ok(list2);
		}

		[HttpGet]
		[Route("Constants")]
		public IActionResult GetConstants()
		{
			return this.Ok(new reSENSIConstants());
		}
	}
}
