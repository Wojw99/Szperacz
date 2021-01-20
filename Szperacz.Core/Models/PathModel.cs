using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core.Models
{
    public class PathModel
    {
        public string Path { get; set; }
        public int PhraseAmount { get; set; }
        public List<String> MatchingPhrases { get; set; }

        public PathModel(string path, int phraseAmount, List<String> matchingPhrases)
        {
            Path = path;
            PhraseAmount = phraseAmount;
            MatchingPhrases = matchingPhrases;
        }

        public override string ToString()
        {
            return $"{Path} {PhraseAmount} {MatchingPhrases}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as PathModel;

            if(this.Path == other.Path && this.PhraseAmount == other.PhraseAmount)
            {
                return true;
            }

            return false;
        }
    }
}
