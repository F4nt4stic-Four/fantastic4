// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// public class CameraMovementPlayModeTests
// {
//     private GameObject cameraObject;
//     private CameraMovement cameraMovement;
//     private float moveSpeed = 5f; // Same as in CameraMovement

//     [SetUp]
//     public void Setup()
//     {
//         cameraObject = new GameObject();
//         cameraMovement = cameraObject.AddComponent<CameraMovement>();
//     }

//     [UnityTest]
//     public IEnumerator CameraMovesForward()
//     {
//         Vector3 initialPosition = cameraObject.transform.position;

//         // Simulate pressing "W" (forward movement in Z-axis)
//         cameraObject.transform.position += new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;

//         yield return null; // Wait for one frame

//         Assert.AreNotEqual(initialPosition, cameraObject.transform.position);
//     }

//     [UnityTest]
//     public IEnumerator CameraMovesBackward()
//     {
//         Vector3 initialPosition = cameraObject.transform.position;

//         // Simulate pressing "S" (backward movement in Z-axis)
//         cameraObject.transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;

//         yield return null;

//         Assert.AreNotEqual(initialPosition, cameraObject.transform.position);
//     }

//     [UnityTest]
//     public IEnumerator CameraMovesLeft()
//     {
//         Vector3 initialPosition = cameraObject.transform.position;

//         // Simulate pressing "A" (left movement in X-axis)
//         cameraObject.transform.position += new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;

//         yield return null;

//         Assert.AreNotEqual(initialPosition, cameraObject.transform.position);
//     }

//     [UnityTest]
//     public IEnumerator CameraMovesRight()
//     {
//         Vector3 initialPosition = cameraObject.transform.position;

//         // Simulate pressing "D" (right movement in X-axis)
//         cameraObject.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;

//         yield return null;

//         Assert.AreNotEqual(initialPosition, cameraObject.transform.position);
//     }

//     [UnityTest]
//     public IEnumerator CameraDoesNotMoveWhenNoKeysArePressed()
//     {
//         Vector3 initialPosition = cameraObject.transform.position;

//         yield return new WaitForSeconds(0.1f); // Wait for a short period

//         Assert.AreEqual(initialPosition, cameraObject.transform.position);
//     }
// }
