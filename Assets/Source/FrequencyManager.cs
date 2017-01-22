using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyManager : MonoBehaviour 
{
	public float maxFreq = 400f;
	public float minFreq = 0f;
	public WaveRenderer waveRenderer;
    public UnityEngine.UI.Slider _sldIndicador;

	private float _frequency= 80;
	public float frequency 
	{
		get
		{
			return _frequency;
		}
		set
		{

			_frequency = Mathf.Clamp(value, minFreq, maxFreq);
            _sldIndicador.value = (_frequency - minFreq) / (maxFreq - minFreq);

            waveRenderer.mFreq = _frequency;
		}
	}
}
