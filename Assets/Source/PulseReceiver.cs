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
	public ParticleSystem pulseCircle;

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

				float minSize = 0.4f;
				float maxSize = 3f;
				float maxStr =0.5f;

				float nValue = difference/maxDifference;
				float str = Mathf.Lerp(maxSize, minSize,nValue);

				if(float.IsNaN(str))
				{
					str = maxStr;
				}
				pulseCircle.transform.localScale = new Vector3(str,str,str);

				var ma = pulseCircle.main;

				ma.startColor = new Color(1,1,1,1-nValue);
				pulseCircle.Play();

				Debug.Log("Vibrating. Str: "+ str);
				vibrationTransform.DOShakePosition(0.7f, vibrationDir*str*0.5f,15,0);
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
		
		pulseCircle.transform.localScale = new Vector3(5f,5f,5f);
		var ma = pulseCircle.main;
		ma.startColor = new Color(1,1,1,1);
		pulseCircle.Play();

		vibrationTransform.DOShakePosition(1.7f, vibrationDir*1,25,0);
	}
}
