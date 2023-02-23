using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenuController mainMenuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volValue = null;
    [SerializeField] private Slider volSlider = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;



	private void Awake()
	{
		if(canUse)
		{
			if (PlayerPrefs.HasKey("masterVol"))
			{
				float localVolume = PlayerPrefs.GetFloat("masterVol");

				volValue.text = localVolume.ToString("0.0");
				volSlider.value = localVolume;
				AudioListener.volume = localVolume;
			}
			else
			{
				mainMenuController.ResetBtn("Audio");
			}

			if (PlayerPrefs.HasKey("masterQuality"))
			{
				int localQuality = PlayerPrefs.GetInt("masterQuality");
				qualityDropdown.value = localQuality;
				QualitySettings.SetQualityLevel(localQuality);
			}
		}
	}
}
