using System;
using Prism.Ioc;
using reSENSIUI.ViewModels.Base;

namespace reSENSIUI.ViewModels.ContentZoneGamepad
{
	public class ContentPaddlesVM : BaseKeyBindVM
	{
		public ContentPaddlesVM(IContainerProvider uc)
			: base(uc)
		{
		}
	}
}
