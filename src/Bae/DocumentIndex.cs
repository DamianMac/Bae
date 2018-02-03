using System;
using System.Collections.Generic;

namespace Bae
{
    public class DocumentIndex : Dictionary<string, int>
    {

        public int DocumentCount { get; private set; }

        public void IndexEntry(Entry entry)
        {
            DocumentCount++;

            foreach(var word in entry.Tokens)
            {
                if ( !this.ContainsKey(word))
                    this.Add(word, 1);
                else
                    this[word]++;
            }
        }

        public int WordCount(string word)
        {
            if (this.ContainsKey(word))
                return this[word];
            return 0;
        }
    }
}