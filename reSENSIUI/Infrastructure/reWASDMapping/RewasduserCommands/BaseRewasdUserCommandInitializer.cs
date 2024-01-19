using System;
using System.Collections.Generic;
using System.Linq;
using reSENSICommon.Infrastructure;
using XBEliteWPF.Infrastructure.reSENSIMapping.reSENSIuserCommands;

namespace reSENSIUI.Infrastructure.reSENSIMapping.reSENSIuserCommands
{
	public class BasereSENSIUserCommandInitializer
	{
		public static void Init()
		{
			if (BasereSENSIUserCommand.reSENSI_USER_COMMAND_TABLE == null)
			{
				List<BasereSENSIUserCommand[]> list = new List<BasereSENSIUserCommand[]> { new BasereSENSIUserCommand[]
				{
					new BasereSENSIUserCommand(1, "COMMAND_TURN_REMAP_OFF", 11658, "UriBtnUnmapped", 0, 6, 1),
					new BasereSENSIUserCommand(2, "COMMAND_TURN_WIRELESS_JOY_OFF", 11659, "UriUserCommandTurnGamepadOff", 11826, 6, 2),
					new BasereSENSIUserCommand(3, "COMMAND_CLOSE_reSENSI_GUI", 11660, "Remove", 0, 6, 3),
					new BasereSENSIUserCommand(4, "COMMAND_SWITCH_GYRO_AXIS", 11661, "UriUserCommandGyroSwitch", 0, 6, 2),
					new BasereSENSIUserCommand(5, "COMMAND_RESET_TILT", 11662, "UriUserCommandTiltReset", 0, 6, 2),
					new BasereSENSIUserCommand(7, "COMMAND_LOG_OUT", 11664, "UriUserCommandLogout", 0, 6, 3),
					new BasereSENSIUserCommand(8, "COMMAND_SHUTDOWN", 11665, "UriKeyScanCodePower", 0, 6, 3),
					new BasereSENSIUserCommand(17, "COMMAND_RESTART_PC", 12546, "UriUserCommandRestartPC", 0, 6, 3),
					new BasereSENSIUserCommand(9, "COMMAND_SLEEP", 11666, "UriKeyScanCodeSleep", 0, 6, 4),
					new BasereSENSIUserCommand(13, "COMMAND_HIBERNATE", 11667, "UriUserCommandHibernate", 0, 6, 5),
					new BasereSENSIUserCommand(10, "COMMAND_TURN_ANTIBOSS_MODE", 11668, "UriUserCommandAntibossMode", 0, 6, 6),
					new BasereSENSIUserCommand(11, "COMMAND_TAKE_SCREENSHOT", 11669, "UriUserCommandScreenshot", 0, 6, 7),
					new BasereSENSIUserCommand(12, "COMMAND_CLOSE_ACTIVE_TASK", 11670, "UriUserCommandCloseTask", 0, 6, 8),
					new BasereSENSIUserCommand(14, "COMMAND_RECONNECT_TO_EXTERNAL_TARGET", 12069, "UriUserCommandReconnectToTargetBT", 0, 6, 2),
					new BasereSENSIUserCommand(6, "COMMAND_TOGGLE_GYRO_ON_OFF", 11663, "UriUserCommandToggleGyro", 0, 6, 2),
					new BasereSENSIUserCommand(15, "COMMAND_TURN_GYRO_ON", 12344, "UriUserCommandToggleGyroOn", 0, 6, 2),
					new BasereSENSIUserCommand(16, "COMMAND_TURN_GYRO_OFF", 12345, "UriUserCommandToggleGyroOff", 0, 6, 2),
					new BasereSENSIUserCommand(18, "COMMAND_RESET_VIRTUAL_GYRO", 12794, "VGyroReset", 0, 6, 2)
				} };
				if (Constants.CreateOverlayShift)
				{
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(130, "OVERLAY_MENU_PREV_SECTOR", 12531, "OverlayMappingPrevSector", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(131, "OVERLAY_MENU_NEXT_SECTOR", 12532, "OverlayMappingNextSector", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(132, "OVERLAY_MENU_APPLY", 11099, "OverlayMappingApply", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(133, "OVERLAY_MENU_CANCEL", 12718, "OverlayMappingCancel", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(134, "OVERLAY_MENU_UP", 11047, "IcoSwipeUp", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(135, "OVERLAY_MENU_DOWN", 11048, "IcoSwipeDown", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(137, "OVERLAY_MENU_LEFT", 11049, "IcoSwipeLeft", 0, 6, 101)
					});
					list.Add(new BasereSENSIUserCommand[]
					{
						new BasereSENSIUserCommand(136, "OVERLAY_MENU_RIGHT", 11050, "IcoSwipeRight", 0, 6, 101)
					});
				}
				BasereSENSIUserCommand.reSENSI_USER_COMMAND_TABLE = list.SelectMany((BasereSENSIUserCommand[] x) => x).ToArray<BasereSENSIUserCommand>();
			}
		}
	}
}
