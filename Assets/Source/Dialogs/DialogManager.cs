using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour 
{
	public DialogDatabase database;
	public DialogScriptable currentDialog;
	public bool english = true;

	public Animator windowAnimator;
	public Text windowText;
	public DialogInteractuable currentInteractuable;

	public int currentIndex = 0;

	void Awake()
	{
		string lang = PlayerPrefs.GetString("language");
		if(lang == "english")
		{
			english = true;
		}
		else
		{
			english = false;
		}
	}

	public void ShowDialog(string key, DialogInteractuable interactuable)
	{		
		Debug.Log("Showing dialog");
		windowAnimator.gameObject.SetActive(true);
		currentDialog = database.GetDialog(key, english);
		currentInteractuable = interactuable;
		ShowWindow();
	}

	void ShowWindow()
	{		
		windowAnimator.SetTrigger("show");	
		Invoke("StartDialog", 0.1f);
	}

	void StartDialog()
	{
		Debug.Log("Starting dialog");

		currentIndex = 0;
		ShowText(currentDialog.texts[currentIndex]);
		currentIndex++;
		currentInteractuable.OnInteract += CurrentInteractuable_OnInteract;
	}

	void ShowText(string text)
	{
		Debug.Log("Showing text" + text);

		windowText.text = text;
	}

	void CurrentInteractuable_OnInteract (InteractuableItem i)
	{
		if(currentIndex < currentDialog.texts.Count)
		{
			ShowText(currentDialog.texts[currentIndex]);
			currentIndex++;
		}
		else
		{
			currentInteractuable.OnInteract -= CurrentInteractuable_OnInteract;
			OnDialogFinished();
		}
	}

	void OnDialogFinished()
	{
		Debug.Log("OnDialogFinished");
		HideWindow();
		currentInteractuable.InteractionFinished();
		currentDialog = null;
		GameObject.FindObjectOfType<PlayerLogic>().FinishedDialog();
	}


	void HideWindow()
	{
		windowAnimator.SetTrigger("hide");
	}
}
