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

        public bool Equals(PathModel p)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Path == p.Path && PhraseAmount == p.PhraseAmount);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PathModel);
        }

        public override int GetHashCode()
        {
            return PhraseAmount * 0x00010000 + Path.GetHashCode();
        }
    }
}
