using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGenerator : MonoBehaviour 
{
	public Pulse mPulse;
	public void SendPulse(float radius, AnimationCurve curve, float seconds, float frequency)
	{
		Debug.Log( string.Format("Pulse! Rad: {0} Sec: {1} Freq: {2}", radius, seconds, frequency));
		mPulse.SendPulse(radius,curve,seconds,frequency);
	}
}
