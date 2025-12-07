using QVSRunner.Core.Execution;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QVSRunner.Reports.JSON
{
    public static class JsonReportWriter
    {
        public static void WriteReport(List<TestResult> results, string filePath)
        {
            var report = new TestReport()
            {
                total = results.Count,
                passed = results.FindAll(r => r.Passed).Count,
                failed = results.FindAll(r => !r.Passed).Count,
                results = results
            };

            string json = JsonUtility.ToJson(report, true);

            File.WriteAllText(filePath, json);

            Debug.Log($"JSON Report written to: {filePath}");
        }
    }
}

[System.Serializable]
public class TestReport
{
    public int total;
    public int passed;
    public int failed;
    public List<TestResult> results;
}