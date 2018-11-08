using System;

namespace scrubber_playground.file_parsers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class FileMapAttribute : Attribute
    {
        public int StartIndex { get; set; }
        public int Length { get; set; }

        public FileMapAttribute(int startIndex, int length) => (StartIndex, Length) = (startIndex, length);        
    }
}
