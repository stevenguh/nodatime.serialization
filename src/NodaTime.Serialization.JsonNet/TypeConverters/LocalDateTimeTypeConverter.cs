using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for local dates and times, using the ISO-8601 date/time pattern, extended as required to accommodate milliseconds and ticks.
    /// No time zone designator is applied.
    /// </summary>
    public class LocalDateTimeTypeConverter : NodaRegularPatternTypeConverterBase<LocalDateTime>
    {
        /// <summary>
        /// Creates a <see cref="LocalDateTimeTypeConverter"/>.
        /// </summary>
        public LocalDateTimeTypeConverter()
            : base(LocalDateTimePattern.ExtendedIso)
        {
        }
    }
}