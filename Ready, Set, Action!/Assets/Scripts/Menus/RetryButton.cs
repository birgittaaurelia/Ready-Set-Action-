using UnityEngine;
using UnityEngine.SceneManagement; 

public class RetryButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string menuSceneName = "ActionScene";

    public void GoToRetry()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}