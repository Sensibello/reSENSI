using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using reSENSIUI.ViewModels.CommunityConfigs;
using XBEliteWPF.Infrastructure.reSENSIMapping;

namespace reSENSIUI.ViewModels
{
	internal class SupportedGamepadsConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(BasereSENSIMapping).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			{
				return null;
			}
			{
				JArray.Load(reader);
				return null;
			}
			SupportedGamepads supportedGamepads = new SupportedGamepads();
			try
			{
				{
					supportedGamepads.Gamepads.Add(new Gamepad
					{
						Id = keyValuePair.Key,
						Name = (string)keyValuePair.Value
					});
				}
				if (supportedGamepads.Gamepads.Last<Gamepad>() != null)
				{
					supportedGamepads.Gamepads.Last<Gamepad>().IsLast = true;
				}
				return supportedGamepads;
			}
			catch (Exception)
			{
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JObject.FromObject(value, serializer).WriteTo(writer, Array.Empty<JsonConverter>());
		}
	}
}
