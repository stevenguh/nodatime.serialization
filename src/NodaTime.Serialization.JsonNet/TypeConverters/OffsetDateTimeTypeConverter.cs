using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for offset date/times.
    /// </summary>
    public class OffsetDateTimeTypeConverter : NodaRegularPatternTypeConverterBase<OffsetDateTime>
    {
        /// <summary>
        /// Creates an <see cref="OffsetDateTimeTypeConverter"/>.
        /// </summary>
        public OffsetDateTimeTypeConverter() : base(OffsetDateTimePattern.Rfc3339)
        {
        }

        /// <summary>
        /// Gets the <see cref="CalendarSystem"/> of <see cref="OffsetDateTime"/>.
        /// for validation.
        /// </summary>
        /// <param name="value">The value used to get <see cref="CalendarSystem"/>.</param>
        /// <returns>The <see cref="CalendarSystem"/> for <see cref="OffsetDateTime"/>.</returns>
        protected override CalendarSystem GetCalendar(OffsetDateTime value)
        {
            return value.Calendar;
        }
    }
}