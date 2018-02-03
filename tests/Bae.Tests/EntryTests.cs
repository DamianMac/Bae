using System.Linq;
using Xunit;

namespace Bae.Tests
{
    public class EntryTests
    {

        [Fact]
        public void TokenisationRemovesDuplicates()
        {
            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            Assert.Equal(new[] { "the", "quick", "brown", "fox", "jumps", "over", "lazy", "dog" }, entry.Tokens.ToArray());
        }

        [Fact]
        public void RemovesSpecialCharacters()
        {
            var entry = new Entry("hey #>*there</ boo!");
            Assert.Equal(new[] { "hey", "there", "boo" }, entry.Tokens);
        }


    }
}