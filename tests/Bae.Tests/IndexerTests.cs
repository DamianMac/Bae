using Xunit;

namespace Bae.Tests
{
    public class IndexerTests
    {
        [Fact]
        public void IndexingAnEntry_IncrementsDocumentCountForIndex()
        {
            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            
            var indexer = new DocumentIndex();
            indexer.IndexEntry(entry);

            Assert.Equal(1, indexer.DocumentCount);

        }

        [Fact]
        public void IndexingAnEntry_RecordsUniqueWords()
        {
            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            var indexer = new DocumentIndex();
            indexer.IndexEntry(entry);

            Assert.Equal(1, indexer["quick"]);            
        }

        [Fact]
        public void IndexingASecondEntry_RecordsAdditionalWordAppearences()
        {
            var indexer = new DocumentIndex();

            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            var entry2 = new Entry("Come here quick");
            indexer.IndexEntry(entry);
            indexer.IndexEntry(entry2);

            Assert.Equal(2, indexer["quick"]);            
        }

        [Fact]
        public void IndexerCanReturnCountOfWords()
        {
            var indexer = new DocumentIndex();
            indexer.IndexEntry(new Entry("quick brown fox"));
            indexer.IndexEntry(new Entry("come here quick"));

            Assert.Equal(2, indexer.WordCount("quick"));
        }

        [Fact]
        public void IndexerReturnsZeroForWordsItHasntSeen()
        {
            var indexer = new DocumentIndex();
            indexer.IndexEntry(new Entry("come here quick"));

            Assert.Equal(0, indexer.WordCount("foo"));
        }
        
    }
}