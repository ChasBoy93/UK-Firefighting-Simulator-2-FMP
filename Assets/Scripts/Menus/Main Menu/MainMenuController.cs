using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public void ExitTheGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
