using System;

namespace reSENSIUI.Properties
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
	public sealed class ItemCanBeNullAttribute : Attribute
	{
	}
}
