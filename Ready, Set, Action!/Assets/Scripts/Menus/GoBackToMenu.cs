using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string menuSceneName = "MainMenuScene";

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}