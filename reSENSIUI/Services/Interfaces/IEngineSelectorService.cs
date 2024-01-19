using System;
using System.Collections.ObjectModel;

namespace reSENSIUI.Services.Interfaces
{
	public interface IEngineSelectorService
	{
		ObservableCollection<EngineInfo> Engines { get; set; }

		EngineInfo CurrentEngine { get; set; }
	}
}
