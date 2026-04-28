using UnityEngine;
using DG.Tweening;
using UnityEditorInternal;
using System.Collections;
using System.Threading.Tasks;

public class MainMenuController : MonoBehaviour
{
    //Main Menu Button Tweens
    [Header("Left Button")]
    [SerializeField] RectTransform leftButton;
    [SerializeField] float leftOnPos, leftOffPos;

    [Header("Middle Button")]
    [SerializeField] RectTransform middleButton;
    [SerializeField] float middleOnPos, middleOffPos;

    [Header("Right Button")]
    [SerializeField] RectTransform rightButton;
    [SerializeField] float rightOnPos, rightOffPos;

    [Header("Bottom Button")]
    [SerializeField] RectTransform bottomButton;
    [SerializeField] float bottomOnPos, bottomOffPos;

    //Tutorial UI Tweens
    [Header("Left Tutorial Button")]
    [SerializeField] RectTransform leftTutButton;
    [SerializeField] float leftTutOnPos, leftTutOffPos;

    [Header("Right Tutorial Button")]
    [SerializeField] RectTransform rightTutButton;
    [SerializeField] float rightTutOnPos, rightTutOffPos;

    [Header("Bottom Tutorial Button")]
    [SerializeField] RectTransform bottomTutButton;
    [SerializeField] float bottomTutOnPos, bottomTutOffPos;

    //Other Stuff

    [SerializeField] float tweenDuration;

    [Header("Other UI")]
    public GameObject mainMenuUi;
    public GameObject tutorialUi;

    public async void TutorialMainButton()
    {
       await TutorialUIOn();
    }

    public async void TutorialBackButton()
    {
        await TutorialUIOff();
    }
    async Task TutorialUIOn()
    {
        //Main Menu Tweens
        leftButton.DOAnchorPosX(leftOffPos, tweenDuration).SetUpdate(true);
        middleButton.DOAnchorPosY(middleOffPos, tweenDuration).SetUpdate(true);
        rightButton.DOAnchorPosX(rightOffPos, tweenDuration).SetUpdate(true);
        await bottomButton.DOAnchorPosY(bottomOffPos, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();

        //Tutorial Menu Tweens
        leftTutButton.DOAnchorPosX(leftTutOnPos, tweenDuration).SetUpdate(true);
        rightTutButton.DOAnchorPosX(rightTutOnPos, tweenDuration).SetUpdate(true);
        bottomTutButton.DOAnchorPosY(bottomTutOnPos, tweenDuration).SetUpdate(true);

        mainMenuUi.SetActive(false);
        tutorialUi.SetActive(true);


    }

    async Task TutorialUIOff()
    {
        //Tutorial Menu Tweens
        leftTutButton.DOAnchorPosX(leftTutOffPos, tweenDuration).SetUpdate(true);
        rightTutButton.DOAnchorPosX(rightTutOffPos, tweenDuration).SetUpdate(true);
        await bottomTutButton.DOAnchorPosY(bottomTutOffPos, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();

        mainMenuUi.SetActive(true);
        tutorialUi.SetActive(false);

        //Main Menu Tweens
        leftButton.DOAnchorPosX(leftOnPos, tweenDuration).SetUpdate(true);
        middleButton.DOAnchorPosY(middleOnPos, tweenDuration).SetUpdate(true);
        rightButton.DOAnchorPosX(rightOnPos, tweenDuration).SetUpdate(true);
        bottomButton.DOAnchorPosY(bottomOnPos, tweenDuration).SetUpdate(true);


    }

    public void ExitTheGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }

    #region Tutorial Button

  /*  IEnumerator ToTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        mainMenuUi.SetActive(false);
        tutorialUi.SetActive(true);
    }

    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(1f);
        mainMenuUi.SetActive(true);
        tutorialUi.SetActive(false);
    } */

    #endregion
}
