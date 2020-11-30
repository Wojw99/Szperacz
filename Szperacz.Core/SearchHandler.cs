using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core
{
    public static class SearchHandler
    {
        public static List<String> SearchWord(string word, string path, bool createChart, bool letterSizeMeans, bool automaticSelection)
        {
            var list = new List<String>();

            // In this place a function will be calling the python script, waiting for answer and returning a result
            // For now it returns example paths
            list.Add("D:\\Users\\Pulpit\\tekst.txt");
            list.Add("D:\\Users\\Pulpit\\guf.pdf");
            list.Add("D:\\Users\\Pulpit\\tekst2.doc");

            return list;
        }
    }
}
