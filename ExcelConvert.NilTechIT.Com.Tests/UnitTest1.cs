//namespace ExcelConvert.NilTechIT.Com.Tests
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void Test1()
//        {
//            Assert.Pass();
//        }
//    }
//}


using ExcelConvert.NilTechIT.Com.Excel;
using NUnit.Framework;
using System.Collections.Generic;

namespace ExcelConvert.NilTechIT.Com.Tests
{
    [TestFixture]
    public class Tests
    {
        private ExcelConverter _excelConverter;

        [SetUp]
        public void SetUp()
        {
            _excelConverter = new ExcelConverter();
        }

        [Test]
        public void ConvertListToExcel_WithValidData_ReturnsExpectedOutput()
        {
            // Arrange
            var dataList = new List<TestData>
            {
                new TestData { Id = 1, Name = "Alice", Age = 30 },
                new TestData { Id = 2, Name = "Bob", Age = 25 }
            };

            // Act
            string csvOutput = _excelConverter.ConvertListToExcel(dataList);

            // Assert
            Assert.IsNotNull(csvOutput, "CSV output should not be null.");

            var outputLines = csvOutput.Split('\n');
            Assert.AreEqual(3, outputLines.Length, "Output should have three lines: header and two data lines.");

            var headerLine = outputLines[0].Trim();
            var expectedHeader = "Id,Name,Age,";
            Assert.AreEqual(expectedHeader, headerLine, "Header line does not match the expected format.");

            var firstDataLine = outputLines[1].Trim();
            Assert.AreEqual("1,Alice,30,", firstDataLine, "First data line does not match the expected format.");

            var secondDataLine = outputLines[2].Trim();
            Assert.AreEqual("2,Bob,25,", secondDataLine, "Second data line does not match the expected format.");
        }

        [Test]
        public void ConvertListToExcel_WithEmptyData_ReturnsOnlyHeader()
        {
            // Arrange
            var dataList = new List<TestData>();

            // Act
            string csvOutput = _excelConverter.ConvertListToExcel(dataList);

            // Assert
            Assert.IsNotNull(csvOutput, "CSV output should not be null.");

            var outputLines = csvOutput.Split('\n');
            Assert.AreEqual(2, outputLines.Length, "Output should have only one line: header and one empty line.");

            var headerLine = outputLines[0].Trim();
            var expectedHeader = "Id,Name,Age,";
            Assert.AreEqual(expectedHeader, headerLine, "Header line does not match the expected format.");
        }

        // Additional tests can be added to cover more scenarios

        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
