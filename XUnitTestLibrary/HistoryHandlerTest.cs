using System;
using System.Collections.Generic;
using System.Text;
using Szperacz.Core.Models;
using Xunit;

namespace XUnitTestLibrary
{
    public class HistoryHandlerTest
    {
        [Fact]
        public void HistorySerializeTest1()
        {
            var expected = new List<SearchModel>()
            {
                new SearchModel("Żołądź", @"D:\Pulpit/TEXT.txt"),
                new SearchModel("Kasztan", @"D:\Pulpit/TEXT1.pdf"),
                new SearchModel("Orzech laskowy", @"D:\Pulpit/tekst2.docx")
            };

            HistoryHandler.SerializeHistoryList(expected);
            var actual = HistoryHandler.DeserializeHistoryList();

            Assert.Equal(expected, actual);
        }
    }
}
