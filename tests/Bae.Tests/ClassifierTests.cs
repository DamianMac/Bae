using System;
using System.Linq;
using Xunit;

namespace Bae.Tests
{
    public class ClassifierTests
    {
        [Fact]
        public void AddingAnEntry_IncreasesDocumentCount()
        {

            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            var classifier = new Classifier();
            classifier.Train("blah", entry);
            Assert.Equal(1, classifier.DocumentCount);

        }

        [Fact]
        public void AddingAnEntry_BuildsAnIndex()
        {
            var entry = new Entry("The quick brown fox jumps over the lazy dog");
            var classifier = new Classifier();
            classifier.Train("blah", entry);

            Assert.Contains("blah", classifier.Indexes.Keys);

        }


        [Fact]
        public void ClassifyingADocuent_ReturnsScores()
        {
            var classifier = new Classifier();
            classifier.Train("sleepy", new Entry("snooze nap sleep kip tired heavy sleepy drowsy"));
            classifier.Train("awake", new Entry("bright fresh awake perky alert rested"));
            
            var results = classifier.Guess(new Entry("I'm so tired I need to sleep"));
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void ClassifyingADocuent_GuessesRight()
        {
            var classifier = new Classifier();
            classifier.Train("sleepy", new Entry("snooze nap sleep kip tired heavy sleepy drowsy"));
            classifier.Train("awake", new Entry("bright fresh awake perky alert rested"));
            
            var results = classifier.Guess(new Entry("I'm so tired I need to sleep"));
            
            Assert.Equal("sleepy", results.OrderByDescending(r => r.Score).First().Label);
        }

    }
}
