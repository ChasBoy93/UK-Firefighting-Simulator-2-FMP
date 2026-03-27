using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitResponce : MonoBehaviour
{
    public TMP_InputField[] textInputFields;

    public void ClearText()
    {
        foreach (TMP_InputField field in textInputFields)
        {
            field.text = "";
        }
    }
}
