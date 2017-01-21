using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteractuable : InteractuableItem
{
	public string dialogKey = "TEST";
	public bool started = false;

	public void StartDialog(string dialogKey)
	{
		DialogManager dm = GameObject.FindObjectOfType<DialogManager>();
		dm.ShowDialog(dialogKey,this);
	}

	public override void Interact ()
	{
		base.Interact ();
		if(!started && canInteract)
		{
			StartDialog(dialogKey);
			started = true;
		}
	}

	public void InteractionFinished()
	{
		Debug.Log("Interaction finished");
		canInteract = false;
		started = false;
		Invoke("ResetInteraction",0.5f);
	}

	void ResetInteraction()
	{
		canInteract = true;
	}


}
