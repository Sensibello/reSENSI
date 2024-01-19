using System;

namespace reSENSIUI.Infrastructure
{
	internal interface ISetLoginPasswordModel
	{
		string Login { get; set; }

		string Password { get; set; }
	}
}
