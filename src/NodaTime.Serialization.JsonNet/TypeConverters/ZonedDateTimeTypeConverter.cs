using NodaTime.Text;
using System.ComponentModel;
using System.Globalization;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for zoned date/times, using the zone provider
    /// in <see cref="DateTimeZoneProviderAttribute"/>.
    /// </summary>
    public class ZonedDateTimeTypeConverter : NodaPatternTypeConverterBase<ZonedDateTime>
    {
        private ZonedDateTimePattern pattern;

        /// <summary>
        /// Gets an <see cref="IPattern{T}"/> for type conversion.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <returns>A <see cref="IPattern{T}"/>.</returns>
        protected override IPattern<ZonedDateTime> GetPattern(ITypeDescriptorContext context, CultureInfo culture)
        {
            IDateTimeZoneProvider provider = GetZoneProvider(context);
            if (pattern == null || !this.pattern.ZoneProvider.Equals(provider))
            {
                this.pattern = ZonedDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFFo<G> z", provider);
            }

            return this.pattern;
        }

        /// <summary>
        /// Gets the <see cref="CalendarSystem"/> of <see cref="ZonedDateTime"/>.
        /// for validation.
        /// </summary>
        /// <param name="value">The value used to get <see cref="CalendarSystem"/>.</param>
        /// <returns>The <see cref="CalendarSystem"/> for <see cref="ZonedDateTime"/>.</returns>
        protected override CalendarSystem GetCalendar(ZonedDateTime value)
        {
            return value.Calendar;
        }
    }
}