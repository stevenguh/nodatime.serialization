using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A type converter for durations.
    /// </summary>
    public class DurationTypeConverter : NodaRegularPatternTypeConverterBase<Duration>
    {
        /// <summary>
        /// Creates a <see cref="DurationTypeConverter"/>.
        /// </summary>
        public DurationTypeConverter()
            : base(DurationPattern.CreateWithInvariantCulture("-H:mm:ss.FFFFFFFFF"))
        {
        }
    }
}