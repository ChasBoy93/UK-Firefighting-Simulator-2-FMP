using TMPro;
using UnityEngine;

public class SetQuality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private const string QUALITY_PREF_KEY = "QualityLevel";

    private void Start()
    {
        int savedQuality = PlayerPrefs.GetInt(QUALITY_PREF_KEY, QualitySettings.GetQualityLevel());

        QualitySettings.SetQualityLevel(savedQuality, false);

        qualityDropdown.value = savedQuality;
        qualityDropdown.RefreshShownValue();
    }

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);

        PlayerPrefs.SetInt(QUALITY_PREF_KEY, index);
        PlayerPrefs.Save();
    }
}