using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using reSENSICommon.Infrastructure.Enums;
using reSENSIUI.Utils.XBUtil;

namespace reSENSIUI.Utils.Converters
{
	[ValueConversion(typeof(ActivatorType), typeof(string))]
	public class ActivatorTypeToDescriptionConverter : MarkupExtension, IValueConverter, IMultiValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			ActivatorTypeToDescriptionConverter activatorTypeToDescriptionConverter;
			if ((activatorTypeToDescriptionConverter = ActivatorTypeToDescriptionConverter._converter) == null)
			{
				activatorTypeToDescriptionConverter = (ActivatorTypeToDescriptionConverter._converter = new ActivatorTypeToDescriptionConverter());
			}
			return activatorTypeToDescriptionConverter;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			return XBUtils.GetDescriptionForActivator((ActivatorType)value, false);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Any((object v) => v == null || v == DependencyProperty.UnsetValue))
			{
				return null;
			}
			ActivatorType activatorType = (ActivatorType)values[0];
			bool flag = false;
			return XBUtils.GetDescriptionForActivator(activatorType, flag);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private static ActivatorTypeToDescriptionConverter _converter;
	}
}
