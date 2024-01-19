using System;
using reSENSIUI.Services.HttpClient;

namespace reSENSIUI.Services.Interfaces
{
	public interface IHttpClientService
	{
		EngineClientService Engine { get; }

		GamepadClientService Gamepad { get; }

		GameProfilesClientService GameProfiles { get; }

		ExternalDevicesClientService ExternalDevices { get; }

		LicenseApi LicenseApi { get; }
	}
}
