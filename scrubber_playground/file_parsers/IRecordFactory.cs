namespace scrubber_playground.file_parsers
{
    public interface IRecordFactory
    {
        IFileRecord Build(string fromData);
    }
}
