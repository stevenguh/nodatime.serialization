using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// Normalizing ISO type converter for periods. Use this when you want compatibility with systems expecting
    /// ISO durations (~= Noda Time periods). However, note that Noda Time can have negative periods. Note that
    /// this converter losees information - after serialization and deserialization, "90 minutes" will become "an hour and 30 minutes".
    /// </summary>
    public class NormalizingIsoPeriodTypeConverter : NodaRegularPatternTypeConverterBase<Period>
    {
        /// <summary>
        /// Creates an <see cref="NormalizingIsoPeriodTypeConverter"/>.
        /// </summary>
        public NormalizingIsoPeriodTypeConverter() : base(PeriodPattern.NormalizingIso)
        {
        }
    }
}