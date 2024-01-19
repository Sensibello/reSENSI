﻿using System;
using System.Windows.Media;
using DiscSoft.NET.Common.ColorStuff;
using DiscSoft.NET.Common.Interfaces;
using DiscSoft.NET.Common.WCF;
using reSENSICommon;
using reSENSICommon.Infrastructure.Enums;
using reSENSICommon._3dPartyManufacturersAPI;

namespace reSENSIUI.Utils
{
	public class LedOperationsDecider : BaseWCFOperationDecider<IThirdPartyOperations, ThirdPartyServiceEmpty>, IThirdPartyOperations, IInterProcessCommunicationServiceWCF
	{
		protected override string HelperApplicationName
		{
			get
			{
				return "reSENSI3rdPartyHelper.exe";
			}
		}

		protected override string HelperApplicationMutex
		{
			get
			{
				return "reSENSIThirdPartyWCFMutexName";
			}
		}

		protected override string PipeName
		{
			get
			{
				return "reSENSIThirdPartyWCFPipeName";
			}
		}

		protected override string RunVerb
		{
			get
			{
				return "";
			}
		}

		protected override bool IsExecuteInternally
		{
			get
			{
				return false;
			}
		}

		public LedOperationsDecider Instance
		{
			get
			{
				if (this._instance == null)
				{
					this._instance = new LedOperationsDecider();
				}
				return this._instance;
			}
		}

		public bool ExitHelper()
		{
			return !base.IsWCFServiceRuning() || base.DecideAndExecuteBoolServiceOperation("ExitHelper");
		}

		public bool IsAnyServiceRunning()
		{
			return base.DecideAndExecuteBoolServiceOperation("IsAnyServiceRunning");
		}

		public bool IncrementActiveConfigs()
		{
			return base.DecideAndExecuteBoolServiceOperation("IncrementActiveConfigs");
		}

		public bool DecrementActiveConfigs()
		{
			return base.DecideAndExecuteBoolServiceOperation("DecrementActiveConfigs");
		}

		public bool Stop(bool resetColor = true, bool resetPlayerLed = true, bool stopMice = true, bool stopKeyboards = true)
		{
			if (!base.IsWCFServiceRuning())
			{
				return true;
			}
			object[] array = new object[] { resetColor, resetPlayerLed, stopMice, stopKeyboards };
			return base.DecideAndExecuteBoolServiceOperationWithParams(array, "Stop");
		}

		public bool Stop2(LEDDeviceInfo ledDeviceInfo, bool resetColor = true, bool resetPlayerLed = true)
		{
			if (!base.IsWCFServiceRuning())
			{
				return true;
			}
			object[] array = new object[] { ledDeviceInfo, resetColor, resetPlayerLed };
			return base.DecideAndExecuteBoolServiceOperationWithParams(array, "Stop2");
		}

		public bool SetColor1(zColor color, LEDColorMode ledColorMode = 1, int durationMS = 5000)
		{
			object[] array = new object[] { color, ledColorMode, durationMS };
			return base.DecideAndExecuteBoolServiceOperationWithParams(array, "SetColor1");
		}

		public bool SetColor2(Color color, LEDDeviceInfo ledDeviceInfo, LEDColorMode ledColorMode = 1, int durationMS = 5000, bool isAllowColorSolidOrchestratorRedirect = true)
		{
			object[] array = new object[] { color, ledDeviceInfo, ledColorMode, durationMS, isAllowColorSolidOrchestratorRedirect };
			return base.DecideAndExecuteBoolServiceOperationWithParams(array, "SetColor2");
		}

		public bool SetColor3(zColor color, LEDDeviceInfo ledDeviceInfo, LEDColorMode ledColorMode = 1, int durationMS = 5000, bool isAllowColorSolidOrchestratorRedirect = true)
		{
			object[] array = new object[] { color, ledDeviceInfo, ledColorMode, durationMS, isAllowColorSolidOrchestratorRedirect };
			return base.DecideAndExecuteBoolServiceOperationWithParams(array, "SetColor3");
		}

		private LedOperationsDecider _instance;
	}
}
