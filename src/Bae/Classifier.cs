using System;
using System.Collections.Generic;
using System.Linq;

namespace Bae
{
    public class Classifier
    {
        public Classifier()
        {
            DocumentCount = 0;
            Indexes = new Dictionary<string, DocumentIndex>();
        }

        public int DocumentCount { get; private set; }
        public Dictionary<string, DocumentIndex> Indexes { get; private set; }

        public void Train(string label, Entry entry)
        {
            DocumentCount++;

            if ( ! Indexes.ContainsKey(label))
            {
                Indexes.Add(label, new DocumentIndex());
            }

            Indexes[label].IndexEntry(entry);
        }

        public IEnumerable<GuessResult> Guess(Entry entry)
        {
            var scores = new List<GuessResult>();

            var documentsInLabel = new Dictionary<string, int>();
            var documentsNotInLabel = new Dictionary<string, int>();

            foreach(var label in Indexes.Keys)
            {

                var index = Indexes[label];
                                
                documentsInLabel[label] = Indexes[label].DocumentCount;
                documentsNotInLabel[label] = DocumentCount - documentsInLabel[label];
            
                double logSum = 0;

                foreach(var word in entry.Tokens)
                {
                    var totalWordCount = FindTotalOccurrences(word);
                    if ( totalWordCount == 0)
                        continue;

                    var wordProbability = (double)index.WordCount(word) / (double)index.DocumentCount;
                    var wordInverseProbability = (double)FindOccurrencesInOtherIndexes(word, label) / (double)documentsNotInLabel[label];
                    var wordicity = wordProbability / (wordProbability + wordInverseProbability);

                    wordicity = ((1 * 0.5) + (totalWordCount * wordicity)) / (double)(1 + totalWordCount);
                    if (wordicity == 0)
                        wordicity = 0.01;
                    else if (wordicity == 1)
                        wordicity = 0.99;

                    logSum += (Math.Log(1 - wordicity) - Math.Log(wordicity));
                }

                scores.Add(new GuessResult(label, 1 / ( 1 + Math.Exp(logSum) )));
            
            }

            return scores;
        }

        private int FindOccurrencesInOtherIndexes(string word, string label)
        {
            int count = 0;
            foreach(var key in Indexes.Keys)
            {
                if ( key == label)
                    continue;
                count += Indexes[key].WordCount(word);
            }
            return count;
        }
        private int FindTotalOccurrences(string word)
        {
            int count = 0;
            foreach(var index in Indexes.Values)
            {
                count += index.WordCount(word);
            }
            return count;
        }
    }
}
