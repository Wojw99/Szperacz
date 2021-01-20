using System;
using System.Collections.Generic;
using Szperacz.Core;
using Szperacz.Core.Models;
using Xunit;
using System.Linq;

namespace XUnitTestLibrary
{
    public class SearchHandlerTest
    {
        private string ConcatListOfPaths(List<PathModel> list)
        {
            string str = "";

            foreach(var x in list)
            {
                str += x.Path;
            }

            return str;
        }

        /// <summary>
        /// All searching test
        /// </summary>
        [Fact]
        public void AllSearchTest1()
        {
            // Arrange
            var word = "koliber hawañski"; // This word is pressent in all of files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = true;
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\DOCX.docx", 3, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF.pdf", 3, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF-B.pdf", 3, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\TEXT.txt", 3, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Letter size searching test
        /// </summary>
        [Fact]
        public void LetterSizeMeansTest1()
        {
            // Arrange
            var word = "Hawañski"; // This word is pressent in all of files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = false; // Letter size means
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\DOCX.docx", 2, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF.pdf", 2, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF-B.pdf", 2, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\TEXT.txt", 2, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Letter size searching test
        /// </summary>
        [Fact]
        public void LetterSizeMeansTest2()
        {
            // Arrange
            var word = "koliber"; // This word is pressent in all of files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = false; // Letter size means
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\DOCX.docx", 1, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF.pdf", 1, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF-B.pdf", 1, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\TEXT.txt", 1, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Pdf searching test
        /// </summary>
        [Fact]
        public void PdfSearchTest1()
        {
            // Arrange
            var word = "dziêcio³ zielony"; // This word is pressent in pdf files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = true;
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF.pdf", 2, new List<String>()),
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF-B.pdf", 2, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Docx searching test
        /// </summary>
        [Fact]
        public void DocxSearchTest1()
        {
            // Arrange
            var word = "sokó³ wêdrowny"; // This word is pressent in docx files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = true;
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\DOCX.docx", 2, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Txt searching test
        /// </summary>
        [Fact]
        public void TxtSearchTest1()
        {
            // Arrange
            var word = "sowa jarzêbata"; // This word is pressent in docx files
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja";
            var createGraph = false;
            var letterSizeNotMeans = true;
            var autoSelection = false;
            var cpuThreadNumber = 1;

            SearchHandler.SearchWord(word, path, createGraph, letterSizeNotMeans, autoSelection, cpuThreadNumber);
            var expected = new List<PathModel>()
            {
                new PathModel(@"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\TEXT.txt", 2, new List<String>())
            }.OrderBy(p => p.Path).ToList();

            // Act
            var actual = SearchHandler.GetPaths().OrderBy(p => p.Path).ToList();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
