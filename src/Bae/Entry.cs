using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bae
{
    public class Entry
    {
        private string text;

        public IEnumerable<string> Tokens { get; private set; }

        public Entry(string text)
        {
            this.text = CleanInput(text);
            Tokens = Tokenise();
        }

        private IEnumerable<string> Tokenise()
        {
            return text.Split(' ')
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(t => t.ToLowerInvariant()).Distinct();
        }

        private static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn, @"[^\w\'@-]", " ");
        }
    }
}