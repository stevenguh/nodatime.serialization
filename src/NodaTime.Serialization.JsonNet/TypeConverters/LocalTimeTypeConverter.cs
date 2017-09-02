using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for local times, using the ISO-8601 time pattern, extended as required to accommodate milliseconds and ticks.
    /// </summary>
    public class LocalTimeTypeConverter : NodaRegularPatternTypeConverterBase<LocalTime>
    {
        /// <summary>
        /// Creates a <see cref="LocalTimeTypeConverter"/>.
        /// </summary>
        public LocalTimeTypeConverter() : base(LocalTimePattern.ExtendedIso)
        {
        }
    }
}