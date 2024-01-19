using System;
using CommandLine;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSICommandLine.Verbs
{
	[Verb("apply", false, HelpText = "Apply config(s).\nExample: reSENSICommandLine.exe apply --id 318984554375544835;318984554375544834\n         --path \"C:\\Users\\Public\\Documents\\reSENSI\\Profiles\\Profile1\\Controller\\Config1.reSENSI\" --slot slot1")]
	internal class ApplyConfigVerb
	{
		[Option("id", HelpText = "Device ID. 'all_gamepad' to apply to all gamepads.", Required = true)]
		public string id { get; set; }

		[Option("path", HelpText = "Full path to config that will be applied.", Required = true)]
		public string configPath { get; set; }

		[Option("slot", HelpText = "Device slot. If not specified - first slot will be used.", Required = false)]
		public Slot? slot { get; set; }

		public const string ALL_GAMEPADS = "all_gamepad";
	}
}
