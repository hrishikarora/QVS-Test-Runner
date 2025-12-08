# QVS Test Runner
QVS Test Runner is a Unity based test framework designed to automate tests. It uses simple attributes [TestClass], [Test] and simple reflection discovery to create testing workflow.

The goal of QVS Test Runner is to provide:
- A minimal test system
- Easy integration directly inside Unity projects
- **JSON-based reporting** for analysis.

# Writing a Test

Test passes when completes without throwing an exception.  
Test fails if any exception is thrown.

```csharp
[TestClass]
public class MathTests
{
    [Test]
    public void MultiplyTest()
    {
        int result = 2 * 2;
        if (result != 4)
            throw new Exception("Expected 4 but got " + result);
    }
}
```

# Json Example
```json
{
  "tests": [
    {
      "class": "MathTests",
      "method": "MultiplyTest",
      "status": "Passed",
      "durationMs": 0
    }
  ]
}
```

# Build (CI Integration)

QVS Test Runner includes continuous integration support using **GitHub Actions**.  
Every push to the repository triggers an automatic Unity build using the `game-ci" workflow.
