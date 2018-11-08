using scrubber_playground.file_parsers.hero.record_types;

using System.Collections.Generic;
using System.IO;

namespace scrubber_playground.file_parsers.hero
{
    public class HeroFileParser : IFileParser
    {
        private readonly string _fileData;

        public HeroFileParser(string fileData) => _fileData = fileData;

        public IEnumerable<IFileRecord> Parse()
        {
            var records = new List<IFileRecord>();
            var recordFactory = new HeroRecordFactory();

            using (StringReader reader = new StringReader(_fileData))
            {
                var line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                        records.Add(recordFactory.Build(line));


                } while (line != null);
            }

            return records;
        }
    }
}
