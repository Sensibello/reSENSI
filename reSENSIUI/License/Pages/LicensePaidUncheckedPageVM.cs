using System;
using DiscSoft.NET.Common.Localization;

namespace reSENSIUI.License.Pages
{
	internal class LicensePaidUncheckedPageVM : BaseLicensePage
	{
		public LicensePaidUncheckedPageVM(LicenseInfoModel licenseInfo)
			: base(licenseInfo)
		{
		}

		public string Description
		{
			get
			{
				return string.Format(DTLocalization.GetString(12144), "reSENSI");
			}
		}
	}
}
