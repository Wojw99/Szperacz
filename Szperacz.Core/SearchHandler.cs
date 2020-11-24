using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core
{
    public static class SearchHandler
    {
        public static List<String> SearchWord(string word, string path)
        {
            var list = new List<String>();

            // In this place a function will be calling the python script, waiting for answer and returning a result
            // For it returns example paths
            list.Add("D:\\Users\\Pulpit\\tekst.txt");
            list.Add("D:\\Users\\Pulpit\\guf.pdf");
            list.Add("D:\\Users\\Pulpit\\tekst2.doc");

            return list;
        }
    }
}
