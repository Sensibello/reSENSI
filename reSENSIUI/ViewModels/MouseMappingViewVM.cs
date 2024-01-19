using System;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using reSENSIUI.Infrastructure;
using reSENSIUI.Infrastructure.KeyBindings;
using reSENSIUI.Infrastructure.KeyBindings.XB;
using reSENSIUI.ViewModels.Base;

namespace reSENSIUI.ViewModels
{
	public class MouseMappingViewVM : BaseServicesVM, INavigationAware
	{
		public DelegateCommand<XBBinding> ShowMacroSettingsCommand
		{
			get
			{
				DelegateCommand<XBBinding> delegateCommand;
				if ((delegateCommand = this._showMacroSettings) == null)
				{
					delegateCommand = (this._showMacroSettings = new DelegateCommand<XBBinding>(new Action<XBBinding>(this.ShowMacroSettings)));
				}
				return delegateCommand;
			}
		}

		private void ShowMacroSettings(XBBinding xbBinding)
		{
			reSENSIApplicationCommands.ShowMacroSettings(xbBinding);
		}

		public MouseMappingViewVM(IContainerProvider uc)
			: base(uc)
		{
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			BaseXBBindingCollection realCurrentBeingMappedBindingCollection = base.GameProfilesService.RealCurrentBeingMappedBindingCollection;
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			if (base.GameProfilesService.RealCurrentBeingMappedBindingCollection == null)
			{
				return;
			}
			if (navigationContext.NavigationService.Region.Name == RegionNames.Gamepad && !navigationContext.Uri.ToString().Contains("MacroSettings"))
			{
				base.GameProfilesService.RealCurrentBeingMappedBindingCollection.ControllerBindings.CurrentEditItem = null;
			}
		}

		private DelegateCommand<XBBinding> _showMacroSettings;
	}
}
