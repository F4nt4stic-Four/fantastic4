using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
        
    }

    public void LoadSolarSystem()
    {
        SceneManager.LoadScene(1);

    }

    public void LoadAsteroidGame()
    {
        SceneManager.LoadScene(2);

    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
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
