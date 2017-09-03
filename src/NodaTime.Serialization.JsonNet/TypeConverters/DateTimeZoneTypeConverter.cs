using System;
using System.ComponentModel;
using System.Globalization;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for time zones, using the zone provider
    /// in <see cref="DateTimeZoneProviderAttribute"/>.
    /// </summary>
    public class DateTimeZoneTypeConverter : NodaTypeConverterBase
    {
        /// <summary>
        /// Converts the given object to the type of this converter, using the specified
        /// context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="object"/> to convert.</param>
        /// <returns>An <see cref="object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                IDateTimeZoneProvider provider = GetZoneProvider(context);
                return provider[(string)value];
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context
        /// and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="object"/> to object.</param>
        /// <param name="destinationType">The type to convert the value parameter to.</param>
        /// <returns>An <see cref="object"/> that represents the converted value.</returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="destinationType"/> parameter is null.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Preconditions.CheckNotNull(destinationType, nameof(destinationType));
            if (destinationType == typeof(string) && value is DateTimeZone typedValue)
            {
                return typedValue.Id;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}