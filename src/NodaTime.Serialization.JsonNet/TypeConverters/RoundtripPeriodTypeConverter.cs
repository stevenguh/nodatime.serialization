using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A round-tripping type converter for periods. Use this when you really want to preserve information,
    /// and don't need interoperability with systems expecting ISO.
    /// </summary>
    public class RoundtripPeriodTypeConverter : NodaRegularPatternTypeConverterBase<Period>
    {
        /// <summary>
        /// Creates a <see cref="RoundtripPeriodTypeConverter"/>.
        /// </summary>
        public RoundtripPeriodTypeConverter() : base(PeriodPattern.Roundtrip)
        {
        }
    }
}