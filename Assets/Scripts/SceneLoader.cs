using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public enum SceneType
{
    Main = 0,
    SolarSystem = 1,
    AsteroidGame = 2
}

public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    

    void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}