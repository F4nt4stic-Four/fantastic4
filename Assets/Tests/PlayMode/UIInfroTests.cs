// ------------------ UIInfoTests.cs ------------------
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TMPro;

public class UIInfoTests
{
    [UnityTest]
    public IEnumerator EnableUI_SetsCorrectText()
    {
        // Create UI panel
        var panelGO = new GameObject("UIPanel");

        // Add a dummy spacer to simulate actual hierarchy (index 0)
        new GameObject("Spacer").transform.SetParent(panelGO.transform);

        // Create and assign TMP text objects
        var label = new GameObject("Text1").AddComponent<TextMeshProUGUI>();
        label.transform.SetParent(panelGO.transform);

        var distance = new GameObject("Text2").AddComponent<TextMeshProUGUI>();
        distance.transform.SetParent(panelGO.transform);

        // Create Camera object and assign the panel to it
        var cameraGO = new GameObject("Camera");
        var camera = cameraGO.AddComponent<CameraMovement>();

        // Assign the UIInfoPanel via reflection
        camera.GetType().GetField("UIInfoPanel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(camera, panelGO);

        // Manually call Awake to initialize planetData
        camera.Invoke("Awake", 0f);

        // Simulate UI trigger
        camera.SendMessage("EnableUI", "Mars");

        yield return null;

        Assert.IsTrue(label.text.Contains("Mars"));
        Assert.IsTrue(distance.text.Contains("227.9"));
    }
}
