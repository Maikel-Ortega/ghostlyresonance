using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRenderer : MonoBehaviour 
{
	public LineRenderer mLineRenderer;
	int maxPoints = 200;
	public float speed = 2f;
	public float width= 40f;
	public float mFreq = 5f;
	public float offset = 0f;
	public float height= 100f;
	public Vector3 centerPointLocalPosition;
	public List<Vector3> mPositions;

	void Awake()
	{
		mLineRenderer.numPositions = maxPoints;
		RenderWave(100f);
	}

	public void RenderWave(float freq)
	{
		mLineRenderer.startColor = Color.Lerp(Color.red, Color.cyan, (freq-20)/100);
		mLineRenderer.endColor= Color.Lerp(Color.red, Color.cyan, (freq-20)/100);

		mLineRenderer.SetPositions(CalculatePositions(freq));
	}

	Vector3[] CalculatePositions(float f)
	{
		Vector3[] pos = new Vector3[maxPoints];
		mPositions.Clear();

		for(int i = 0; i < pos.Length; i++)
		{
			float x = i*width/pos.Length;
			float y = height * Mathf.Sin(f* (x+offset)/width);
			Vector3 p = new Vector3(x,y);
			pos[i] = p;
			mPositions.Add(p);
		}

		return pos;
	}

	void Update()
	{
		RenderWave(mFreq);
		offset += speed * Time.deltaTime;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.TransformPoint(centerPointLocalPosition), transform.TransformPoint(centerPointLocalPosition) +  Vector3.right * width);
		foreach(var p in mPositions)
		{
			Gizmos.DrawSphere(transform.TransformPoint(p), 0.01f);
		}
	}
}
