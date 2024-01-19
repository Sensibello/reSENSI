using System;

namespace reSENSIUI.Properties
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class CannotApplyEqualityOperatorAttribute : Attribute
	{
	}
}
