using System;
using Prism.Ioc;
using reSENSIUI.ViewModels.Base;

namespace reSENSIUI.ViewModels.ContentZoneGamepad
{
	public class ContentTriggersVM : BaseKeyBindVM
	{
		public ContentTriggersVM(IContainerProvider uc)
			: base(uc)
		{
		}
	}
}
