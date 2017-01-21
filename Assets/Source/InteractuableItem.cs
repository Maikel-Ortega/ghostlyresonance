using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableItem : MonoBehaviour 
{
	public delegate void InteractuableEvent(InteractuableItem i);
	public event InteractuableEvent OnInteract;
	public bool interacting = false;
	public bool canInteract = true;

	public virtual void Interact()
	{
		if(canInteract)
		{
			interacting = !interacting;
		}
	}
}
