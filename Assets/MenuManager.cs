using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
	public string nextScene = "Main";
	public void OnSpanishButtonClicked()
	{
		PlayerPrefs.SetString("language","spanish");
		SceneManager.LoadScene(nextScene);
	}

	public void OnEnglishButtonClicked()
	{
		PlayerPrefs.SetString("language","english");
		SceneManager.LoadScene(nextScene);
	}
}
