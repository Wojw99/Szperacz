using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core.Models
{
    public class PathModel
    {
        public string Path { get; set; }
        public int PhraseAmount { get; set; }
        public string MatchingPhrases { get; set; }

        public PathModel(string path, int phraseAmount, string matchingPhrases)
        {
            Path = path;
            PhraseAmount = phraseAmount;
            MatchingPhrases = matchingPhrases;
        }

        public override string ToString()
        {
            return $"{Path} {PhraseAmount} {MatchingPhrases}";
        }
    }
}
