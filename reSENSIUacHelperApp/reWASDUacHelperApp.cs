using System;
using reSENSICommon;
using UacHelper;

namespace reSENSIUACHelper
{
	public class reSENSIUacHelperApp : UacHelperBaseApp
	{
		public override string WCF_MODE_MUTEX_NAME
		{
			get
			{
				return "reSENSIUACWCFMutexName";
			}
		}

		protected override string PIPE_NAME
		{
			get
			{
				return "reSENSIUACWCFPipeName";
			}
		}

		protected override Type ServiceType
		{
			get
			{
				return typeof(AdminOperationsService);
			}
		}

		protected override Type ServiceInterfaceType
		{
			get
			{
				return typeof(IAdminOperations);
			}
		}

		protected override int WaitTimeout
		{
			get
			{
				return 10000;
			}
		}

		public static void Main(string[] args)
		{
			new reSENSIUacHelperApp().Run();
		}

		public const string reSENSI_UAC_WCF_MODE_MUTEX_NAME = "reSENSIUACWCFMutexName";

		public const string reSENSI_UAC_WCF_PIPE_NAME = "reSENSIUACWCFPipeName";
	}
}
