using System;

namespace scrubber_playground.file_parsers
{
    public class UnknownRecordTypeException : Exception
    {
        public UnknownRecordTypeException(string recordType) 
            : base($"Unknown record type: {recordType}") { }
    }
}
