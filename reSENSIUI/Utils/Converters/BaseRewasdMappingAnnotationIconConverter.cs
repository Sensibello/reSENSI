using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using XBEliteWPF.Infrastructure.reSENSIMapping;
using XBEliteWPF.Utils.XBUtilModel;

namespace reSENSIUI.Utils.Converters
{
	public class BasereSENSIMappingAnnotationIconConverter : MarkupExtension, IValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			BasereSENSIMappingAnnotationIconConverter basereSENSIMappingAnnotationIconConverter;
			if ((basereSENSIMappingAnnotationIconConverter = BasereSENSIMappingAnnotationIconConverter._converter) == null)
			{
				basereSENSIMappingAnnotationIconConverter = (BasereSENSIMappingAnnotationIconConverter._converter = new BasereSENSIMappingAnnotationIconConverter());
			}
			return basereSENSIMappingAnnotationIconConverter;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is BasereSENSIMapping))
			{
				return null;
			}
			return XBUtils.GetAnnotationDrawingForBasereSENSIMapping((BasereSENSIMapping)value, parameter == null);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private static BasereSENSIMappingAnnotationIconConverter _converter;
	}
}
