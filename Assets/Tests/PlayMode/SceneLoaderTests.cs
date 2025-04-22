using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoaderTests
{
    [UnityTest]
    public IEnumerator LoadScene_TriggersLoad()
    {
        var go = new GameObject();
        var loader = go.AddComponent<SceneLoader>();

        // Use reflection to call private method (if needed)
        loader.LoadScene(0);

        yield return null;

        Assert.AreEqual(0, SceneManager.GetActiveScene().buildIndex);
    }
}
