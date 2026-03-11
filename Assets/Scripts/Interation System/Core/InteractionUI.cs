using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public GameObject promptRoot;    // parent of text + icon
    public TMP_Text promptText;      // assign your TMP text in Inspector
    public GameObject promptIcon;    // optional icon

    public void Show(string text)
    {
        if (promptRoot != null)
            promptRoot.SetActive(true);

        if (promptIcon != null)
            promptIcon.SetActive(true);

        if (promptText != null)
            promptText.text = text; // <-- set the text
    }

    public void Hide()
    {
        if (promptRoot != null)
            promptRoot.SetActive(false);

        if (promptIcon != null)
            promptIcon.SetActive(false);
    }
}