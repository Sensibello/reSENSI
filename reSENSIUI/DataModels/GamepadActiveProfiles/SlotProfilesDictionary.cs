﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DiscSoft.NET.Common.Utils;
using reSENSICommon.Infrastructure.Enums;

namespace reSENSIUI.DataModels.GamepadActiveProfiles
{
	[Serializable]
	public class SlotProfilesDictionary : ObservableDictionary<Slot, GamepadProfile>
	{
		public SlotProfilesDictionary()
		{
		}

		public SlotProfilesDictionary(ObservableDictionary<Slot, GamepadProfile> dictionary)
		{
			foreach (KeyValuePair<Slot, GamepadProfile> keyValuePair in ((IEnumerable<KeyValuePair<Slot, GamepadProfile>>)dictionary))
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		protected SlotProfilesDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "AdditionalParameters")
				{
					this.AdditionalParameters = (Dictionary<string, string>)info.GetValue("AdditionalParameters", typeof(Dictionary<string, string>));
				}
			}
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("AdditionalParameters", this.AdditionalParameters);
		}

		public readonly Dictionary<string, string> AdditionalParameters = new Dictionary<string, string>();
	}
}
