using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for local dates, using the ISO-8601 date pattern.
    /// </summary>
    public class LocalDateTypeConverter : NodaRegularPatternTypeConverterBase<LocalDate>
    {
        /// <summary>
        /// Creates a <see cref="LocalDateTypeConverter"/>.
        /// </summary>
        public LocalDateTypeConverter() : base(LocalDatePattern.Iso)
        {
        }

        /// <summary>
        /// Gets the <see cref="CalendarSystem"/> of <see cref="LocalDate"/>.
        /// for validation.
        /// </summary>
        /// <param name="value">The value used to get <see cref="CalendarSystem"/>.</param>
        /// <returns>The <see cref="CalendarSystem"/> for <see cref="LocalDate"/>.</returns>
        protected override CalendarSystem GetCalendar(LocalDate value)
        {
            return value.Calendar;
        }
    }
}