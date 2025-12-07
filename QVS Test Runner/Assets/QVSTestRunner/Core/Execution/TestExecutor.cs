using System;
using System.Collections.Generic;
using QVSRunner.Core.Discovery;

namespace QVSRunner.Core.Execution
{
    public static class TestExecutor
    {
        public static List<TestResult> Execute(List<DiscoveredTest> tests)
        {
            var results = new List<TestResult>();

            foreach (var test in tests)
            {
                var result = new TestResult
                {
                    ClassName = test.ClassType.Name,
                    MethodName = test.Method.Name
                };

                try
                {
                    object instance = Activator.CreateInstance(test.ClassType);
                    var start = DateTime.Now;
                    test.Method.Invoke(instance, null);
                    var end = DateTime.Now;
                    result.DurationMs = (float)(end - start).TotalMilliseconds;
                    result.Passed = true; 
                }
                catch (Exception ex)
                {
                    result.Passed = false;
                    result.ErrorMessage = ex.Message;
                }

                results.Add(result);
            }

            return results;
        }
    }
}
