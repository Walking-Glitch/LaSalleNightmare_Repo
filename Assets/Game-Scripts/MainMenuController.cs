using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController: MonoBehaviour
{

	[Header("Volume Setting")]
	[SerializeField] private TMP_Text volValue = null;
	[SerializeField] private Slider volSlider = null;
	[SerializeField] private float defaultVol = 0.5f;

	[Header("Confirmation")]
	[SerializeField] private GameObject confirmationPrompt = null;


	[Header("Video")]
	private int _qualityLvl;
	

	[Header("All Levels")]
	public string _newGame;
	private string loadLvl;
	[SerializeField] private GameObject noSavedGame = null;

	[Header("ResolutionDropdown")]
	public TMP_Dropdown resolutionDropdown;
	private Resolution[] resolutions;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;

		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);
			if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
			{
				currResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	

	public void NewGameBtnYes()
	{
		SceneManager.LoadScene("6thFloor");
	}

	public void LoadGameBtnYes()
	{
		if (PlayerPrefs.HasKey("SavedLvl"))
		{
			loadLvl = PlayerPrefs.GetString("SavedLvl");
			SceneManager.LoadScene(loadLvl);
		}
		else
		{
			noSavedGame.SetActive(true);
		}
	}

	public void ExitBtn()
	{
		Application.Quit();
	}

	public void SetVol(float volume)
    {
		AudioListener.volume = volume;
		volValue.text = volume.ToString("0.0");
    }

	public void VolApply()
    {
		PlayerPrefs.SetFloat("masterVol,", AudioListener.volume);
		StartCoroutine(ConfirmationBox());
    }

	public void ResetBtn(string Menutype)
	{
		if(Menutype == "Audio")
		{
			AudioListener.volume = defaultVol;
			volSlider.value = defaultVol;
			volValue.text = defaultVol.ToString("0.0");
			VolApply();
		}
	}

	public IEnumerator ConfirmationBox()
    {
		confirmationPrompt.SetActive(true);
		yield return new WaitForSeconds(2);
		confirmationPrompt.SetActive(false);
    }

	public void SetQuality(int qualityIndex)
	{
		_qualityLvl = qualityIndex;
	}

	public void GraphicsApply()
	{
		PlayerPrefs.SetInt("masterQuality", _qualityLvl);
		QualitySettings.SetQualityLevel(_qualityLvl);

		StartCoroutine(ConfirmationBox());
	}
}
