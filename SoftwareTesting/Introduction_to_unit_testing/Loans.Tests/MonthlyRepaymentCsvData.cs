using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Loans.Tests
{
    public class MonthlyRepaymentCsvData
    {
        public static IEnumerable GetTestCases(string csvFileName)
        {
            var csvLines = File.ReadAllLines(csvFileName);

            var testCases = csvLines.Select(x => x.Split(',').Select(y => y.Trim()).ToArray()).Select(values=>new TestCaseData(decimal.Parse(values[0]), decimal.Parse(values[1]), int.Parse(values[2]), decimal.Parse(values[3])));

            return testCases;
        }
    }
}