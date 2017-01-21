using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseReceiver : MonoBehaviour 
{
	public float targetFrequency = 30f;
	public float offset= 0f;

	public void OnPulseReceived(Pulse p)
	{
		Debug.Log(string.Format("<color=magenta> Pulse received {0} </color>", p.ToString()));

		if(p.mFrequency < targetFrequency + offset || p.mFrequency > targetFrequency -offset)
		{
			//Reaction(Mathf.Abs)
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("!");
	}

	virtual void Reaction(float difference)
	{
		
	}
}
