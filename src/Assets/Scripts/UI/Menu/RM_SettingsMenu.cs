using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class RM_SettingsMenu : MonoBehaviour {
    [SerializeField]
    private TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    void Start() {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            transform.gameObject.SetActive(false);
        }
    }

    public void SetQuality(int qualityIndex) { QualitySettings.SetQualityLevel(qualityIndex, true); }
    public void SetFullScreen(bool isFullScreen) { Screen.fullScreen = isFullScreen; }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}