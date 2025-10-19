using UnityEngine;
using UnityEngine.SceneManagement;

// To exit the play mode in the editor when user press the Quit button
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(0); // Replace "MainGameScene" with your actual scene name
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
