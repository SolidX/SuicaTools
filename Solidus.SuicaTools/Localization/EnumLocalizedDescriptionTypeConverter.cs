using System.ComponentModel;
using System.Globalization;

namespace Solidus.SuicaTools.Localization
{
    public class EnumLocalizedDescriptionTypeConverter : EnumConverter
    {
        public EnumLocalizedDescriptionTypeConverter(Type type) : base(type)
        {
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    var fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {

                        var attributes = (LocalizedDescriptionAttribute[])fi.GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false);
                        if (attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description))
                        {
                            if (culture == null)
                                return attributes[0].Description;
                            else
                            {
                                attributes[0].Culture = culture;
                                return attributes[0].Description;
                            }
                        }
                        
                        return value.ToString();
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
