using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MoveToNextScene()
    {
        SceneManager.LoadScene("ActionScene");
    }
}