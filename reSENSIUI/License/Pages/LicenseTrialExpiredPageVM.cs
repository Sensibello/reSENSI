using System;
using DiscSoft.NET.Common.Localization;

namespace reSENSIUI.License.Pages
{
	internal class LicenseTrialExpiredPageVM : BaseLicensePage
	{
		public LicenseTrialExpiredPageVM(LicenseInfoModel licenseInfo)
			: base(licenseInfo)
		{
		}

		public string EnjoyedTrial
		{
			get
			{
				return string.Format(DTLocalization.GetString(9157), "reSENSI");
			}
		}

		public string LinkName
		{
			get
			{
				return "Visit the official store to get the full version";
			}
		}

		public bool IsLongTrialExpired
		{
			get
			{
				return this._licenseInfo.TrialDaysLeft < -3;
			}
		}
	}
}
