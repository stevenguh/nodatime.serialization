using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for instants, using the ISO-8601 date/time pattern, extended as required to accommodate milliseconds and ticks, and
    /// specifying 'Z' at the end to show it's effectively in UTC.
    /// </summary>
    public class InstantTypeConverter : NodaRegularPatternTypeConverterBase<Instant>
    {
        /// <summary>
        /// Creates an <see cref="InstantTypeConverter"/>.
        /// </summary>
        public InstantTypeConverter() : base(InstantPattern.ExtendedIso)
        {
        }
    }
}