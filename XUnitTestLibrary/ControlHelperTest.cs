using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Szperacz.Core.ViewModels;
using Xunit;

namespace XUnitTestLibrary
{
    public class ControlHelperTest
    {
        /*
         * WARNING: close all txt/pdf/docx process before starting the test!!! 
         */

        [Fact]
        public async Task OpenPdfTest1()
        {
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\PDF.pdf";
            await ControlHelper.OpenTxtFile(path);
            var list = Process.GetProcessesByName("AcroRd32");
            Assert.True(list.Length > 0);
        }

        [Fact]
        public async Task OpenDocxTest1()
        {
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\DOCX.docx";
            await ControlHelper.OpenTxtFile(path);
            var list = Process.GetProcessesByName("soffice");
            Assert.True(list.Length > 0);
        }

        [Fact]
        public async Task OpenTxtTest1()
        {
            var path = @"D:\Development\GitHub\Szperacz\XUnitTestLibrary\lokacja\TEXT.txt";
            await ControlHelper.OpenTxtFile(path);
            var list = Process.GetProcessesByName("notepad");
            Assert.True(list.Length > 0);
        }
    }
}
