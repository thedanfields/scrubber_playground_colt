namespace scrubber_playground.file_parsers.hero.record_types
{
    public class NameRecord : IFileRecord
    {
        [FileMap(1, 20)]
        public string HeroName { get; set; }
    }
}
