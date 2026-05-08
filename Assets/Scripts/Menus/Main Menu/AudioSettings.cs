using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class AudioSettings : MonoBehaviour
{
    private Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public AudioMixer mixer;
    public string volumeName;
    public string playerPrefsKey = "VolumeSetting";
    public TMP_Text volumeLabel;

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(playerPrefsKey);
            slider.value = savedVolume;
        }

        UpdateValueOnChange(slider.value);

        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
            SaveVolume(slider.value);
        });
    }

    public void UpdateValueOnChange(float value)
    {
        if (mixer != null)
        {
            mixer.SetFloat(volumeName, Mathf.Log(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        }

        if (volumeLabel != null)
        {
            volumeLabel.text = Mathf.Round(value * 100.0f).ToString() + "%";
        }
    }

    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(playerPrefsKey, value);
        PlayerPrefs.Save();
    }
}