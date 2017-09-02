using System;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A attribute use in conjunction with NodaTime's TypeConverter.
    /// </summary>
    public sealed class DateTimeZoneProviderAttribute : Attribute
    {
        /// <summary>
        /// Creates an instance of <see cref="DateTimeZoneProviderAttribute"/>
        /// with the <see cref="IDateTimeZoneProvider"/>.
        /// </summary>
        /// <param name="provider">The zone provider this attribute stores.</param>
        public DateTimeZoneProviderAttribute(IDateTimeZoneProvider provider)
        {
            ZoneProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Creates an instance of <see cref="DateTimeZoneProviderAttribute"/>
        /// with the <see cref="DateTimeZoneProviders.Tzdb"/>.
        /// </summary>
        public DateTimeZoneProviderAttribute()
        {
            ZoneProvider = DateTimeZoneProviders.Tzdb;
        }

        /// <summary>
        /// Gets a <see cref="IDateTimeZoneProvider"/> stored in this attribute.
        /// </summary>
        public IDateTimeZoneProvider ZoneProvider { get; }
    }
}