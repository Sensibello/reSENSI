using System;

namespace reSENSIUI.Properties
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
	{
		public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		[NotNull]
		public string Format { get; private set; }
	}
}
