using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DiscSoft.NET.Common.Localization;
using DiscSoft.NET.Common.Utils;
using DiscSoft.NET.Common.Utils.ExtensionMethods;
using DiscSoft.NET.Common.View.SecondaryWindows;
using Prism.Commands;
using Prism.Regions;
using reSENSICommon.Infrastructure;
using reSENSICommon.Infrastructure.Enums;
using reSENSIUI.Controls.XBBindingControls.BindingFrame.BindingFrameViews;
using reSENSIUI.DataModels;
using reSENSIUI.Infrastructure.KeyBindings;
using reSENSIUI.Infrastructure.KeyBindings.ActivatorXB;
using reSENSIUI.Infrastructure.KeyBindings.MacroBinding;
using reSENSIUI.Infrastructure.KeyBindings.XB;
using reSENSIUI.Infrastructure.KeyBindings.XBBindingDirectionalGroups;
using reSENSIUI.Views.ContentZoneGamepad.Macro;
using XBEliteWPF.Infrastructure.KeyBindingsModel;
using XBEliteWPF.Infrastructure.reSENSIMapping.KeyScanCodes;
using XBEliteWPF.Utils.Extensions;
using XBEliteWPF.Utils.XBUtilModel;

namespace reSENSIUI.Infrastructure
{
	public class reSENSIApplicationCommands
	{
		public static DelegateCommand<XBBinding> CopyBindingCommand
		{
			get
			{
				DelegateCommand<XBBinding> delegateCommand;
				if ((delegateCommand = reSENSIApplicationCommands._copyBinding) == null)
				{
					Action<XBBinding> action;
					if ((action = reSENSIApplicationCommands.<>O.<0>__CopyBinding) == null)
					{
						action = (reSENSIApplicationCommands.<>O.<0>__CopyBinding = new Action<XBBinding>(reSENSIApplicationCommands.CopyBinding));
					}
					Func<XBBinding, bool> func;
					if ((func = reSENSIApplicationCommands.<>O.<1>__CopyBindingCanExecute) == null)
					{
						func = (reSENSIApplicationCommands.<>O.<1>__CopyBindingCanExecute = new Func<XBBinding, bool>(reSENSIApplicationCommands.CopyBindingCanExecute));
					}
					delegateCommand = (reSENSIApplicationCommands._copyBinding = new DelegateCommand<XBBinding>(action, func));
				}
				return delegateCommand;
			}
		}

		private static void CopyBinding(XBBinding xbBinding)
		{
			reSENSIApplicationCommands.XBBindingToCopy = null;
			reSENSIApplicationCommands.DirectionData = null;
			if (xbBinding.IsMouseDirection)
			{
				reSENSIApplicationCommands.DirectionData = new reSENSIApplicationCommands.DirectionCopyPaste();
				reSENSIApplicationCommands.DirectionData.CopyDirectionGroup(xbBinding);
			}
			else
			{
				if (reSENSIApplicationCommands.CanCopyXBBinding(xbBinding))
				{
					reSENSIApplicationCommands.XBBindingToCopy = xbBinding;
				}
				else
				{
					ShiftXBBindingCollection shiftXBBindingCollection = xbBinding.HostCollection as ShiftXBBindingCollection;
					if (shiftXBBindingCollection != null)
					{
						reSENSIApplicationCommands.XBBindingToCopy = shiftXBBindingCollection.ParentBindingCollection.GetXBBindingByAssociatedControllerButton(xbBinding.ControllerButton);
					}
				}
				reSENSIApplicationCommands.XBBindingToCopy = reSENSIApplicationCommands.XBBindingToCopy.Clone();
			}
			reSENSIApplicationCommands.PasteBindingCommand.RaiseCanExecuteChanged();
		}

		private static bool CanCopyXBBinding(XBBinding xbBinding)
		{
			if (xbBinding == null)
			{
				return false;
			}
			BaseTouchpadDirectionalGroup baseTouchpadDirectionalGroup = xbBinding.HostCollection.GetDirectionalGroupByXBBinding(xbBinding) as BaseTouchpadDirectionalGroup;
			return (baseTouchpadDirectionalGroup == null || !baseTouchpadDirectionalGroup.TouchpadAnalogMode) && (xbBinding.IsAnyActivatorDescriptionPresent || xbBinding.IsAnyActivatorVirtualMappingPresent || xbBinding.IsAnyActivatorVirtualGamepadMappingPresent || xbBinding.IsContainsJumpToShift || (xbBinding.IsMouseDirection && (xbBinding.HostCollection.MouseDirectionalGroup.IsBoundToVirtualLeftStick || xbBinding.HostCollection.MouseDirectionalGroup.IsBoundToVirtualRightStick || xbBinding.HostCollection.MouseDirectionalGroup.IsUnmapped)) || xbBinding.IsRemapedOrUnmapped);
		}

		private static bool CopyBindingCanExecute(XBBinding xbBinding)
		{
			if (xbBinding == null)
			{
				return false;
			}
			if (reSENSIApplicationCommands.CanCopyXBBinding(xbBinding))
			{
				return true;
			}
			ShiftXBBindingCollection shiftXBBindingCollection = xbBinding.HostCollection as ShiftXBBindingCollection;
			return shiftXBBindingCollection != null && reSENSIApplicationCommands.CanCopyXBBinding(shiftXBBindingCollection.ParentBindingCollection.GetXBBindingByAssociatedControllerButton(xbBinding.ControllerButton));
		}

		public static DelegateCommand<XBBinding> PasteBindingCommand
		{
			get
			{
				DelegateCommand<XBBinding> delegateCommand;
				if ((delegateCommand = reSENSIApplicationCommands._pasteBinding) == null)
				{
					Action<XBBinding> action;
					if ((action = reSENSIApplicationCommands.<>O.<2>__PasteBinding) == null)
					{
						action = (reSENSIApplicationCommands.<>O.<2>__PasteBinding = new Action<XBBinding>(reSENSIApplicationCommands.PasteBinding));
					}
					Func<XBBinding, bool> func;
					if ((func = reSENSIApplicationCommands.<>O.<3>__PasteBindingCanExecute) == null)
					{
						func = (reSENSIApplicationCommands.<>O.<3>__PasteBindingCanExecute = new Func<XBBinding, bool>(reSENSIApplicationCommands.PasteBindingCanExecute));
					}
					delegateCommand = (reSENSIApplicationCommands._pasteBinding = new DelegateCommand<XBBinding>(action, func));
				}
				return delegateCommand;
			}
		}

		private static void PasteBinding(XBBinding xbBinding)
		{
			if (reSENSIApplicationCommands.DirectionData != null && reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsMouseGroup)
			{
				reSENSIApplicationCommands.DirectionData.PasteDirection(xbBinding);
			}
			else
			{
				reSENSIApplicationCommands.PasteXBBinding(xbBinding);
			}
			if (xbBinding != null)
			{
				BaseXBBindingCollection hostCollection = xbBinding.HostCollection;
				if (hostCollection == null)
				{
					return;
				}
				SubConfigData subConfigData = hostCollection.SubConfigData;
				if (subConfigData == null)
				{
					return;
				}
				ConfigData configData = subConfigData.ConfigData;
				if (configData == null)
				{
					return;
				}
				configData.CheckVirtualMappingsExist();
			}
		}

		private static void PasteXBBinding(XBBinding xbBinding)
		{
			bool flag = true;
			XBBinding xbbinding = reSENSIApplicationCommands.XBBindingToCopy.Clone();
			if (xbBinding.ControllerButton.IsKeyScanCode)
			{
				foreach (KeyValuePair<ActivatorType, ActivatorXBBinding> keyValuePair in ((IEnumerable<KeyValuePair<ActivatorType, ActivatorXBBinding>>)xbbinding.ActivatorXBBindingDictionary))
				{
					keyValuePair.Value.IsRumble = false;
				}
			}
			if (xbBinding.IsInheritedBinding)
			{
				ShiftXBBindingCollection shiftXBBindingCollection = xbBinding.HostCollection as ShiftXBBindingCollection;
				ShiftXBBindingCollection shiftXBBindingCollection2;
				if (shiftXBBindingCollection != null)
				{
					shiftXBBindingCollection2 = shiftXBBindingCollection;
				}
				else
				{
					shiftXBBindingCollection2 = ((MainXBBindingCollection)xbBinding.HostCollection).CurrentShiftXBBindingCollection;
				}
				xbBinding = shiftXBBindingCollection2.GetXBBindingByAssociatedControllerButton(xbBinding.ControllerButton);
			}
			if (xbBinding.HostCollection is MainXBBindingCollection && xbbinding.SingleActivator.MappedKey == KeyScanCodeV2.NoInheritance)
			{
				xbbinding.SingleActivator.MappedKey = KeyScanCodeV2.NoMap;
			}
			xbBinding.ActivatorXBBindingDictionary = new ActivatorXBBindingDictionary(xbbinding.ActivatorXBBindingDictionary, xbBinding.ControllerButton.Clone());
			xbBinding.ActivatorXBBindingDictionary.CheckJumpToShift();
			xbBinding.ActivatorXBBindingDictionary.CheckOverlayCommands();
			xbBinding.CurrentActivatorXBBinding.RefreshAnnotations();
			xbBinding.Refresh<XBBinding>();
			if (xbbinding.IsRemaped && xbBinding.ControllerButton.IsGamepadRemappingAvailiable)
			{
				xbBinding.RemapedTo = xbbinding.RemapedTo;
			}
			else if (xbbinding.IsUnmapped && xbBinding.ControllerButton.IsUnmapAvailiable)
			{
				xbBinding.RemapedTo = xbbinding.RemapedTo;
			}
			else
			{
				xbBinding.RevertRemap();
			}
			if (flag)
			{
				xbBinding.IsDisableInheritVirtualMapFromMain = !xbBinding.IsDisableInheritVirtualMapFromMain;
				xbBinding.IsDisableInheritVirtualMapFromMain = !xbBinding.IsDisableInheritVirtualMapFromMain;
			}
			xbbinding.Dispose();
		}

		private static bool PasteBindingCanExecute(XBBinding xbBinding)
		{
			if (xbBinding == null)
			{
				return false;
			}
			BaseTouchpadDirectionalGroup baseTouchpadDirectionalGroup = xbBinding.HostCollection.GetDirectionalGroupByXBBinding(xbBinding) as BaseTouchpadDirectionalGroup;
			if (baseTouchpadDirectionalGroup != null && baseTouchpadDirectionalGroup.TouchpadAnalogMode)
			{
				return false;
			}
			if (reSENSIApplicationCommands.XBBindingToCopy != null && reSENSIApplicationCommands.XBBindingToCopy.IsContainsJumpToShift && (xbBinding.IsDS3AnalogZone || xbBinding.IsStick || xbBinding.IsTriggerZone || xbBinding.IsGyroTilt || xbBinding.IsMouseScrolls))
			{
				return false;
			}
			if (!xbBinding.HostCollection.IsOverlayCollection && reSENSIApplicationCommands.XBBindingToCopy != null)
			{
				foreach (KeyValuePair<ActivatorType, ActivatorXBBinding> keyValuePair in ((IEnumerable<KeyValuePair<ActivatorType, ActivatorXBBinding>>)reSENSIApplicationCommands.XBBindingToCopy.ActivatorXBBindingDictionary))
				{
					if (keyValuePair.Value.MappedKey.IsAnyOverlayMenuCommand)
					{
						return false;
					}
				}
			}
			if (xbBinding.IsMouseDirection && reSENSIApplicationCommands.DirectionData != null && reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsMouseGroup)
			{
				return true;
			}
			if (reSENSIApplicationCommands.XBBindingToCopy == null || GamepadButtonExtensions.IsMouseStick(xbBinding.GamepadButton))
			{
				return false;
			}
			if (reSENSIApplicationCommands.XBBindingToCopy.ActivatorXBBindingDictionary.AnyValue((ActivatorXBBinding item) => item.MappedKey.IsMouseFlickDirection) && !GamepadButtonExtensions.IsAnyXStickDirection(xbBinding.GamepadButton))
			{
				return false;
			}
			if ((GamepadButtonExtensions.IsAnyStickZone(xbBinding.GamepadButton) || GamepadButtonExtensions.IsAnyTriggerZone(xbBinding.GamepadButton) || GamepadButtonExtensions.IsDS3AnalogZone(xbBinding.GamepadButton)) && (reSENSIApplicationCommands.XBBindingToCopy.IsAnyTogglePresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyNonSingleActivatorVirtualMappingPresent || reSENSIApplicationCommands.XBBindingToCopy.IsRemapedOrUnmapped))
			{
				return false;
			}
			if (GamepadButtonExtensions.IsAnyStickDirection(xbBinding.GamepadButton) || GamepadButtonExtensions.IsAnyGyroTiltDirection(xbBinding.GamepadButton))
			{
				if (reSENSIApplicationCommands.XBBindingToCopy.IsAnyNonSingleActivatorVirtualMappingPresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTogglePresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTurboPresent || (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator != null && reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.IsRumble))
				{
					return false;
				}
				if (GamepadButtonExtensions.IsAnyStickDirection(xbBinding.GamepadButton) && reSENSIApplicationCommands.XBBindingToCopy.IsRemaped)
				{
					return false;
				}
				if (GamepadButtonExtensions.IsAnyGyroTiltDirection(xbBinding.GamepadButton) && reSENSIApplicationCommands.XBBindingToCopy.IsRemapedOrUnmapped)
				{
					return false;
				}
			}
			if (GamepadButtonExtensions.IsGyroTiltZone(xbBinding.GamepadButton) && (reSENSIApplicationCommands.XBBindingToCopy.IsAnyNonSingleActivatorVirtualMappingPresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTogglePresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTurboPresent || reSENSIApplicationCommands.XBBindingToCopy.IsRemapedOrUnmapped))
			{
				return false;
			}
			if (GamepadButtonExtensions.IsMouseScroll(xbBinding.GamepadButton))
			{
				if (reSENSIApplicationCommands.XBBindingToCopy.IsAnyNonSingleActivatorVirtualMappingPresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTogglePresent || reSENSIApplicationCommands.XBBindingToCopy.IsAnyTurboPresent || reSENSIApplicationCommands.XBBindingToCopy.IsRemaped)
				{
					return false;
				}
				if (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator != null)
				{
					if (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.IsRumble)
					{
						return false;
					}
					MacroSequence macroSequence = reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.MacroSequence;
					if (macroSequence != null && macroSequence.Count > 0 && reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.MacroSequence.IsHoldUntilRelease)
					{
						return false;
					}
					if (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.IsOneKeyVirtualMappingPresent && (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.MappedKey.PCKeyCategory == 5 || reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.MappedKey.PCKeyCategory == 9 || reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.MappedKey.PCKeyCategory == 2))
					{
						return false;
					}
				}
			}
			BaseXBBindingCollection hostCollection = xbBinding.HostCollection;
			bool flag;
			if (hostCollection == null)
			{
				flag = false;
			}
			else
			{
				SubConfigData subConfigData = hostCollection.SubConfigData;
				bool? flag2 = ((subConfigData != null) ? new bool?(subConfigData.IsPeripheral) : null);
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			if (flag)
			{
				if (reSENSIApplicationCommands.XBBindingToCopy.SingleActivator != null && reSENSIApplicationCommands.XBBindingToCopy.SingleActivator.IsRumble)
				{
					return false;
				}
				if (reSENSIApplicationCommands.XBBindingToCopy.IsRemaped)
				{
					return false;
				}
			}
			return true;
		}

		public static DelegateCommand<XBBinding> CopyBindingGroupCommand
		{
			get
			{
				DelegateCommand<XBBinding> delegateCommand;
				if ((delegateCommand = reSENSIApplicationCommands._copyBindingGroup) == null)
				{
					Action<XBBinding> action;
					if ((action = reSENSIApplicationCommands.<>O.<4>__CopyBindingGroup) == null)
					{
						action = (reSENSIApplicationCommands.<>O.<4>__CopyBindingGroup = new Action<XBBinding>(reSENSIApplicationCommands.CopyBindingGroup));
					}
					Func<XBBinding, bool> func;
					if ((func = reSENSIApplicationCommands.<>O.<5>__CopyBindingGroupCanExecute) == null)
					{
						func = (reSENSIApplicationCommands.<>O.<5>__CopyBindingGroupCanExecute = new Func<XBBinding, bool>(reSENSIApplicationCommands.CopyBindingGroupCanExecute));
					}
					delegateCommand = (reSENSIApplicationCommands._copyBindingGroup = new DelegateCommand<XBBinding>(action, func));
				}
				return delegateCommand;
			}
		}

		private static void CopyBindingGroup(XBBinding xbBinding)
		{
			reSENSIApplicationCommands.DirectionData = new reSENSIApplicationCommands.DirectionCopyPaste();
			reSENSIApplicationCommands.DirectionData.CopyDirectionGroup(xbBinding);
			reSENSIApplicationCommands.PasteBindingGroupCommand.RaiseCanExecuteChanged();
		}

		private static bool CopyBindingGroupCanExecute(XBBinding xbBinding)
		{
			if (xbBinding == null)
			{
				return false;
			}
			if (xbBinding.IsLeftStickDirection)
			{
				LeftStickDirectionalGroup leftStickDirectionalGroup = xbBinding.HostCollection.LeftStickDirectionalGroup;
				return !leftStickDirectionalGroup.IsDefaultValues || !leftStickDirectionalGroup.IsAdvancedDefault();
			}
			if (xbBinding.IsRightStickDirection)
			{
				RightStickDirectionalGroup rightStickDirectionalGroup = xbBinding.HostCollection.RightStickDirectionalGroup;
				return !rightStickDirectionalGroup.IsDefaultValues || !rightStickDirectionalGroup.IsAdvancedDefault();
			}
			if (xbBinding.IsGyroTiltDirection)
			{
				GyroTiltDirectionalGroup gyroTiltDirectionalGroup = xbBinding.HostCollection.GyroTiltDirectionalGroup;
				return !gyroTiltDirectionalGroup.IsDefaultValues || !gyroTiltDirectionalGroup.IsAdvancedDefault();
			}
			return false;
		}

		public static DelegateCommand<XBBinding> PasteBindingGroupCommand
		{
			get
			{
				DelegateCommand<XBBinding> delegateCommand;
				if ((delegateCommand = reSENSIApplicationCommands._pasteBindingGroup) == null)
				{
					Action<XBBinding> action;
					if ((action = reSENSIApplicationCommands.<>O.<6>__PasteBindingGroup) == null)
					{
						action = (reSENSIApplicationCommands.<>O.<6>__PasteBindingGroup = new Action<XBBinding>(reSENSIApplicationCommands.PasteBindingGroup));
					}
					Func<XBBinding, bool> func;
					if ((func = reSENSIApplicationCommands.<>O.<7>__PasteBindingGroupCanExecute) == null)
					{
						func = (reSENSIApplicationCommands.<>O.<7>__PasteBindingGroupCanExecute = new Func<XBBinding, bool>(reSENSIApplicationCommands.PasteBindingGroupCanExecute));
					}
					delegateCommand = (reSENSIApplicationCommands._pasteBindingGroup = new DelegateCommand<XBBinding>(action, func));
				}
				return delegateCommand;
			}
		}

		private static void PasteBindingGroup(XBBinding xbBinding)
		{
			if (reSENSIApplicationCommands.DirectionData != null)
			{
				reSENSIApplicationCommands.DirectionData.PasteDirection(xbBinding);
			}
		}

		private static bool PasteBindingGroupCanExecute(XBBinding xbBinding)
		{
			return xbBinding != null && reSENSIApplicationCommands.DirectionData != null && (xbBinding.HostCollection.IsOverlayCollection || reSENSIApplicationCommands.DirectionData == null || reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup == null || !reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsAnyDirectionOverlayMappingPresent) && (((xbBinding.IsLeftStickDirection || xbBinding.IsRightStickDirection) && reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup != null && (reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsLeftStickGroup || reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsRightStickGroup)) || (xbBinding.IsGyroTiltDirection && reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup != null && reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup.IsGyroTiltGroup));
		}

		public static bool CanCopyMacroSequence(MacroSequence macroSequence)
		{
			if (macroSequence == null)
			{
				return false;
			}
			return macroSequence.Any((BaseMacro item) => item.IsSelected);
		}

		public static void CopyMacroSequence(MacroSequence macroSequence)
		{
			reSENSIApplicationCommands._copyMacroSequence = new MacroSequence(macroSequence.ControllerButton, 1, 0);
			foreach (BaseMacro baseMacro in macroSequence)
			{
				if (baseMacro.IsSelected && !(baseMacro is MacroCrutchHoldUntillRelease))
				{
					reSENSIApplicationCommands._copyMacroSequence.Add(baseMacro.Clone(reSENSIApplicationCommands._copyMacroSequence));
					if (macroSequence.MacroSequenceType == null)
					{
						if (baseMacro.IsKeyBinding)
						{
							MacroKeyBinding macroKeyBinding = (MacroKeyBinding)baseMacro;
							reSENSIApplicationCommands._copyMacroSequence.Add(new MacroKeyBinding(reSENSIApplicationCommands._copyMacroSequence, macroKeyBinding.KeyScanCode, 1));
						}
						if (baseMacro.IsGamepadBinding)
						{
							MacroGamepadBinding macroGamepadBinding = (MacroGamepadBinding)baseMacro;
							reSENSIApplicationCommands._copyMacroSequence.Add(new MacroGamepadBinding(reSENSIApplicationCommands._copyMacroSequence, macroGamepadBinding.GamepadButtonDescription, 1));
						}
					}
				}
			}
			macroSequence.RaiseCanExecuteChangedForCommands();
		}

		public static bool CanPasteMacroSequence()
		{
			return reSENSIApplicationCommands._copyMacroSequence != null;
		}

		public static void PasteMacroSequence(MacroSequence macroSequence, int index)
		{
			if (reSENSIApplicationCommands._copyMacroSequence == null)
			{
				return;
			}
			reSENSIApplicationCommands.PasteMacros(macroSequence, index);
		}

		public static void ReplaceMacroSequence(MacroSequence macroSequence, int index)
		{
			if (reSENSIApplicationCommands._copyMacroSequence == null)
			{
				return;
			}
			reSENSIApplicationCommands.RemoveOldMacros(macroSequence, ref index);
			reSENSIApplicationCommands.PasteMacros(macroSequence, index);
		}

		private static void PasteMacros(MacroSequence macroSequence, int index)
		{
			BaseMacro baseMacro = macroSequence.FirstOrDefault((BaseMacro macro) => macro is MacroGamepadStickCompensation);
			macroSequence.SupressOnCollectionChanged = true;
			foreach (BaseMacro baseMacro2 in reSENSIApplicationCommands._copyMacroSequence)
			{
				if (!(baseMacro2 is MacroGamepadStickCompensation) || baseMacro == null)
				{
					BaseMacro baseMacro3 = baseMacro2.Clone(macroSequence);
					baseMacro3.IsSelected = true;
					macroSequence.InsertOrAdd(baseMacro3, index, false);
					index++;
				}
			}
			macroSequence.SupressOnCollectionChanged = false;
			macroSequence.CleanupSequence(true);
			if (macroSequence.MacroSequenceType == null)
			{
				macroSequence.ConvertOnceToHold();
				macroSequence.FixMacroSequenceKeys(false);
			}
			macroSequence.MoveStickCompensationToEndOfSequence();
			macroSequence.RaiseCanExecuteChangedForCommands();
		}

		private static void RemoveOldMacros(MacroSequence macroSequence, ref int index)
		{
			int num = -1;
			for (int i = 0; i < index; i++)
			{
				if (!macroSequence[i].IsSelected)
				{
					num = i;
				}
			}
			BaseMacro baseMacro = null;
			if (num != -1)
			{
				baseMacro = macroSequence[num];
			}
			macroSequence.RemoveSelectedItems(false);
			index = 0;
			if (baseMacro != null)
			{
				index = macroSequence.IndexOf(baseMacro) + 1;
			}
		}

		public static void CopyShiftConfig(ConfigVM config, int shiftIndex)
		{
			reSENSIApplicationCommands._copyShiftConfig = new reSENSIApplicationCommands.CopyShiftConfigData();
			ConfigData configData = XBUtils.CreateConfigData(true);
			config.ConfigData.CopyToModel(configData);
			reSENSIApplicationCommands._copyShiftConfig.configData = configData;
			reSENSIApplicationCommands._copyShiftConfig.shiftIndex = shiftIndex;
		}

		public static bool CanCopyShiftConfig(ConfigVM config, int parameter, bool checkName = true)
		{
			if (((config != null) ? config.ConfigData : null) != null)
			{
				int parameter2 = parameter;
				int? num = ((config != null) ? new int?(config.ConfigData.LayersCount) : null);
				if (!((parameter2 >= num.GetValueOrDefault()) & (num != null)))
				{
					bool flag = config.ConfigData.Any((SubConfigData subConfig) => subConfig.MainXBBindingCollection.GetCollectionByLayer(parameter).IsCollectionHasMappingsForCopy || subConfig.MainXBBindingCollection.GetCollectionByLayer(parameter).HasJumpToShift || subConfig.MainXBBindingCollection.GetCollectionByLayer(parameter).VirtualDeviceSettings.IsAnyVirtualSettingsNonDefault());
					string text = null;
					using (IEnumerator<SubConfigData> enumerator = config.ConfigData.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							text = enumerator.Current.MainXBBindingCollection.GetCollectionByLayer(parameter).Description;
						}
					}
					if (checkName && text != null && !text.Equals(""))
					{
						if (parameter == 0)
						{
							flag |= text != DTLocalization.GetString(11195);
						}
						else
						{
							flag |= text != string.Format(DTLocalization.GetString(12474), parameter);
						}
					}
					return flag;
				}
			}
			return false;
		}

		public static void FillShift(ConfigData source, int shiftIndexSource, ConfigVM config, int shiftIndex, bool isFillPreset = false, bool isCopyToNotEmpty = true)
		{
			Dictionary<ControllerFamily, int> dictionary = new Dictionary<ControllerFamily, int>();
			foreach (SubConfigData subConfigData in source)
			{
				if (!dictionary.ContainsKey(subConfigData.ControllerFamily))
				{
					dictionary[subConfigData.ControllerFamily] = 0;
				}
				Dictionary<ControllerFamily, int> dictionary2 = dictionary;
				ControllerFamily controllerFamily = subConfigData.ControllerFamily;
				int num = dictionary2[controllerFamily];
				dictionary2[controllerFamily] = num + 1;
				int num2 = dictionary[subConfigData.ControllerFamily];
				BaseXBBindingCollection collectionByLayer = subConfigData.MainXBBindingCollection.GetCollectionByLayer(shiftIndexSource);
				SubConfigData subConfigData2 = config.ConfigData.FindControllerCollection(subConfigData.ControllerFamily, num2 - 1, true);
				BaseXBBindingCollection collectionByLayer2 = subConfigData2.MainXBBindingCollection.GetCollectionByLayer(shiftIndex);
				subConfigData2.MainXBBindingCollection.SetEnablePropertyChanged(false, false);
				if (isCopyToNotEmpty)
				{
					collectionByLayer2.FillDictionaryWithEmpyXBBindings(subConfigData.ControllerFamily);
				}
				collectionByLayer2.CopyFromModel(collectionByLayer, isCopyToNotEmpty);
				collectionByLayer2.MaskBindingCollection.CurrentEditItem = null;
				collectionByLayer2.MaskBindingCollection.CopyFromModel(collectionByLayer.MaskBindingCollection, isCopyToNotEmpty);
				if (isCopyToNotEmpty || (!isCopyToNotEmpty && !collectionByLayer2.VirtualDeviceSettings.VirtualLeftStick.IsVirtualStickSettingsNonDefault(false)))
				{
					collectionByLayer.VirtualDeviceSettings.VirtualLeftStick.CopyTo(collectionByLayer2.VirtualDeviceSettings.VirtualLeftStick);
				}
				if (isCopyToNotEmpty || (!isCopyToNotEmpty && !collectionByLayer2.VirtualDeviceSettings.VirtualRightStick.IsVirtualStickSettingsNonDefault(false)))
				{
					collectionByLayer.VirtualDeviceSettings.VirtualRightStick.CopyTo(collectionByLayer2.VirtualDeviceSettings.VirtualRightStick);
				}
				if (isCopyToNotEmpty || (!isCopyToNotEmpty && !collectionByLayer2.VirtualDeviceSettings.VirtualGyro.IsVirtualGyroSettingsNonDefault()))
				{
					collectionByLayer.VirtualDeviceSettings.VirtualGyro.CopyTo(collectionByLayer2.VirtualDeviceSettings.VirtualGyro);
				}
				collectionByLayer2.MaskBindingCollection.ShiftIndex = shiftIndex;
				collectionByLayer2.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
				{
					item.ActivatorXBBindingDictionary.CheckJumpToShift();
				});
				collectionByLayer2.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
				{
					item.ActivatorXBBindingDictionary.CheckOverlayCommands();
				});
				subConfigData2.MainXBBindingCollection.SetEnablePropertyChanged(true, false);
			}
			config.RefreshShiftModificators();
			App.EventAggregator.GetEvent<CurrentShiftBindingCollectionChanged>().Publish(App.GameProfilesService.CurrentGame.CurrentConfig.CurrentBindingCollection.CurrentShiftXBBindingCollection);
		}

		public static void FillPresetDirections(ConfigData source, int shiftIndexSource, ConfigVM config, int shiftIndex, bool isCopyToNotEmpty = true)
		{
			if (shiftIndex != 0)
			{
				return;
			}
			Dictionary<ControllerFamily, int> dictionary = new Dictionary<ControllerFamily, int>();
			foreach (SubConfigData subConfigData in source)
			{
				if (!dictionary.ContainsKey(subConfigData.ControllerFamily))
				{
					dictionary[subConfigData.ControllerFamily] = 0;
				}
				Dictionary<ControllerFamily, int> dictionary2 = dictionary;
				ControllerFamily controllerFamily = subConfigData.ControllerFamily;
				int num = dictionary2[controllerFamily];
				dictionary2[controllerFamily] = num + 1;
				int num2 = dictionary[subConfigData.ControllerFamily];
				BaseXBBindingCollection collectionByLayer = subConfigData.MainXBBindingCollection.GetCollectionByLayer(shiftIndexSource);
				SubConfigData subConfigData2 = config.ConfigData.FindControllerCollection(subConfigData.ControllerFamily, num2 - 1, true);
				BaseXBBindingCollection collectionByLayer2 = subConfigData2.MainXBBindingCollection.GetCollectionByLayer(shiftIndex);
				subConfigData2.MainXBBindingCollection.SetEnablePropertyChanged(false, false);
				if (collectionByLayer2.IsMainCollection && subConfigData.MainXBBindingCollection != null)
				{
					(collectionByLayer2 as MainXBBindingCollection).CopyFromModelForPresets(subConfigData.MainXBBindingCollection, isCopyToNotEmpty);
				}
				collectionByLayer2.CopyFromModelDirectionalGroups(collectionByLayer, isCopyToNotEmpty);
				collectionByLayer2.MaskBindingCollection.ShiftIndex = shiftIndex;
				collectionByLayer2.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
				{
					item.ActivatorXBBindingDictionary.CheckJumpToShift();
				});
				collectionByLayer2.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
				{
					item.ActivatorXBBindingDictionary.CheckOverlayCommands();
				});
				subConfigData2.MainXBBindingCollection.SetEnablePropertyChanged(true, false);
			}
			config.RefreshShiftModificators();
			App.EventAggregator.GetEvent<CurrentShiftBindingCollectionChanged>().Publish(App.GameProfilesService.CurrentGame.CurrentConfig.CurrentBindingCollection.CurrentShiftXBBindingCollection);
		}

		public static void PasteShiftConfig(ConfigVM config, int shiftIndex)
		{
			if (App.GameProfilesService.RealCurrentBeingMappedBindingCollection == null)
			{
				return;
			}
			bool isMaskModeView = App.GameProfilesService.RealCurrentBeingMappedBindingCollection.IsMaskModeView;
			reSENSIApplicationCommands.FillShift(reSENSIApplicationCommands._copyShiftConfig.configData, reSENSIApplicationCommands._copyShiftConfig.shiftIndex, config, shiftIndex, false, true);
			App.GameProfilesService.RealCurrentBeingMappedBindingCollection.IsMaskModeView = isMaskModeView;
		}

		public static bool CanPasteShiftConfig()
		{
			return reSENSIApplicationCommands._copyShiftConfig != null;
		}

		public static void RenameShiftConfig(ConfigVM config, int shiftIndex, string name)
		{
			foreach (SubConfigData subConfigData in config.ConfigData)
			{
				BaseXBBindingCollection collectionByLayer = subConfigData.MainXBBindingCollection.GetCollectionByLayer(shiftIndex);
				collectionByLayer.Description = ((collectionByLayer.DefaultDescription != name) ? name : null);
			}
			config.ConfigData.IsChanged = true;
		}

		public static void RemoveShiftConfig(ConfigVM config, int shiftIndex)
		{
			bool flag;
			if (config == null)
			{
				flag = false;
			}
			else
			{
				MainXBBindingCollection currentBindingCollection = config.CurrentBindingCollection;
				int? num;
				if (currentBindingCollection == null)
				{
					num = null;
				}
				else
				{
					ShiftXBBindingCollection currentShiftXBBindingCollection = currentBindingCollection.CurrentShiftXBBindingCollection;
					num = ((currentShiftXBBindingCollection != null) ? new int?(currentShiftXBBindingCollection.ShiftIndex) : null);
				}
				int? num2 = num;
				flag = (num2.GetValueOrDefault() == shiftIndex) & (num2 != null);
			}
			bool flag2 = flag;
			if (config != null)
			{
				config.ConfigData.MoveJumpToShifts(shiftIndex, -1);
			}
			config.ConfigData.AdditionalData.RemoveAt(shiftIndex);
			foreach (SubConfigData subConfigData in config.ConfigData)
			{
				subConfigData.MainXBBindingCollection.ShiftXBBindingCollections.RemoveAt(shiftIndex - 1);
				subConfigData.MainXBBindingCollection.CorrectShiftIndexes();
			}
			for (int i = 0; i < config.ConfigData.AdditionalData.Count; i++)
			{
				config.ConfigData.AdditionalData[i].LEDSettings.ShiftIndex = config.CurrentBindingCollection.GetCollectionByLayer(i).ShiftIndex;
				config.ConfigData.AdditionalData[i].LEDSettings.ResetShiftDrawing();
				config.ConfigData.AdditionalData[i].MaskBindingCollection.ShiftIndex = config.CurrentBindingCollection.GetCollectionByLayer(i).ShiftIndex;
			}
			config.ConfigData.RaiseAdditionalDataPropertyChanged();
			if (flag2)
			{
				App.GameProfilesService.ChangeCurrentShiftCollection(new int?(0), false);
			}
		}

		public static void ClearShiftConfig(ConfigVM config, int shiftIndex)
		{
			SubConfigData currentSubConfigData = config.CurrentSubConfigData;
			foreach (SubConfigData subConfigData in config.ConfigData)
			{
				BaseXBBindingCollection collectionByLayer = subConfigData.MainXBBindingCollection.GetCollectionByLayer(shiftIndex);
				collectionByLayer.Description = null;
				if (collectionByLayer is MainXBBindingCollection)
				{
					subConfigData.MainXBBindingCollection.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
					{
						item.ActivatorXBBindingDictionary.ClearShift();
					});
				}
				collectionByLayer.MaskBindingCollection.ClearAll();
				subConfigData.MainXBBindingCollection.SetEnablePropertyChanged(false, false);
				collectionByLayer.FillDictionaryWithEmpyXBBindings(subConfigData.ControllerFamily);
				collectionByLayer.ControllerBindings.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualLeftStick.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualRightStick.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualGyro.Clear();
				if (subConfigData.IsMouse)
				{
					collectionByLayer.MouseDirectionalGroup.ResetToDefault(false);
				}
				if (subConfigData.IsGamepad)
				{
					collectionByLayer.DPADDirectionalGroup.ResetToDefault(false);
					collectionByLayer.GyroTiltDirectionalGroup.ResetToDefault(false);
					collectionByLayer.LeftStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.RightStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.AdditionalStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.Touchpad1DirectionalGroup.ResetToDefault(false);
					collectionByLayer.Touchpad2DirectionalGroup.ResetToDefault(false);
					collectionByLayer.AdaptiveLeftTriggerSettings = null;
					collectionByLayer.AdaptiveRightTriggerSettings = null;
					if (collectionByLayer is MainXBBindingCollection)
					{
						subConfigData.MainXBBindingCollection.IsHardwareDeadzoneRightTrigger = false;
						subConfigData.MainXBBindingCollection.IsHardwareDeadzoneLeftTrigger = false;
						subConfigData.MainXBBindingCollection.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
						{
							item.ActivatorXBBindingDictionary.ClearShift();
						});
					}
					if (shiftIndex == 0)
					{
						subConfigData.MainXBBindingCollection.LeftTrigger.Clear();
						subConfigData.MainXBBindingCollection.RightTrigger.Clear();
						subConfigData.MainXBBindingCollection.LeftBumper.Clear();
						subConfigData.MainXBBindingCollection.RightBumper.Clear();
						subConfigData.MainXBBindingCollection.DS3CircleAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3CrossAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3SquareAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3TriangleAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3DPADUpAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3DPADDownAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3DPADLeftAnalog.Clear();
						subConfigData.MainXBBindingCollection.DS3DPADRightAnalog.Clear();
						subConfigData.MainXBBindingCollection.SteamDeckTrackpad1Pressure.Clear();
						subConfigData.MainXBBindingCollection.SteamDeckTrackpad2Pressure.Clear();
					}
				}
				subConfigData.MainXBBindingCollection.SetEnablePropertyChanged(true, false);
			}
			config.CurrentSubConfigData = currentSubConfigData;
			config.RefreshShiftModificators();
			config.ConfigData.CheckVirtualMappingsExist();
			config.ConfigData.CheckFeatures();
			App.EventAggregator.GetEvent<CurrentShiftBindingCollectionChanged>().Publish(App.GameProfilesService.CurrentGame.CurrentConfig.CurrentBindingCollection.CurrentShiftXBBindingCollection);
		}

		internal static void ClearConfig(ConfigVM config)
		{
			SubConfigData currentSubConfigData = config.CurrentSubConfigData;
			List<SubConfigData> list = new List<SubConfigData>();
			using (IEnumerator<SubConfigData> enumerator = config.ConfigData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SubConfigData subConfig2 = enumerator.Current;
					if (!subConfig2.IsMainSubConfig)
					{
						list.Add(subConfig2);
					}
					else
					{
						subConfig2.MainXBBindingCollection.SetEnablePropertyChanged(false, false);
						subConfig2.MainXBBindingCollection.GamepadVibrationMainLeft.ResetToDefaults();
						subConfig2.MainXBBindingCollection.GamepadVibrationMainRight.ResetToDefaults();
						subConfig2.MainXBBindingCollection.GamepadVibrationTriggerLeft.ResetToDefaults();
						subConfig2.MainXBBindingCollection.GamepadVibrationTriggerRight.ResetToDefaults();
						reSENSIApplicationCommands.ClearSubConfigCommand(config, subConfig2, 0, true);
						subConfig2.MainXBBindingCollection.ShiftXBBindingCollections.ForEach(delegate(ShiftXBBindingCollection shift)
						{
							reSENSIApplicationCommands.ClearSubConfigCommand(config, subConfig2, shift.ShiftIndex, true);
							shift.VirtualDeviceSettings.VirtualLeftStick.Clear();
							shift.VirtualDeviceSettings.VirtualRightStick.Clear();
							shift.VirtualDeviceSettings.VirtualGyro.Clear();
						});
						subConfig2.MainXBBindingCollection.SetEnablePropertyChanged(true, false);
					}
				}
			}
			using (List<SubConfigData>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					SubConfigData subConfig = enumerator2.Current;
					SubConfigData subConfigData = null;
					if (config.ConfigData.ConfigVM.CurrentSubConfigData == subConfig && subConfig.Index > 0)
					{
						subConfigData = config.ConfigData.FirstOrDefault((SubConfigData w) => w.Index == subConfig.Index - 1 && w.ControllerFamily == subConfig.ControllerFamily);
					}
					if (subConfigData != null)
					{
						config.ConfigData.ConfigVM.CurrentSubConfigData = subConfigData;
					}
					App.GameProfilesService.CurrentGame.CurrentConfig.ConfigData.Remove(subConfig);
					App.GameProfilesService.CurrentGame.CurrentConfig.ConfigData.Where((SubConfigData s) => s.ControllerFamily == subConfig.ControllerFamily && s.Index > subConfig.Index).ForEach(delegate(SubConfigData s)
					{
						s.Index--;
					});
				}
			}
			if (Constants.CreateOverlayShift && config.ConfigData.OverlayMenu != null)
			{
				config.ConfigData.OverlayMenu.Clear();
			}
			config.IsUseMouseKeyboardSettingsForAllSubConfigs = true;
			config.ConfigData[0].MainXBBindingCollection.MouseSensitivity = 4;
			config.ConfigData[0].MainXBBindingCollection.MouseDeflection = 8;
			config.ConfigData[0].MainXBBindingCollection.WheelDeflection = 13;
			config.ConfigData[0].MainXBBindingCollection.MouseAcceleration = 8;
			config.ConfigData[0].MainXBBindingCollection.VirtualKeyboardRepeatRate = 0;
			if (config.ConfigData[0].MainXBBindingCollection.Touchpad2DirectionalGroup != null)
			{
				config.ConfigData[0].MainXBBindingCollection.Touchpad2DirectionalGroup.IsTrackballFriction = true;
			}
			config.ConfigData.IsVirtualUsbHubPresent = false;
			config.ConfigData.IsUdpPresent = false;
			config.ConfigData.IsExternal = false;
			config.ConfigData.ResetLedSettings();
			VirtualGamepadType virtualGamepadType;
			Enum.TryParse<VirtualGamepadType>(RegistryHelper.GetValue("Config", "VirtualGamepadType", 0, false).ToString(), out virtualGamepadType);
			config.ConfigData.IsBoundToGamepad = false;
			config.ConfigData.VirtualGamepadType = virtualGamepadType;
			App.EventAggregator.GetEvent<CurrentShiftBindingCollectionChanged>().Publish(App.GameProfilesService.CurrentGame.CurrentConfig.CurrentBindingCollection.CurrentShiftXBBindingCollection);
			config.ConfigData.CheckVirtualMappingsExist();
			config.ConfigData.CheckFeatures();
			config.UpdateCreateSubConfigsCommandsCanExecute();
		}

		private static void ClearSubConfigCommand(ConfigVM config, SubConfigData subConfig, int shiftIndex, bool clearLayer = false)
		{
			SubConfigData currentSubConfigData = config.CurrentSubConfigData;
			BaseXBBindingCollection collectionByLayer = subConfig.MainXBBindingCollection.GetCollectionByLayer(shiftIndex);
			if (collectionByLayer != null)
			{
				collectionByLayer.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
				{
					item.ActivatorXBBindingDictionary.ClearShift();
				});
				collectionByLayer.MaskBindingCollection.ClearAll();
				collectionByLayer.FillDictionaryWithEmpyXBBindings(subConfig.ControllerFamily);
				collectionByLayer.ControllerBindings.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualLeftStick.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualRightStick.Clear();
				collectionByLayer.VirtualDeviceSettings.VirtualGyro.Clear();
				if (subConfig.IsMouse)
				{
					collectionByLayer.MouseDirectionalGroup.ResetToDefault(false);
				}
				if (subConfig.IsGamepad)
				{
					collectionByLayer.DPADDirectionalGroup.ResetToDefault(false);
					collectionByLayer.GyroTiltDirectionalGroup.ResetToDefault(false);
					collectionByLayer.LeftStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.RightStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.AdditionalStickDirectionalGroup.ResetToDefault(false);
					collectionByLayer.Touchpad1DirectionalGroup.ResetToDefault(false);
					collectionByLayer.Touchpad2DirectionalGroup.ResetToDefault(false);
					collectionByLayer.AdaptiveLeftTriggerSettings = null;
					collectionByLayer.AdaptiveRightTriggerSettings = null;
					if (collectionByLayer is MainXBBindingCollection)
					{
						subConfig.MainXBBindingCollection.IsHardwareDeadzoneRightTrigger = false;
						subConfig.MainXBBindingCollection.IsHardwareDeadzoneLeftTrigger = false;
						subConfig.MainXBBindingCollection.EnumAllBindings(true, true, true).ForEach(delegate(XBBinding item)
						{
							item.ActivatorXBBindingDictionary.ClearShift();
						});
					}
					subConfig.MainXBBindingCollection.LeftTrigger.Clear();
					subConfig.MainXBBindingCollection.RightTrigger.Clear();
					subConfig.MainXBBindingCollection.LeftBumper.Clear();
					subConfig.MainXBBindingCollection.RightBumper.Clear();
					subConfig.MainXBBindingCollection.DS3CircleAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3CrossAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3SquareAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3TriangleAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3DPADUpAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3DPADDownAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3DPADLeftAnalog.Clear();
					subConfig.MainXBBindingCollection.DS3DPADRightAnalog.Clear();
					subConfig.MainXBBindingCollection.SteamDeckTrackpad1Pressure.Clear();
					subConfig.MainXBBindingCollection.SteamDeckTrackpad2Pressure.Clear();
				}
				if (shiftIndex == 0 && (subConfig.MainXBBindingCollection.MouseSensitivity != 4 || subConfig.MainXBBindingCollection.MouseDeflection != 8 || subConfig.MainXBBindingCollection.WheelDeflection != 13 || subConfig.MainXBBindingCollection.MouseAcceleration != 8 || subConfig.MainXBBindingCollection.VirtualKeyboardRepeatRate != null))
				{
					config.IsUseMouseKeyboardSettingsForAllSubConfigs = false;
					subConfig.MainXBBindingCollection.MouseSensitivity = 4;
					subConfig.MainXBBindingCollection.MouseDeflection = 8;
					subConfig.MainXBBindingCollection.WheelDeflection = 13;
					subConfig.MainXBBindingCollection.MouseAcceleration = 8;
					subConfig.MainXBBindingCollection.VirtualKeyboardRepeatRate = 0;
				}
			}
			config.CurrentSubConfigData = currentSubConfigData;
			config.RefreshShiftModificators();
			config.ConfigData.CheckFeatures();
		}

		internal static void ClearSubConfig(ConfigVM config, SubConfigData subConfig, int shiftIndex, bool clearLayer = false)
		{
			subConfig.MainXBBindingCollection.SetEnablePropertyChanged(false, false);
			if (clearLayer)
			{
				reSENSIApplicationCommands.ClearSubConfigCommand(config, subConfig, shiftIndex, clearLayer);
			}
			else
			{
				subConfig.MainXBBindingCollection.GamepadVibrationMainLeft.ResetToDefaults();
				subConfig.MainXBBindingCollection.GamepadVibrationMainRight.ResetToDefaults();
				subConfig.MainXBBindingCollection.GamepadVibrationTriggerLeft.ResetToDefaults();
				subConfig.MainXBBindingCollection.GamepadVibrationTriggerRight.ResetToDefaults();
				reSENSIApplicationCommands.ClearSubConfigCommand(config, subConfig, 0, clearLayer);
				subConfig.MainXBBindingCollection.ShiftXBBindingCollections.ForEach(delegate(ShiftXBBindingCollection shift)
				{
					reSENSIApplicationCommands.ClearSubConfigCommand(config, subConfig, shift.ShiftIndex, clearLayer);
				});
			}
			subConfig.MainXBBindingCollection.SetEnablePropertyChanged(true, false);
			config.ConfigData.CheckVirtualMappingsExist();
			config.ConfigData.CheckFeatures();
			App.EventAggregator.GetEvent<CurrentShiftBindingCollectionChanged>().Publish(App.GameProfilesService.CurrentGame.CurrentConfig.CurrentBindingCollection.CurrentShiftXBBindingCollection);
		}

		public static void ShowMacroSettings(XBBinding xbBinding)
		{
			if (xbBinding == null)
			{
				return;
			}
			bool flag = true;
			if (xbBinding.CurrentActivatorXBBinding.IsOneKeyVirtualMappingPresent)
			{
				string text;
				if (xbBinding.IsOverlaySector)
				{
					text = DTLocalization.GetString(12721);
				}
				else if (xbBinding.HostMaskItem != null)
				{
					text = xbBinding.HostMaskItem.MaskConditions.ToString();
				}
				else if (xbBinding.ControllerButton.IsKeyScanCode)
				{
					text = xbBinding.KeyScanCode.FriendlyName;
				}
				else if (xbBinding.GamepadButtonDescription.Button != 2001)
				{
					text = xbBinding.GamepadButtonDescription.FriendlyName;
				}
				else
				{
					text = xbBinding.KeyScanCode.FriendlyName;
				}
				if (MessageBoxWithDoNotShowLogic.Show(Application.Current.MainWindow, string.Format(DTLocalization.GetString(11585), text), MessageBoxButton.YesNo, MessageBoxImage.Question, "DuplicateMappings", MessageBoxResult.Yes, false, 0.0, null, null, null, null, null, null) == MessageBoxResult.Yes)
				{
					xbBinding.CurrentActivatorXBBinding.MappedKey = KeyScanCodeV2.NoMap;
					xbBinding.CurrentActivatorXBBinding.MacroSequence.MacroSequenceType = 1;
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				if (xbBinding.ControllerButton.IsKeyScanCode || xbBinding.ControllerButton.IsMouseButton)
				{
					xbBinding.TryShowKeyboardMappingUnmapWarning(xbBinding.CurrentActivatorXBBinding);
				}
				Dictionary<object, object> dictionary = new Dictionary<object, object>();
				dictionary.Add("navigatePath", typeof(MacroSettings));
				NavigationParameters navigationParameters = new NavigationParameters();
				navigationParameters.Add("XBBinding", xbBinding);
				dictionary.Add("NavigationParameters", navigationParameters);
				if (xbBinding.HostCollection != App.GameProfilesService.RealCurrentBeingMappedBindingCollection)
				{
					navigationParameters.Add("BindingFrameViewTypeToReturnBack", typeof(BFShift));
				}
				reSENSIApplicationCommands.NavigateGamepadCommand.Execute(dictionary);
			}
		}

		public static CompositeCommand NavigateContentCommand = new CompositeCommand();

		public static CompositeCommand NavigateBindingFrameCommand = new CompositeCommand();

		public static CompositeCommand NavigateGamepadCommand = new CompositeCommand();

		public static CompositeCommand NavigateSideBarRegionCommand = new CompositeCommand();

		public static XBBinding XBBindingToCopy;

		private static reSENSIApplicationCommands.DirectionCopyPaste DirectionData;

		private static DelegateCommand<XBBinding> _copyBinding;

		private static DelegateCommand<XBBinding> _pasteBinding;

		private static DelegateCommand<XBBinding> _copyBindingGroup;

		private static DelegateCommand<XBBinding> _pasteBindingGroup;

		private static MacroSequence _copyMacroSequence = null;

		private static reSENSIApplicationCommands.CopyShiftConfigData _copyShiftConfig;

		public class DirectionCopyPaste
		{
			public void CopyDirectionGroup(XBBinding xbBinding)
			{
				if (xbBinding.IsLeftStickDirection)
				{
					this.DirectionalAnalogGroup = new LeftStickDirectionalGroup(xbBinding.HostCollection);
					this.description = xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description;
					xbBinding.HostCollection.LeftStickDirectionalGroup.CopyTo(reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup as LeftStickDirectionalGroup);
					this.XBBindingsToCopy = new Dictionary<GamepadButton, XBBinding>();
					this.XBBindingsToCopy[42] = xbBinding.HostCollection.LeftStickDirectionalGroup.LeftDirectionValue.Clone();
					this.XBBindingsToCopy[40] = xbBinding.HostCollection.LeftStickDirectionalGroup.UpDirectionValue.Clone();
					this.XBBindingsToCopy[41] = xbBinding.HostCollection.LeftStickDirectionalGroup.DownDirectionValue.Clone();
					this.XBBindingsToCopy[43] = xbBinding.HostCollection.LeftStickDirectionalGroup.RightDirectionValue.Clone();
					if (this.DirectionalAnalogGroup.IsDiagonalDirectionsAllowed)
					{
						this.XBBindingsToCopy[175] = xbBinding.HostCollection.LeftStickDirectionalGroup.UpLeftValue.Clone();
						this.XBBindingsToCopy[176] = xbBinding.HostCollection.LeftStickDirectionalGroup.UpRightValue.Clone();
						this.XBBindingsToCopy[177] = xbBinding.HostCollection.LeftStickDirectionalGroup.DownLeftValue.Clone();
						this.XBBindingsToCopy[178] = xbBinding.HostCollection.LeftStickDirectionalGroup.DownRightValue.Clone();
					}
					if (this.DirectionalAnalogGroup.IsZonesDirectionAllowed)
					{
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.HighZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneLeftValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.HighZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneRightValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.HighZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneUpValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.HighZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneDownValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.HighZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.MedZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneLeftValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.MedZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneRightValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.MedZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneUpValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.MedZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneDownValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.MedZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.LowZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneLeftValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.LowZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneRightValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.LowZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneUpValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.LowZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneDownValue.GamepadButton] = xbBinding.HostCollection.LeftStickDirectionalGroup.LowZoneDownValue.Clone();
					}
					this.IsXInvert = xbBinding.HostCollection.LeftStickDirectionalGroup.IsXInvert;
					this.IsYInvert = xbBinding.HostCollection.LeftStickDirectionalGroup.IsYInvert;
					this.DirectionalAnalogGroup.MouseFlickSmoothing = xbBinding.HostCollection.LeftStickDirectionalGroup.MouseFlickSmoothing;
					this.DirectionalAnalogGroup.MouseFlickSmoothing = xbBinding.HostCollection.LeftStickDirectionalGroup.MouseFlickSmoothing;
					this.IsLeftStickSwapped = xbBinding.HostCollection.IsLeftStickSwapped;
				}
				if (xbBinding.IsRightStickDirection)
				{
					this.DirectionalAnalogGroup = new RightStickDirectionalGroup(xbBinding.HostCollection);
					this.description = xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description;
					xbBinding.HostCollection.RightStickDirectionalGroup.CopyTo(reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup as RightStickDirectionalGroup);
					this.XBBindingsToCopy = new Dictionary<GamepadButton, XBBinding>();
					this.XBBindingsToCopy[49] = xbBinding.HostCollection.RightStickDirectionalGroup.LeftDirectionValue.Clone();
					this.XBBindingsToCopy[47] = xbBinding.HostCollection.RightStickDirectionalGroup.UpDirectionValue.Clone();
					this.XBBindingsToCopy[48] = xbBinding.HostCollection.RightStickDirectionalGroup.DownDirectionValue.Clone();
					this.XBBindingsToCopy[50] = xbBinding.HostCollection.RightStickDirectionalGroup.RightDirectionValue.Clone();
					if (this.DirectionalAnalogGroup.IsDiagonalDirectionsAllowed)
					{
						this.XBBindingsToCopy[179] = xbBinding.HostCollection.RightStickDirectionalGroup.UpLeftValue.Clone();
						this.XBBindingsToCopy[180] = xbBinding.HostCollection.RightStickDirectionalGroup.UpRightValue.Clone();
						this.XBBindingsToCopy[181] = xbBinding.HostCollection.RightStickDirectionalGroup.DownLeftValue.Clone();
						this.XBBindingsToCopy[182] = xbBinding.HostCollection.RightStickDirectionalGroup.DownRightValue.Clone();
					}
					if (this.DirectionalAnalogGroup.IsZonesDirectionAllowed)
					{
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.HighZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneLeftValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.HighZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneRightValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.HighZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneUpValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.HighZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneDownValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.HighZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.MedZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneLeftValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.MedZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneRightValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.MedZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneUpValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.MedZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneDownValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.MedZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.LowZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneLeftValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.LowZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneRightValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.LowZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneUpValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.LowZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneDownValue.GamepadButton] = xbBinding.HostCollection.RightStickDirectionalGroup.LowZoneDownValue.Clone();
					}
					this.IsXInvert = xbBinding.HostCollection.RightStickDirectionalGroup.IsXInvert;
					this.IsYInvert = xbBinding.HostCollection.RightStickDirectionalGroup.IsYInvert;
					this.IsRightStickSwapped = xbBinding.HostCollection.IsRightStickSwapped;
				}
				if (xbBinding.IsGyroTiltDirection)
				{
					this.DirectionalAnalogGroup = new GyroTiltDirectionalGroup(xbBinding.HostCollection);
					this.description = xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description;
					xbBinding.HostCollection.GyroTiltDirectionalGroup.CopyTo(reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup as GyroTiltDirectionalGroup);
					this.XBBindingsToCopy = new Dictionary<GamepadButton, XBBinding>();
					this.XBBindingsToCopy[70] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LeftDirectionValue.Clone();
					this.XBBindingsToCopy[68] = xbBinding.HostCollection.GyroTiltDirectionalGroup.UpDirectionValue.Clone();
					this.XBBindingsToCopy[69] = xbBinding.HostCollection.GyroTiltDirectionalGroup.DownDirectionValue.Clone();
					this.XBBindingsToCopy[71] = xbBinding.HostCollection.GyroTiltDirectionalGroup.RightDirectionValue.Clone();
					if (this.DirectionalAnalogGroup.IsZonesDirectionAllowed)
					{
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneUpValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneDownValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneUpValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneDownValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneDownValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneLeftValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneRightValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneUpValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneUpValue.Clone();
						this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneDownValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneDownValue.Clone();
						if (this.DirectionalAnalogGroup.IsZoneLeanAllowed)
						{
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneLeanLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneLeanLeftValue.Clone();
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.HighZoneLeanRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.HighZoneLeanRightValue.Clone();
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneLeanLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneLeanLeftValue.Clone();
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.MedZoneLeanRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.MedZoneLeanRightValue.Clone();
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneLeanLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneLeanLeftValue.Clone();
							this.XBBindingsToCopy[this.DirectionalAnalogGroup.LowZoneLeanRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LowZoneLeanRightValue.Clone();
						}
					}
					if ((this.DirectionalAnalogGroup as GyroTiltDirectionalGroup).IsGyroMode)
					{
						this.XBBindingsToCopy[(this.DirectionalAnalogGroup as GyroTiltDirectionalGroup).LeanLeftValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LeanLeftValue.Clone();
						this.XBBindingsToCopy[(this.DirectionalAnalogGroup as GyroTiltDirectionalGroup).LeanRightValue.GamepadButton] = xbBinding.HostCollection.GyroTiltDirectionalGroup.LeanRightValue.Clone();
					}
					this.IsXInvert = xbBinding.HostCollection.GyroTiltDirectionalGroup.IsXInvert;
					this.IsYInvert = xbBinding.HostCollection.GyroTiltDirectionalGroup.IsYInvert;
				}
				if (xbBinding.IsMouseDirection)
				{
					this.DirectionalAnalogGroup = new MouseDirectionalGroup(xbBinding.HostCollection);
					this.description = xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description;
					xbBinding.HostCollection.MouseDirectionalGroup.CopyTo(reSENSIApplicationCommands.DirectionData.DirectionalAnalogGroup);
					this.XBBindingsToCopy = new Dictionary<GamepadButton, XBBinding>();
					this.XBBindingsToCopy[63] = xbBinding.HostCollection.MouseDirectionalGroup.HighZoneValue.Clone();
					this.XBBindingsToCopy[141] = xbBinding.HostCollection.MouseDirectionalGroup.HighZoneUpValue.Clone();
					this.XBBindingsToCopy[142] = xbBinding.HostCollection.MouseDirectionalGroup.HighZoneDownValue.Clone();
					this.XBBindingsToCopy[143] = xbBinding.HostCollection.MouseDirectionalGroup.HighZoneLeftValue.Clone();
					this.XBBindingsToCopy[144] = xbBinding.HostCollection.MouseDirectionalGroup.HighZoneRightValue.Clone();
					this.IsXInvert = xbBinding.HostCollection.MouseDirectionalGroup.IsXInvert;
					this.IsYInvert = xbBinding.HostCollection.MouseDirectionalGroup.IsYInvert;
				}
			}

			public void PasteDirection(XBBinding xbBinding)
			{
				if (xbBinding.IsLeftRightStick && (this.DirectionalAnalogGroup.IsLeftStickGroup || this.DirectionalAnalogGroup.IsRightStickGroup))
				{
					BaseStickDirectionalGroup baseStickDirectionalGroup;
					if (xbBinding.IsLeftStickDirection)
					{
						baseStickDirectionalGroup = xbBinding.HostCollection.LeftStickDirectionalGroup;
					}
					else
					{
						baseStickDirectionalGroup = xbBinding.HostCollection.RightStickDirectionalGroup;
					}
					if (xbBinding.HostCollection.IsMainCollection)
					{
						baseStickDirectionalGroup.CopyFrom(this.DirectionalAnalogGroup);
					}
					else
					{
						baseStickDirectionalGroup.CopyFromForShiftCollection(this.DirectionalAnalogGroup);
					}
					xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description = this.description;
					if (this.DirectionalAnalogGroup.IsBoundToMouse)
					{
						baseStickDirectionalGroup.BindToMouse();
					}
					if (this.DirectionalAnalogGroup.IsBoundToVirtualLeftStick)
					{
						baseStickDirectionalGroup.BindToLeftVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsBoundToVirtualRightStick)
					{
						baseStickDirectionalGroup.BindToRightVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsUnmapped != baseStickDirectionalGroup.IsUnmapped)
					{
						baseStickDirectionalGroup.ToggleUnmap();
					}
					if (this.DirectionalAnalogGroup.IsBoundToWASD)
					{
						baseStickDirectionalGroup.BindToWASD();
					}
					if (this.DirectionalAnalogGroup.IsBoundToFlickStick)
					{
						baseStickDirectionalGroup.BindToFlickStick();
						if (baseStickDirectionalGroup.IsMouseSmoothingAvailable)
						{
							baseStickDirectionalGroup.MouseFlickSmoothing = this.DirectionalAnalogGroup.MouseFlickSmoothing;
						}
					}
					object obj = this.IsYInvert && xbBinding.HostCollection.IsMainCollection;
					bool flag = this.IsXInvert && xbBinding.HostCollection.IsMainCollection;
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 40 : 47];
					object obj2 = obj;
					reSENSIApplicationCommands.PasteXBBinding((obj2 != null) ? baseStickDirectionalGroup.DownDirectionValue : baseStickDirectionalGroup.UpDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 41 : 48];
					reSENSIApplicationCommands.PasteXBBinding((obj2 != null) ? baseStickDirectionalGroup.UpDirectionValue : baseStickDirectionalGroup.DownDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 42 : 49];
					reSENSIApplicationCommands.PasteXBBinding(flag ? baseStickDirectionalGroup.RightDirectionValue : baseStickDirectionalGroup.LeftDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 43 : 50];
					reSENSIApplicationCommands.PasteXBBinding(flag ? baseStickDirectionalGroup.LeftDirectionValue : baseStickDirectionalGroup.RightDirectionValue);
					if (this.DirectionalAnalogGroup.IsDiagonalDirectionsAllowed)
					{
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 175 : 179];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.UpLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 176 : 180];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.UpRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 177 : 181];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.DownLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 178 : 182];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.DownRightValue);
					}
					if (this.DirectionalAnalogGroup.IsZonesDirectionAllowed)
					{
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 39 : 46];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.HighZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 119 : 131];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.HighZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 120 : 132];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.HighZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 117 : 129];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.HighZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 118 : 130];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.HighZoneDownValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 38 : 45];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.MedZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 115 : 127];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.MedZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 116 : 128];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.MedZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 113 : 125];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.MedZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 114 : 126];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.MedZoneDownValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 37 : 44];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.LowZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 111 : 123];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.LowZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 112 : 124];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.LowZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 109 : 121];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.LowZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[this.DirectionalAnalogGroup.IsLeftStickGroup ? 110 : 122];
						reSENSIApplicationCommands.PasteXBBinding(baseStickDirectionalGroup.LowZoneDownValue);
					}
					if (xbBinding.HostCollection.IsMainCollection)
					{
						if (this.IsXInvert != baseStickDirectionalGroup.IsXInvert)
						{
							baseStickDirectionalGroup.HostCollection.InvertStickX(baseStickDirectionalGroup.IsLeftStickGroup ? 0 : 1);
						}
						if (this.IsYInvert != baseStickDirectionalGroup.IsYInvert)
						{
							baseStickDirectionalGroup.HostCollection.InvertStickY(baseStickDirectionalGroup.IsLeftStickGroup ? 0 : 1);
						}
					}
					reSENSIApplicationCommands.XBBindingToCopy = null;
				}
				if (xbBinding.IsGyroTiltDirection && this.DirectionalAnalogGroup.IsGyroTiltGroup)
				{
					GyroTiltDirectionalGroup gyroTiltDirectionalGroup = xbBinding.HostCollection.GyroTiltDirectionalGroup;
					if (xbBinding.HostCollection.IsMainCollection)
					{
						gyroTiltDirectionalGroup.CopyFrom(this.DirectionalAnalogGroup as GyroTiltDirectionalGroup);
					}
					else
					{
						gyroTiltDirectionalGroup.CopyFromForShiftCollection(this.DirectionalAnalogGroup as GyroTiltDirectionalGroup);
					}
					xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description = this.description;
					if (this.DirectionalAnalogGroup.IsBoundToMouse)
					{
						gyroTiltDirectionalGroup.BindToMouse();
					}
					if (this.DirectionalAnalogGroup.IsBoundToVirtualLeftStick)
					{
						gyroTiltDirectionalGroup.BindToLeftVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsBoundToVirtualRightStick)
					{
						gyroTiltDirectionalGroup.BindToRightVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsUnmapped != gyroTiltDirectionalGroup.IsUnmapped)
					{
						gyroTiltDirectionalGroup.ToggleUnmap();
					}
					if (this.DirectionalAnalogGroup.IsBoundToWASD)
					{
						gyroTiltDirectionalGroup.BindToWASD();
					}
					object obj3 = this.IsYInvert && xbBinding.HostCollection.IsMainCollection;
					bool flag2 = this.IsXInvert && xbBinding.HostCollection.IsMainCollection;
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[68];
					object obj4 = obj3;
					reSENSIApplicationCommands.PasteXBBinding((obj4 != null) ? gyroTiltDirectionalGroup.DownDirectionValue : gyroTiltDirectionalGroup.UpDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[69];
					reSENSIApplicationCommands.PasteXBBinding((obj4 != null) ? gyroTiltDirectionalGroup.UpDirectionValue : gyroTiltDirectionalGroup.DownDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[70];
					reSENSIApplicationCommands.PasteXBBinding(flag2 ? gyroTiltDirectionalGroup.RightDirectionValue : gyroTiltDirectionalGroup.LeftDirectionValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[71];
					reSENSIApplicationCommands.PasteXBBinding(flag2 ? gyroTiltDirectionalGroup.LeftDirectionValue : gyroTiltDirectionalGroup.RightDirectionValue);
					if (xbBinding.HostCollection.IsMainCollection)
					{
						if (this.IsXInvert != gyroTiltDirectionalGroup.IsXInvert)
						{
							gyroTiltDirectionalGroup.HostCollection.InvertStickX(3);
						}
						if (this.IsYInvert != gyroTiltDirectionalGroup.IsYInvert)
						{
							gyroTiltDirectionalGroup.HostCollection.InvertStickY(3);
						}
					}
					if (this.DirectionalAnalogGroup.IsZonesDirectionAllowed)
					{
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneLeftValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneRightValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneUpValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneDownValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneDownValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneLeftValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneRightValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneUpValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneDownValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneDownValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneLeftValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneRightValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneRightValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneUpValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneUpValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneDownValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneDownValue);
						if (this.DirectionalAnalogGroup.IsZoneLeanAllowed)
						{
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneLeanLeftValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneLeanLeftValue);
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.HighZoneLeanRightValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.HighZoneLeanRightValue);
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneLeanLeftValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneLeanLeftValue);
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.MedZoneLeanRightValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.MedZoneLeanRightValue);
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneLeanLeftValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneLeanLeftValue);
							reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LowZoneLeanRightValue.GamepadButton];
							reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LowZoneLeanRightValue);
						}
					}
					if ((this.DirectionalAnalogGroup as GyroTiltDirectionalGroup).IsGyroMode)
					{
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LeanLeftValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LeanLeftValue);
						reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[gyroTiltDirectionalGroup.LeanRightValue.GamepadButton];
						reSENSIApplicationCommands.PasteXBBinding(gyroTiltDirectionalGroup.LeanRightValue);
					}
					gyroTiltDirectionalGroup.UpdateProperties();
					reSENSIApplicationCommands.XBBindingToCopy = null;
				}
				if (xbBinding.IsMouseDirection && this.DirectionalAnalogGroup.IsMouseGroup)
				{
					MouseDirectionalGroup mouseDirectionalGroup = xbBinding.HostCollection.MouseDirectionalGroup;
					mouseDirectionalGroup.CopyFrom(this.DirectionalAnalogGroup);
					xbBinding.ActivatorXBBindingDictionary.TryGetValue(0).Description = this.description;
					if (this.DirectionalAnalogGroup.IsBoundToVirtualLeftStick)
					{
						mouseDirectionalGroup.BindToLeftVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsBoundToVirtualRightStick)
					{
						mouseDirectionalGroup.BindToRightVirtualStick();
					}
					if (this.DirectionalAnalogGroup.IsUnmapped != mouseDirectionalGroup.IsUnmapped)
					{
						mouseDirectionalGroup.ToggleUnmap();
					}
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[63];
					reSENSIApplicationCommands.PasteXBBinding(mouseDirectionalGroup.HighZoneValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[141];
					reSENSIApplicationCommands.PasteXBBinding(this.IsYInvert ? mouseDirectionalGroup.HighZoneDownValue : mouseDirectionalGroup.HighZoneUpValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[142];
					reSENSIApplicationCommands.PasteXBBinding(this.IsYInvert ? mouseDirectionalGroup.HighZoneUpValue : mouseDirectionalGroup.HighZoneDownValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[143];
					reSENSIApplicationCommands.PasteXBBinding(this.IsXInvert ? mouseDirectionalGroup.HighZoneRightValue : mouseDirectionalGroup.HighZoneLeftValue);
					reSENSIApplicationCommands.XBBindingToCopy = this.XBBindingsToCopy[144];
					reSENSIApplicationCommands.PasteXBBinding(this.IsXInvert ? mouseDirectionalGroup.HighZoneLeftValue : mouseDirectionalGroup.HighZoneRightValue);
					if (this.IsXInvert != mouseDirectionalGroup.IsXInvert)
					{
						mouseDirectionalGroup.InvertStickX(true, false);
					}
					if (this.IsYInvert != mouseDirectionalGroup.IsYInvert)
					{
						mouseDirectionalGroup.InvertStickY(true, false);
					}
					reSENSIApplicationCommands.XBBindingToCopy = null;
					mouseDirectionalGroup.UpdateProperties();
				}
			}

			public BaseDirectionalAnalogGroup DirectionalAnalogGroup;

			public DPADDirectionalGroup DirectionalDPADGroup;

			private Dictionary<GamepadButton, XBBinding> XBBindingsToCopy;

			public bool IsXInvert;

			public bool IsYInvert;

			public bool IsLeftStickSwapped;

			public bool IsRightStickSwapped;

			private string description;
		}

		private class CopyShiftConfigData
		{
			public ConfigData configData;

			public int shiftIndex;
		}

		[CompilerGenerated]
		private static class <>O
		{
			public static Action<XBBinding> <0>__CopyBinding;

			public static Func<XBBinding, bool> <1>__CopyBindingCanExecute;

			public static Action<XBBinding> <2>__PasteBinding;

			public static Func<XBBinding, bool> <3>__PasteBindingCanExecute;

			public static Action<XBBinding> <4>__CopyBindingGroup;

			public static Func<XBBinding, bool> <5>__CopyBindingGroupCanExecute;

			public static Action<XBBinding> <6>__PasteBindingGroup;

			public static Func<XBBinding, bool> <7>__PasteBindingGroupCanExecute;
		}
	}
}
