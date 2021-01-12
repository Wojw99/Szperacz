using System;
using System.Collections.Generic;
using Szperacz.Core;
using Szperacz.Core.Models;
using Xunit;

namespace XUnitTestLibrary
{
    public class SearchHandlerTest
    {
        [Fact]
        public void PdfSearchTest1()
        {
            // Arrange
            var word = "zielony"; // This word is only pressent in a pdf file
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\src";
            var createGraph = false;
            var letterSizeNotMeans = true;
            var autoSelection = false;
            var cpuThreadNumber = 4;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>() 
            { 
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\src\PDF.pdf", 2, new List<String>()) 
            };

            // Act
            var actual = SearchHandler.GetPaths();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
