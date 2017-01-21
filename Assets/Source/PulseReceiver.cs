using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PulseReceiver : MonoBehaviour 
{
	public float targetFrequency = 30f;
	public float freqAcceptableOffset= 0f;
	public bool vibrate = true; //Hace que el objeto vibre, más fuerte mientras más te acerques a la frecuencia aceptable
	public Transform vibrationTransform;
	public Vector3 vibrationDir;
	public float cdTime = 0f;
	public float maxCdTime = 1f;

	public void OnPulseReceived(Pulse p)
	{
		if(cdTime ==0)
		{
			cdTime = maxCdTime;
			Debug.Log(string.Format("<color=magenta> Pulse received {0} </color>", p.ToString()));

			if(p.mFrequency < targetFrequency + freqAcceptableOffset && p.mFrequency > targetFrequency -freqAcceptableOffset)
			{
				Reaction();
			}
			else if(vibrate)
			{
				float maxF = GameObject.FindObjectOfType<FrequencyManager>().maxFreq;
				float minF = GameObject.FindObjectOfType<FrequencyManager>().minFreq;

				float maxDifference=0f;
				float difference =0f;
				if(p.mFrequency > targetFrequency + freqAcceptableOffset)
				{
					maxDifference = maxF - (targetFrequency + freqAcceptableOffset);
					difference = p.mFrequency - (targetFrequency + freqAcceptableOffset);
				}
				else if(p.mFrequency < targetFrequency - freqAcceptableOffset)
				{
					maxDifference = (targetFrequency - freqAcceptableOffset) - minF;
					difference = (targetFrequency - freqAcceptableOffset) -  p.mFrequency;
				}


				float maxStr =0.5f;
				float str = Mathf.Lerp(maxStr, 0f, difference/maxDifference);

				int vibrato = (int)Mathf.Lerp(22,12, difference/maxDifference);

				Debug.Log("Vibrating. Str: "+ str);
				//vibrationTransform.DOShakePosition(0.7f, vibrationDir*str,25,0);
				vibrationTransform.DOPunchPosition(str*vibrationDir, 5,100);
			}
		}
	}

	void Update()
	{
		if(cdTime > 0f)
		{
			cdTime = Mathf.Clamp(cdTime-Time.deltaTime, 0f, maxCdTime);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("!");
	}

	protected virtual void Reaction()
	{
		this.transform.localScale = Vector3.one * 2;
	}
}
