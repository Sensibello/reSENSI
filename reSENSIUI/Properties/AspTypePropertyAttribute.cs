using System;

namespace reSENSIUI.Properties
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class AspTypePropertyAttribute : Attribute
	{
		public bool CreateConstructorReferences { get; private set; }

		public AspTypePropertyAttribute(bool createConstructorReferences)
		{
			this.CreateConstructorReferences = createConstructorReferences;
		}
	}
}
