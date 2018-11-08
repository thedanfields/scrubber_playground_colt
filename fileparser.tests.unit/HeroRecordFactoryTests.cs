using fileparser.tests.unit.Properties;
using scrubber_playground.file_parsers;
using scrubber_playground.file_parsers.hero;
using scrubber_playground.file_parsers.hero.record_types;
using System.Linq;
using Xunit;

namespace fileparser.tests.unit
{
    public class HeroRecordFactoryTests
    {
        private string FormatStringAsFileContents(string contents) =>
            string.Join(System.Environment.NewLine, contents.Split(System.Environment.NewLine).Select(line => line.Trim()));

        [Fact]
        public void ParseGoodFile()
        {
            var contents = @"NBologna Bill
                             PPungent Meat Sweats
                             SBill      BillStein";

            var heroData = FormatStringAsFileContents(contents);

            var parser = new HeroFileParser(heroData);
            var heroRecords = parser.Parse();

            // make sure we've got one of each record type
            // and the property values are what we are expecting:

            var nameRecord = heroRecords.OfType<NameRecord>().Single();
            Assert.Equal("Bologna Bill", nameRecord.HeroName);

            var powerRecord = heroRecords.OfType<PowerRecord>().Single();
            Assert.Equal("Pungent Meat Sweats", powerRecord.Power);

            var secretIdentityRecord = heroRecords.OfType<SecretIdentityRecord>().Single();
            Assert.Equal("Bill", secretIdentityRecord.FirstName);
            Assert.Equal("BillStein", secretIdentityRecord.LastName);
        }

        [Fact]
        public void BadRecordTypeThrowsAnException()
        {
            var contents = @"NBologna Bill
                             PPungent Meat Sweats
                             SBill      BillStein
                             ~"; // <-- bad indicator here

            var badData = FormatStringAsFileContents(contents);

            var parser = new HeroFileParser(badData);
            Assert.Throws<UnknownRecordTypeException>(() => parser.Parse());
        }
    }
}
