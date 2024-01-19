using System;
using Prism.Ioc;

namespace reSENSIUI.ViewModels.Base
{
	public abstract class BaseKeyBindVM : BaseServicesVM
	{
		public BaseKeyBindVM(IContainerProvider uc)
			: base(uc)
		{
		}
	}
}
