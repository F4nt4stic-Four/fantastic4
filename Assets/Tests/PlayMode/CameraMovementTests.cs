using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CameraMovementTests
{
    [UnityTest]
    public IEnumerator CameraMovesWhenSimulated()
    {
        GameObject go = new GameObject("Camera");
        var cam = go.AddComponent<CameraMovement>();

        Vector3 initialPos = go.transform.position;
        go.transform.position += Vector3.forward;

        yield return null;

        Assert.AreNotEqual(initialPos, go.transform.position);
    }
}

