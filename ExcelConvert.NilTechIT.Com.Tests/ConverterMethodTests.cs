
using ExcelConvert.NilTechIT.Com.Excel;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelConvert.NilTechIT.Com.Tests
{
    [TestFixture]
    public class ConverterMethodTests
    {
        private ExcelConverter _excelConverter;
        private string _outputPath;

        [SetUp]
        public void SetUp()
        {
            _excelConverter = new ExcelConverter();
            _outputPath = @$"D:\report.csv"; // You can change this path as needed
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the output file after each test
            if (File.Exists(_outputPath))
            {
                File.Delete(_outputPath);
            }
        }

        [Test]
        public void ConvertListToExcel_WithValidData_WritesExpectedOutput()
        {
            // Arrange
            var dataList = new List<TestData>
            {
                new TestData { Id = 1, Name = "Alice", Age = 30 },
                new TestData { Id = 2, Name = "Bob", Age = 25 }
            };

            // Act
            _excelConverter.ConvertListToExcel(dataList, _outputPath);

            // Assert
            Assert.IsTrue(File.Exists(_outputPath), "Output file was not created.");

            var outputContent = File.ReadAllLines(_outputPath);
            Assert.AreEqual(3, outputContent.Length, "Output file does not have the expected number of lines.");

            var headerLine = outputContent[0];
            var expectedHeader = "Id,Name,Age,";
            Assert.AreEqual(expectedHeader, headerLine, "Header line does not match the expected format.");

            var firstDataLine = outputContent[1];
            Assert.AreEqual("1,Alice,30,", firstDataLine, "First data line does not match the expected format.");

            var secondDataLine = outputContent[2];
            Assert.AreEqual("2,Bob,25,", secondDataLine, "Second data line does not match the expected format.");
        }

        [Test]
        public void ConvertListToExcel_WithEmptyData_CreatesFileWithHeaderOnly()
        {
            // Arrange
            var dataList = new List<TestData>();

            // Act
            _excelConverter.ConvertListToExcel(dataList, _outputPath);

            // Assert
            Assert.IsTrue(File.Exists(_outputPath), "Output file was not created.");

            var outputContent = File.ReadAllLines(_outputPath);
            Assert.AreEqual(1, outputContent.Length, "Output file does not have the expected number of lines.");

            var headerLine = outputContent[0];
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
