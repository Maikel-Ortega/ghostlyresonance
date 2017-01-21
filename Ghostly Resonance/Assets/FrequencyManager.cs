using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyManager : MonoBehaviour 
{
	public float maxFreq = 400f;
	public float minFreq = 0f;
	public WaveRenderer waveRenderer;

	private float _frequency;
	public float frequency 
	{
		get
		{
			return _frequency;
		}
		set
		{
			_frequency = Mathf.Clamp(value, minFreq, maxFreq);
			waveRenderer.mFreq = _frequency;
		}
	}
}
