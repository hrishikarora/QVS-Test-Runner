using QVSRunner.Core.Discovery;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[TestClass]
public class GameSceneTests
{
    private const string ScenePath = "Assets/Scenes/GameScene.unity";


    [Test]
    public void SceneLoadsSuccessfully()
    {
        EditorSceneManager.OpenScene(ScenePath);

        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        if (!scene.isLoaded)
            throw new System.Exception("GameScene failed to load.");
    }


    [Test]
    public void PlayerObjectExists()
    {
        EditorSceneManager.OpenScene(ScenePath);

        GameObject player = GameObject.Find("Player");

        if (player == null)
            throw new System.Exception("Player GameObject not found in the scene.");
    }


    [Test]
    public void StartButtonExists()
    {
        EditorSceneManager.OpenScene(ScenePath);

        var buttonObj = GameObject.Find("StartButton");

        if (buttonObj == null)
            throw new System.Exception("StartButton GameObject not found.");

        var button = buttonObj.GetComponent<Button>();

        if (button == null)
            throw new System.Exception("StartButton does not have a Button component.");
    }

    [Test]
    public void StartButtonInvokesCallback()
    {
        EditorSceneManager.OpenScene(ScenePath);

        var buttonObj = GameObject.Find("StartButton");
        if (buttonObj == null)
            throw new System.Exception("StartButton GameObject missing.");

        var button = buttonObj.GetComponent<Button>();
        if (button == null || !button.interactable || !button.enabled)
            throw new System.Exception("StartButton has no Button component.");

        bool wasClicked = false;

        button.onClick.AddListener(() => wasClicked = true);

        button.onClick.Invoke();

        if (!wasClicked)
            throw new System.Exception("StartButton.onClick did not invoke its callback.");
    }

    [Test]
    public void EditorFPSIsAbove30()
    {
        float fps = 1f / Time.deltaTime;

        if (fps < 30f)
            throw new System.Exception($"FPS too low: {fps} (expected > 30).");
    }
}
