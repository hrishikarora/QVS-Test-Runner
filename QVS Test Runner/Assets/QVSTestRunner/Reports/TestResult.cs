using System;

namespace QVSRunner.Core.Execution
{
    [Serializable]
    public class TestResult
    {
        public string ClassName;
        public string MethodName;
        public bool Passed;
        public string ErrorMessage;
        public float DurationMs;
    }
}
