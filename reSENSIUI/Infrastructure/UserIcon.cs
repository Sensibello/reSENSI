using System;
using System.Collections.Generic;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSIUI.Infrastructure
{
	public class UserIcon : RadialMenuIcon
	{
		public UserIcon(string resource, List<RadialMenuIconCategory> categories, string description = null)
			: base(resource, categories, description, 1)
		{
		}

		public override RadialMenuIcon Clone()
		{
			return new UserIcon(base.Resource, base.Categories, base.Description);
		}
	}
}
