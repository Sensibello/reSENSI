using System;
using Microsoft.AspNetCore.Mvc;
using reSENSICommon.Network.HTTP.DataTransferObjects;

namespace reSENSIEngine.Services.HttpServer
{
	[Route("Version")]
	public class VersionController : ControllerBase
	{
		[HttpGet]
		[Route("")]
		public IActionResult Get()
		{
			return this.Ok(new HttpVersion());
		}
	}
}
