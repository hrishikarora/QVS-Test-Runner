using UnityEditor;
using UnityEngine;
using QVSRunner.Core.Discovery;
using QVSRunner.Core.Execution;

public class QVSTestRunnerWindow : EditorWindow
{
    private Vector2 scrollPos;
    private System.Collections.Generic.List<TestResult> lastResults;
    private bool autoRun;

    [InitializeOnLoadMethod]
    static void OnProjectLoaded()
    {
        AssemblyReloadEvents.afterAssemblyReload += HandleAssemblyReload;
    }

    [MenuItem("QVS/Test Runner")]
    public static void ShowWindow()
    {
        GetWindow<QVSTestRunnerWindow>("QVS Test Runner");
    }

    private void OnGUI()
    {
        autoRun = EditorGUILayout.Toggle("Auto Run on Script Reload", autoRun);

        if (GUILayout.Button("Run Tests", GUILayout.Height(30)))
        {
            RunTests();
        }

        GUILayout.Space(10);

        if (lastResults == null)
        {
            EditorGUILayout.HelpBox("No tests run yet.", MessageType.Info);
            return;
        }

        DrawSummary();
        GUILayout.Space(10);

        DrawResults();
    }

    private void RunTests()
    {
        var discovered = TestDiscovery.Discover();
        lastResults = TestExecutor.Execute(discovered);

        string path = Application.dataPath + "/QVS_TestReport.json";
        QVSRunner.Reports.JSON.JsonReportWriter.WriteReport(lastResults, path);
    }


    private void DrawResults()
    {
        EditorGUILayout.LabelField("Test Results", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (var r in lastResults)
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);

            if (r.Passed)
                style.normal.textColor = Color.green;
            else
                style.normal.textColor = Color.red;

            EditorGUILayout.LabelField(
                $"{(r.Passed ? "PASS" : "FAIL")} - {r.ClassName}.{r.MethodName}  ({r.DurationMs} ms)",
                style
            );
            if (!r.Passed)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Error: " + r.ErrorMessage, EditorStyles.wordWrappedLabel);
                EditorGUI.indentLevel--;
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawSummary()
    {
        int passed = lastResults.FindAll(r => r.Passed).Count;
        int failed = lastResults.Count - passed;

        EditorGUILayout.LabelField("Summary", EditorStyles.boldLabel);

        EditorGUILayout.LabelField($"Total: {lastResults.Count}");

        GUIStyle passStyle = new GUIStyle(EditorStyles.label);
        passStyle.normal.textColor = Color.green;
        EditorGUILayout.LabelField($"Passed: {passed}", passStyle);

        GUIStyle failStyle = new GUIStyle(EditorStyles.label);
        failStyle.normal.textColor = Color.red;
        EditorGUILayout.LabelField($"Failed: {failed}", failStyle);
    }

    private static void HandleAssemblyReload()
    {
        var window = GetWindow<QVSTestRunnerWindow>();

        if (window != null && window.autoRun)
        {
            window.RunTests();
            window.Repaint();
        }
    }

}
