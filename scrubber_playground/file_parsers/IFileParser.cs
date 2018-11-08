using System.Collections.Generic;

namespace scrubber_playground.file_parsers
{
    public interface IFileParser
    {
        IEnumerable<IFileRecord> Parse();
    }
}
