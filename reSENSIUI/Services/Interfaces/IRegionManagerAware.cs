using System;
using Prism.Regions;

namespace reSENSIUI.Services.Interfaces
{
	public interface IRegionManagerAware
	{
		IRegionManager RegionManager { get; set; }
	}
}
