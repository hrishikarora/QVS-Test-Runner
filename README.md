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

<img width="1280" height="764" alt="Screenshot (1)" src="https://github.com/user-attachments/assets/1ba5c88e-8379-4fce-b7d9-bcab5af64fa8" />
<img width="1280" height="764" alt="Screenshot (2)" src="https://github.com/user-attachments/assets/1f7140d1-6ef9-4989-bb4e-21f959c27b49" />


