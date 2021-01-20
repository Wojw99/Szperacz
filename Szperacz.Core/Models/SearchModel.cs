using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core.Models
{
    public class SearchModel
    {
        public string Phrase { get; set; }
        public string FolderPath { get; set; }

        public SearchModel(string phrase, string folderPath)
        {
            Phrase = phrase;
            FolderPath = folderPath;
        }

        public override string ToString()
        {
            return $"{Phrase}, {FolderPath}";
        }

        public bool Equals(SearchModel s)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(s, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, s))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != s.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Phrase == s.Phrase && FolderPath == s.FolderPath);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SearchModel);
        }

        public override int GetHashCode()
        {
            return Phrase.GetHashCode() + 0x00010000 + FolderPath.GetHashCode();
        }
    }
}
