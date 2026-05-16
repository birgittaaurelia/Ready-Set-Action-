using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentState;
    private int currentLevel;
    private void Start()
    {
        currentState = "MainMenu";
        currentLevel = 1;
        animator.Play("MainMenu");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayButton()
    {
        currentState = "LevelSelect";
        animator.Play("MenuToLevelSelect");
    }
    public void CreditsButton()
    {
        currentState = "Credits";
        animator.Play("MenuToCredit");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void animationTransitionHandler(string eventName)
    {
        animator.Play(eventName);
    }
    public void LeftButton()
    {
        if(currentState == "LevelSelect")
        {
            if (currentLevel > 1)
            {
                currentLevel--;
                if (currentLevel == 1)
                {
                    animator.Play("TwoToOne");
                }
                else if (currentLevel == 2)
                {
                    animator.Play("ThreeToTwo");
                }
            }
            else{
                //play audio later
            }
        }
    }
    public void RightButton()
    {
        if (currentState == "LevelSelect")
        {
            if (currentLevel < 3)
            {
                currentLevel++;
                if (currentLevel == 2)
                {
                    animator.Play("OneToTwo");
                }
                else if (currentLevel == 3)
                {
                    animator.Play("TwoToThree");
                }
            }
            else
            {
                //play audio later
            }
        }
    }
    public void BackToMenu()
    {
        if (currentState == "LevelSelect")
        {
            animator.Play("LevelToMenu");
        }
        else if (currentState == "Credits")
        {
            animator.Play("CreditToMenu");
        }
        currentState = "MainMenu";
    }
}
