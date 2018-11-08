namespace scrubber_playground.file_parsers.hero.record_types
{
    public class PowerRecord : IFileRecord
    {
        [FileMap(1, 30)]
        public string Power { get; set; }
    }
}
