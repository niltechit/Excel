using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConvert.NilTechIT.Com.Excel
{
    public class ExcelConverter
    {
        public string ConvertListToExcel<T>(List<T> dataList)
        {
            // Create a StringBuilder to hold the CSV content
            StringBuilder csvData = new StringBuilder();

            // Get the properties of the class dynamically
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Add the header row dynamically based on property names
            foreach (var prop in properties)
            {
                csvData.Append(prop.Name + ",");
            }
            csvData.AppendLine();

            // Add data rows
            foreach (var item in dataList)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item, null) ?? string.Empty;
                    csvData.Append(value.ToString() + ",");
                }
                csvData.AppendLine();
            }

            // Return the CSV data as a string
            return csvData.ToString();
        }
        public void ConvertListToExcel<T>(List<T> dataList, string outputPath)
        {
            // Create a StringBuilder to hold the CSV content
            StringBuilder csvData = new StringBuilder();

            // Get the properties of the class dynamically
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Add the header row dynamically based on property names
            foreach (var prop in properties)
            {
                csvData.Append(prop.Name + ",");
            }
            csvData.AppendLine();

            // Add data rows
            foreach (var item in dataList)
            {
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item, null) ?? string.Empty;
                    csvData.Append(value.ToString() + ",");
                }
                csvData.AppendLine();
            }

            // Save the CSV data to a file
            File.WriteAllText(outputPath, csvData.ToString());
        }
    }
}
