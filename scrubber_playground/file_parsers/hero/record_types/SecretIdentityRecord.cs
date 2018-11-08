namespace scrubber_playground.file_parsers.hero.record_types
{
    public class SecretIdentityRecord : IFileRecord
    {
        [FileMap(1, 10)]
        public string FirstName { get; set; }
        [FileMap(11, 10)]
        public string LastName { get; set; }
    }
}
