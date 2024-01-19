using System;
using System.Collections.ObjectModel;
using reSENSICommon.Infrastructure.Enums;
using reSENSIUI.Infrastructure.Controller;
using XBEliteWPF.Infrastructure;
using XBEliteWPF.Infrastructure.reSENSIMapping;
using XBEliteWPF.Infrastructure.reSENSIMapping.KeyScanCodes;

namespace reSENSIUI.Services.Interfaces
{
	public interface IKeyBindingService
	{
		IGameProfilesService GameProfilesService { get; }

		ObservableCollection<BasereSENSIMapping> reSENSIMappingsCollection { get; }

		ObservableCollection<KeyScanCodeV2> KeyScanCodeCollectionForKeyboard { get; }

		ObservableCollection<KeyScanCodeV2> KeyScanCodeCollectionForMouse { get; }

		ObservableCollection<KeyScanCodeV2> KeyScanCodeCollectionForGamepad { get; }

		ObservableCollection<BasereSENSIMapping> reSENSIMappingsCollectionWithoutMouseAndScrolls { get; }

		ObservableCollection<BasereSENSIMapping> reSENSIMappingsCollectionWithDoNotInherit { get; }

		ObservableCollection<BasereSENSIMapping> reSENSIMappingsCollectionWithoutMouseAndScrollsWithDoNotInherit { get; }

		ObservableCollection<GamepadButtonDescription> ButtonsToRemap { get; }

		ObservableCollection<GamepadButton> ButtonsForMask { get; }

		ObservableCollection<GamepadButtonDescription> ShiftModifierButtons { get; }

		void RecreateKeyScanCodeCollectionForreSENSIMappings(VirtualGamepadType virtualGamepadType = 0);

		void RaiseButtonsToRemapCollectionChanged();

		ObservableCollection<GamepadButton> GenerateButtonsForController(BaseControllerVM gamepadVM = null);

		ObservableCollection<GamepadButtonDescription> GenerateRemapButtonDescriptionsForController(BaseControllerVM gamepadVM = null, bool includeUnmapable = false, GamepadButtonDescription currentButtonDescription = null);

		ObservableCollection<KeyScanCodeV2> GenerateKeysForController(BaseControllerVM controllerVM = null, bool isKeyboardExpected = false, bool isMouseExpected = false, bool isMaskMode = false, bool isGamepad = false);
	}
}
