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

        public override bool Equals(object obj)
        {
            var other = obj as SearchModel;
            if(other.FolderPath == this.FolderPath && other.Phrase == this.Phrase)
            {
                return true;
            }
            return false;
        }
    }
}
