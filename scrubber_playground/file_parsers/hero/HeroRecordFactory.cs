using System;
using System.Collections.Generic;
using System.Linq;

namespace scrubber_playground.file_parsers.hero.record_types
{
    public class HeroRecordFactory : IRecordFactory
    {
        private Dictionary<Type, IEnumerable<System.Reflection.PropertyInfo>> PropertyInfoByTypeDict = new Dictionary<Type, IEnumerable<System.Reflection.PropertyInfo>>();

        public IFileRecord Build(string fromData)
        {
            IFileRecord record = new NameRecord { HeroName = "Old Dad Man" };

            if(string.IsNullOrEmpty(fromData))
            {
                return null;
            }

            switch (char.ToUpper(fromData.First()))
            {
                case 'N':
                    return Parse<NameRecord>(fromData);
                case 'P':
                    return Parse<PowerRecord>(fromData);
                case 'S':
                    return Parse<SecretIdentityRecord>(fromData);
                default:
                    throw new UnknownRecordTypeException(fromData);
            }
        }

        private T Parse<T>(string someData)
            where T: new()
        {
            // BONUS TODO: cache these reflection operations to prevent re-execution on subsequent file parses
            var toType = typeof(T);

            IEnumerable<System.Reflection.PropertyInfo> fileMappedProperties;
            if(PropertyInfoByTypeDict.ContainsKey(toType))
            {
                fileMappedProperties = PropertyInfoByTypeDict[toType];
            }
            else
            {
                fileMappedProperties = toType.GetProperties().Where(property => Attribute.IsDefined(property, typeof(FileMapAttribute)));
                PropertyInfoByTypeDict.Add(toType, fileMappedProperties);
            }

            var record = new T();
            foreach(var fileMappedProperty in fileMappedProperties)
            {
                var attribute = fileMappedProperty.GetCustomAttributes(typeof(FileMapAttribute), false).Single();
                var fileMapAttribute = attribute as FileMapAttribute;

                var startIndex = fileMapAttribute.StartIndex;
                var length = Math.Min(fileMapAttribute.Length, someData.Length - fileMapAttribute.StartIndex);

                var value = someData.Substring(startIndex, length);

                fileMappedProperty.SetValue(record, value.Trim());
            }

            return record;
        }
    }
}
