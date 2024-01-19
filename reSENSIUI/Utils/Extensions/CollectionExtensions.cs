using System;
using System.Collections.Generic;
using System.Linq;
using DiscSoft.NET.Common.Utils;
using DiscSoftreSENSIServiceNamespace;
using reSENSIUI.Infrastructure.Controller;
using XBEliteWPF.Infrastructure;

namespace reSENSIUI.Utils.Extensions
{
	public static class CollectionExtensions
	{
		public static reSENSI_CONTROLLER_PROFILE_EX FindByID(this IEnumerable<reSENSI_CONTROLLER_PROFILE_EX> collection, string ID)
		{
			if (string.IsNullOrEmpty(ID))
			{
				return null;
			}
			Func<ulong, bool> <>9__2;
			Func<reSENSI_CONTROLLER_PROFILE, bool> <>9__1;
			return collection.FirstOrDefault(delegate(reSENSI_CONTROLLER_PROFILE_EX pex)
			{
				IEnumerable<reSENSI_CONTROLLER_PROFILE> profiles = pex.Profiles;
				Func<reSENSI_CONTROLLER_PROFILE, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(reSENSI_CONTROLLER_PROFILE p)
					{
						IEnumerable<ulong> id2 = p.Id;
						Func<ulong, bool> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (ulong id) => id != 0UL && ID.Contains(id.ToString()));
						}
						return id2.Any(func2);
					});
				}
				return profiles.Any(func);
			});
		}

		public static reSENSI_CONTROLLER_PROFILE_EX FindByID(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, string ID)
		{
			Wrapper<reSENSI_CONTROLLER_PROFILE_EX> wrapper = collection.FindWrapperByID(ID);
			if (wrapper == null)
			{
				return null;
			}
			return wrapper.Value;
		}

		public static Wrapper<reSENSI_CONTROLLER_PROFILE_EX> FindWrapperByID(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, string ID)
		{
			if (string.IsNullOrEmpty(ID))
			{
				return null;
			}
			Func<ulong, bool> <>9__2;
			Func<reSENSI_CONTROLLER_PROFILE, bool> <>9__1;
			return collection.FirstOrDefault(delegate(Wrapper<reSENSI_CONTROLLER_PROFILE_EX> pex)
			{
				IEnumerable<reSENSI_CONTROLLER_PROFILE> profiles = pex.Value.Profiles;
				Func<reSENSI_CONTROLLER_PROFILE, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(reSENSI_CONTROLLER_PROFILE p)
					{
						IEnumerable<ulong> id2 = p.Id;
						Func<ulong, bool> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (ulong id) => id != 0UL && ID.Contains(id.ToString()));
						}
						return id2.Any(func2);
					});
				}
				return profiles.Any(func);
			});
		}

		public static reSENSI_CONTROLLER_PROFILE_EX FindByControllerID(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, ulong controllerId)
		{
			Wrapper<reSENSI_CONTROLLER_PROFILE_EX> wrapper = collection.FindWrapperByControllerID(controllerId);
			if (wrapper == null)
			{
				return null;
			}
			return wrapper.Value;
		}

		public static Wrapper<reSENSI_CONTROLLER_PROFILE_EX> FindWrapperByControllerID(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, ulong controllerId)
		{
			if (controllerId == 0UL)
			{
				return null;
			}
			Func<ulong, bool> <>9__2;
			Func<reSENSI_CONTROLLER_PROFILE, bool> <>9__1;
			return collection.FirstOrDefault(delegate(Wrapper<reSENSI_CONTROLLER_PROFILE_EX> pex)
			{
				IEnumerable<reSENSI_CONTROLLER_PROFILE> profiles = pex.Value.Profiles;
				Func<reSENSI_CONTROLLER_PROFILE, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(reSENSI_CONTROLLER_PROFILE p)
					{
						IEnumerable<ulong> id2 = p.Id;
						Func<ulong, bool> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (ulong id) => id != 0UL && id == controllerId);
						}
						return id2.Any(func2);
					});
				}
				return profiles.Any(func);
			});
		}

		public static Wrapper<reSENSI_CONTROLLER_PROFILE_EX> FindWrapperByServiceProfileId(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, ulong serviceProfileId)
		{
			if (serviceProfileId == 0UL)
			{
				return null;
			}
			Func<ushort, bool> <>9__1;
			return collection.FirstOrDefault(delegate(Wrapper<reSENSI_CONTROLLER_PROFILE_EX> pex)
			{
				IEnumerable<ushort> serviceProfileIds = pex.Value.ServiceProfileIds;
				Func<ushort, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (ushort p) => (ulong)p == serviceProfileId);
				}
				return serviceProfileIds.Any(func);
			});
		}

		public static bool ContainsProfileForID(this IEnumerable<Wrapper<reSENSI_CONTROLLER_PROFILE_EX>> collection, string ID)
		{
			return collection.FindByID(ID) != null;
		}

		public static bool ContainsControllerInfoForControllerId(this IEnumerable<reSENSI_CONTROLLER_INFO> collection, ulong controllerId)
		{
			return collection.Any((reSENSI_CONTROLLER_INFO rci) => rci.Id == controllerId);
		}

		public static reSENSI_CONTROLLER_INFO? FindControllerInfoForControllerId(this IEnumerable<reSENSI_CONTROLLER_INFO> collection, ulong controllerId)
		{
			return new reSENSI_CONTROLLER_INFO?(collection.FirstOrDefault((reSENSI_CONTROLLER_INFO rci) => rci.Id == controllerId));
		}

		public static BaseControllerVM FindControllerByControllerId(this IEnumerable<BaseControllerVM> collection, ulong controllerId)
		{
			return collection.FirstOrDefault((BaseControllerVM c) => c.ID.Contains(controllerId.ToString()));
		}
	}
}
