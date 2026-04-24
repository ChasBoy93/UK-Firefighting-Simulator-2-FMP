using Artemis;
using System.Collections;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject playerCameraStop;
    public Animator pauseMenuAnimator;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        playerCameraStop.GetComponent<FPController>().enabled = false;
    }

    IEnumerator ClosePauseMenuDelay()
    {
        yield return new WaitForSeconds(0.5f);
        pauseMenuUI.SetActive(false);
    }
}
