using NodaTime.Text;
using System;
using System.ComponentModel;
using System.Globalization;

namespace NodaTime.Serialization.JsonNet.TypeConverters
{
    /// <summary>
    /// A base class for creating the type converter for NodaTime types.
    /// </summary>
    public abstract class NodaTypeConverterBase : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the
        /// type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
        /// <returns><c>ture</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type,
        /// using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to</param>
        /// <returns><c>ture</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Gets an <see cref="IDateTimeZoneProvider"/> from <see cref="DateTimeZoneProviderAttribute"/>
        /// that's annotated on the type.
        /// </summary>
        /// <typeparam name="T">The type that has <see cref="DateTimeZoneProviderAttribute"/> annotated.</typeparam>
        /// <returns>
        /// An <see cref="IDateTimeZoneProvider"/> from <see cref="DateTimeZoneProviderAttribute"/> that annotated on the same type.
        /// If the attribute is not found, then <see cref="DateTimeZoneProviders.Tzdb"/> will return.
        /// </returns>
        protected IDateTimeZoneProvider GetZoneProvider<T>()
        {
            DateTimeZoneProviderAttribute attr = (DateTimeZoneProviderAttribute)
                TypeDescriptor.GetAttributes(typeof(T))[typeof(DateTimeZoneProviderAttribute)];
            return attr?.ZoneProvider ?? DateTimeZoneProviders.Tzdb;
        }
    }

    /// <summary>
    /// A base class for creating the type converter for NodaTime types
    /// with <see cref="IPattern{T}"/>.
    /// </summary>
    /// <typeparam name="T">The NodaTime type.</typeparam>
    public abstract class NodaPatternTypeConverterBase<T> : NodaTypeConverterBase
    {
        /// <summary>
        /// Converts the given object to the type of this converter, using the specified
        /// context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="object"/> to convert.</param>
        /// <returns>An <see cref="object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string strValue)
            {
                IPattern<T> pattern = GetPattern(context, culture);
                ParseResult<T> result = pattern.Parse(strValue);
                if (result.Success)
                {
                    return result.Value;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context
        /// and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="object"/> to object.</param>
        /// <param name="destinationType">The type to convert the value parameter to.</param>
        /// <returns>An <see cref="object"/> that represents the converted value.</returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="destinationType"/> parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="CalendarSystem"/> of the <paramref name="value"/> is not <see cref="CalendarSystem.Iso"/>.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Preconditions.CheckNotNull(destinationType, nameof(destinationType));
            if (destinationType == typeof(string) && value is T typedValue)
            {
                ValidateIsoCalendar(GetCalendar(typedValue));
                IPattern<T> pattern = GetPattern(context, culture);
                return pattern.Format(typedValue);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Gets the <see cref="CalendarSystem"/> of <paramref name="value"/>
        /// for validation.
        /// </summary>
        /// <param name="value">The value used to get <see cref="CalendarSystem"/>.</param>
        /// <returns><c>null</c> to indicate there isn't <see cref="CalendarSystem"/>.</returns>
        protected virtual CalendarSystem GetCalendar(T value)
        {
            return null;
        }

        /// <summary>
        /// Gets an <see cref="IPattern{T}"/> for type conversion.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <returns>A <see cref="IPattern{T}"/>.</returns>
        protected abstract IPattern<T> GetPattern(ITypeDescriptorContext context, CultureInfo culture);

        private void ValidateIsoCalendar(CalendarSystem calendar)
        {
            if (calendar != null)
            {
                // We rely on CalendarSystem.Iso being a singleton here.
                Preconditions.CheckArgument(calendar == CalendarSystem.Iso,
                    "Values of type {0} must (currently) use the ISO calendar in order to be serialized.",
                    typeof(T).Name);
            }
        }
    }

    /// <summary>
    /// A base class for creating type converter
    /// with regular <see cref="IPattern{T}"/>, which doesn't change base on <see cref="ITypeDescriptorContext"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NodaRegularPatternTypeConverterBase<T> : NodaPatternTypeConverterBase<T>
    {
        private IPattern<T> pattern;

        /// <summary>
        /// Creates a <see cref="NodaRegularPatternTypeConverterBase{T}"/>.
        /// </summary>
        /// <param name="pattern">The <see cref="IPattern{T}"/> use for the converter.</param>
        protected NodaRegularPatternTypeConverterBase(IPattern<T> pattern)
        {
            this.pattern = pattern;
        }

        /// <summary>
        /// Gets an <see cref="IPattern{T}"/> for type conversion.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        /// <returns>A <see cref="IPattern{T}"/>.</returns>
        protected override IPattern<T> GetPattern(ITypeDescriptorContext context, CultureInfo culture)
        {
            return this.pattern;
        }
    }
}