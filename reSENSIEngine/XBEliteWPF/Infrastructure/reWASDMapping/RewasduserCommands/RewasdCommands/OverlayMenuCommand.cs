using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DiscSoft.NET.Common.Utils;
using reSENSICommon.Infrastructure.Enums;
using reSENSIEngine;
using XBEliteWPF.DataModels.GamepadActiveProfiles;
using XBEliteWPF.Infrastructure.Controller;
using XBEliteWPF.Utils.Extensions;
using XBEliteWPF.Utils.XBUtilModel;

namespace XBEliteWPF.Infrastructure.reSENSIMapping.reSENSIuserCommands.reSENSICommands
{
	[Serializable]
	public class OverlayMenuCommand : BaseExecutablereSENSIUserCommand
	{
		public reSENSIOverlayMenuServiceCommand reSENSIOverlayMenuServiceCommand { get; set; }

		public OverlayMenuCommand(int id, string displayName, int displayNameStrId, string drawingResourcename, reSENSIOverlayMenuServiceCommand reSENSIOverlayMenuServiceCommand, reSENSIUserCommandType reSENSIUserCommandType)
			: base(id, displayName, displayNameStrId, drawingResourcename, reSENSIUserCommandType)
		{
			this.reSENSIOverlayMenuServiceCommand = reSENSIOverlayMenuServiceCommand;
		}

		public override bool Execute(ulong profileID)
		{
			OverlayMenuCommand.<>c__DisplayClass5_0 CS$<>8__locals1 = new OverlayMenuCommand.<>c__DisplayClass5_0();
			CS$<>8__locals1.profileID = profileID;
			OverlayMenuCommand.<>c__DisplayClass5_0 CS$<>8__locals2 = CS$<>8__locals1;
			Wrapper<reSENSI_CONTROLLER_PROFILE_EX> wrapper = Engine.GamepadService.ServiceProfilesCollection.FirstOrDefault((Wrapper<reSENSI_CONTROLLER_PROFILE_EX> sp) => sp.Value.ServiceProfileIds.Contains((ushort)CS$<>8__locals1.profileID));
			CS$<>8__locals2.profile = ((wrapper != null) ? wrapper.Value.FindProfileById((ushort)CS$<>8__locals1.profileID) : null);
			if (CS$<>8__locals1.profile == null)
			{
				Tracer.TraceWrite("OverlayMenuCommand: profile not found - unexpected situation!", false);
				return false;
			}
			ulong[] array = CS$<>8__locals1.profile.Value.Id.ToArray<ulong>();
			CS$<>8__locals1.Id = XBUtils.CalculateID(array);
			GamepadProfiles gamepadProfiles = Engine.GamepadService.GamepadProfileRelations.FirstOrDefault((KeyValuePair<string, GamepadProfiles> kvp) => CS$<>8__locals1.Id == kvp.Key).Value;
			if (gamepadProfiles == null)
			{
				gamepadProfiles = Engine.GamepadService.GamepadProfileRelations.FirstOrDefault((KeyValuePair<string, GamepadProfiles> kvp) => CS$<>8__locals1.profile.Value.Id.Any((ulong id) => id != 0UL && kvp.Key.Contains(id.ToString()))).Value;
			}
			if (gamepadProfiles == null)
			{
				Tracer.TraceWrite("OverlayMenuCommand: gamepadProfiles not found - unexpected situation!", false);
				return false;
			}
			Slot slot = reSENSI_CONTROLLER_PROFILE_Extensions.GetSlot(CS$<>8__locals1.profile.Value);
			gamepadProfiles.SlotProfiles.TryGetValue(slot);
			BaseControllerVM associatedController = gamepadProfiles.GetAssociatedController();
			if (associatedController == null)
			{
				Tracer.TraceWrite("OverlayMenuCommand: controller not found!", false);
			}
			if (associatedController != null)
			{
				associatedController.FillFriendlyName();
				Engine.OverlayManagerService.ExecuteOverlayMenuCommand(this.reSENSIOverlayMenuServiceCommand);
			}
			return true;
		}

		protected OverlayMenuCommand()
		{
		}

		protected OverlayMenuCommand(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
