using TMPro;
using UnityEngine;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
