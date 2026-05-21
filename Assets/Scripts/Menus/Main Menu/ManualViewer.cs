using UnityEngine;
using UnityEngine.UI;

public class ManualViewer : MonoBehaviour
{
    [Header("Manual Pages")]
    public Sprite[] pages;

    [Header("UI")]
    public Image pageDisplay;

    private int currentPage = 0;

    void Start()
    {
        currentPage = 0;
        ShowPage();
    }

    public void ResetToPageOne()
    {
        currentPage = 0;
        ShowPage();
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }

    void ShowPage()
    {
        pageDisplay.sprite = pages[currentPage];
    }
}