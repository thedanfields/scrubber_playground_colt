using System;
using System.Linq;

namespace scrubber_playground.file_parsers.hero.record_types
{
    public class HeroRecordFactory : IRecordFactory
    {   
        public IFileRecord Build(string fromData)
        {
            IFileRecord record = new NameRecord { HeroName = "Old Dad Man" };

            // TODO: Add code here to complete this function.  
            //       You should make use of the Parse method below.

            return record;
        }

        private T Parse<T>(string someData)
            where T: new()
        {
            // BONUS TODO: cache these reflection operations to prevent re-execution on subsequent file parses
            var toType = typeof(T);
            var fileMappedProperites = toType.GetProperties()
                                        .Where(propery => Attribute.IsDefined(propery, typeof(FileMapAttribute)));

            var record = new T();
            foreach(var fileMappedPropery in fileMappedProperites)
            {
                var attribute = fileMappedPropery.GetCustomAttributes(typeof(FileMapAttribute), false).Single();
                var fileMapAttribute = attribute as FileMapAttribute;

                var startIndex = fileMapAttribute.StartIndex;
                var length = Math.Min(fileMapAttribute.Length, someData.Length - fileMapAttribute.StartIndex);

                var value = someData.Substring(startIndex, length);

                fileMappedPropery.SetValue(record, value.Trim());
            }

            return record;
        }
    }
}
