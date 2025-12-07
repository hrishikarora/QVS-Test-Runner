using QVSRunner.Core.Discovery;
using QVSRunner.Core.Execution;
using QVSRunner.Reports.JSON;
using UnityEngine;

public class TestRunner : MonoBehaviour
{
    [ContextMenu("Run QVS Tests")]
    public void RunTests()
    {
        var discovered = TestDiscovery.Discover();

        var results = TestExecutor.Execute(discovered);

        foreach (var r in results)
        {
            if (r.Passed)
                Debug.Log($"PASSED: {r.ClassName}.{r.MethodName}");
            else
                Debug.LogError($"FAILED: {r.ClassName}.{r.MethodName} -> {r.ErrorMessage}");
        }

        string path = Application.dataPath + "/QVS_TestReport.json";
        JsonReportWriter.WriteReport(results, path);
    }
}
