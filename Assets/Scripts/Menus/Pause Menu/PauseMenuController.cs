using Artemis;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject playerCameraStop;
    public Animator pauseMenuAnimator;
    public Animator pauseMenuAnimatorOther;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {   
        pauseMenuAnimator.SetBool("Open 0", true);
        pauseMenuAnimatorOther.SetBool("OtherOpen", true);
        StartCoroutine(ClosePauseMenuDelay());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuAnimator.SetBool("Open 0", false);
        pauseMenuAnimatorOther.SetBool("OtherOpen", false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    IEnumerator ClosePauseMenuDelay()
    {
        yield return new WaitForSeconds(1f);
        pauseMenuUI.SetActive(false);
    }

    public void ExitTheGame()
    {
        Application.Quit();
        Debug.Log("Exiting the game");
    }

    public void LoadMainMenu(string LevelToLoad)
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
