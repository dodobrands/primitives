using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Dodo.Primitives
{
    public class UuidTypeConverter : TypeConverter
    {
        private static readonly ConstructorInfo UuidStringCtor = typeof(Uuid)
            .GetTypeInfo()
            .DeclaredConstructors
            .Single(x =>
            {
                ParameterInfo[] parameters = x.GetParameters();
                if (parameters.Length == 1)
                {
                    ParameterInfo parameter = parameters[0];
                    if (parameter.ParameterType == typeof(string))
                    {
                        return true;
                    }
                }

                return false;
            });

        public override bool CanConvertFrom(
            ITypeDescriptorContext context,
            Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(
            ITypeDescriptorContext context,
            Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            if (value is string text)
            {
                return new Uuid(text);
            }

            if (value is InstanceDescriptor descriptor)
            {
                if (descriptor.MemberInfo == UuidStringCtor)
                {
                    return descriptor.Invoke();
                }

                throw GetConvertFromException(value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value is Uuid uuidValue)
            {
                if (destinationType == typeof(string))
                {
                    return uuidValue.ToString("N");
                }

                if (destinationType == typeof(InstanceDescriptor))
                {
                    return new InstanceDescriptor(UuidStringCtor, new object[] {uuidValue.ToString("N")});
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
