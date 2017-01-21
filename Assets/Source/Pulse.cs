using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour 
{
	public float maxRadius = 1f;
	public AnimationCurve mCurve;
	public float maxSeconds;
	public float mFrequency;
	public float curRad;
	public SpriteRenderer spr;
	Coroutine pulseCorr;

	public void SendPulse(float radius, AnimationCurve curve, float seconds, float frequency)
	{
		this.maxRadius = radius;
		this.mCurve = curve;
		this.maxSeconds = seconds;
		this.mFrequency = frequency;
		if(pulseCorr != null)
		{
			StopCoroutine(pulseCorr);
		}
		pulseCorr = StartCoroutine(PulseCoroutine());
	}

	IEnumerator PulseCoroutine()
	{
		float counter = 0;
		while (counter < maxSeconds)
		{
			float normalizedValue = counter/maxSeconds;
			float evaluatedValue = this.mCurve.Evaluate(normalizedValue);
			float curRad = evaluatedValue * maxRadius;

			if(normalizedValue > 0.75f)
			{
				float alpha = Mathf.Lerp(1,0, (normalizedValue-0.75f)/0.25f);
			}

			ChangeRadius(curRad);
			counter += Time.deltaTime;
			yield return null;
		}
		ChangeRadius(0f);
	}

	void ChangeRadius(float rad)
	{
		this.transform.localScale = new Vector3(rad, rad, 1f);
		curRad = rad/2;

		Vector3 vp = Camera.main.WorldToViewportPoint(this.transform.position);

		Shader.SetGlobalVector("_ShockwavePos", new Vector4(vp.x,vp.y,1f,1f));
		Shader.SetGlobalFloat("_Radius", rad/35f);

		if(rad == 0)
		{
			Shader.SetGlobalVector("_ShockwavePos", new Vector4(100,100,1f,1f));	
		}
		CheckCollisions();
	}

	void CheckCollisions()
	{
		List<Collider2D> colliders  =  new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, curRad, 1 << LayerMask.NameToLayer("PulseTrigger")));
		foreach(Collider2D col in colliders)
		{
			CheckCol(col);
		}
	}


	void CheckCol(Collider2D col)
	{
		if(col.GetComponent<PulseReceiver>() != null)
		{
			PulseReceiver rec = col.GetComponent<PulseReceiver>();
			rec.OnPulseReceived(this);
		}
	}
//
//	void OnTriggerEnter2D(Collider2D col)
//	{
//		Debug.Log("!");
//		if(col.GetComponent<PulseReceiver>() != null)
//		{
//			PulseReceiver rec = col.GetComponent<PulseReceiver>();
//			rec.OnPulseReceived(this);
//		}
//	}

	public override string ToString ()
	{
		return string.Format("[Pulse]  Rad: {0} Sec: {1} Freq: {2}", maxRadius, maxSeconds, mFrequency);
	}
}
