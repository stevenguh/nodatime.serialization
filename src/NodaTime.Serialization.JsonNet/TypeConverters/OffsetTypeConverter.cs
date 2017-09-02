using NodaTime.Text;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// Converter for offsets.
    /// </summary>
    public class OffsetTypeConverter : NodaRegularPatternTypeConverterBase<Offset>
    {
        /// <summary>
        /// Creates an <see cref="OffsetTypeConverter"/>.
        /// </summary>
        public OffsetTypeConverter() : base(OffsetPattern.GeneralInvariant)
        {
        }
    }
}