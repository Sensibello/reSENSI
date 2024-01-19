using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using reSENSIUI.Infrastructure.KeyBindings;
using reSENSIUI.Infrastructure.KeyBindings.MacroBinding;
using XBEliteWPF.Infrastructure.KeyBindingsModel;
using XBEliteWPF.Infrastructure.KeyBindingsModel.MacroBinding;

namespace reSENSIUI.Utils.Extensions
{
	public static class KeyBindingExtensions
	{
		public static List<AssociatedControllerButton> Convert(this ObservableCollection<AssociatedControllerButton> collection)
		{
			List<AssociatedControllerButton> list = new List<AssociatedControllerButton>();
			foreach (AssociatedControllerButton associatedControllerButton in collection)
			{
				list.Add(associatedControllerButton.Convert());
			}
			return list;
		}

		public static AssociatedControllerButton Convert(this AssociatedControllerButton acb)
		{
			return new AssociatedControllerButton
			{
				GamepadButtonDescription = acb.GamepadButtonDescription,
				KeyScanCode = acb.KeyScanCode,
				ControllerBindingFrameMode = acb.ControllerBindingFrameMode
			};
		}

		public static MacroKeyBinding Convert(this MacroKeyBinding mkb)
		{
			return new MacroKeyBinding(mkb.KeyScanCode, mkb.MacroKeyType);
		}
	}
}
