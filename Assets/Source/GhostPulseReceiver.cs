using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPulseReceiver : PulseReceiver 
{
	public Collider2D ghostCollider;
	public Transform ghostGraphicPivot;
	public Collider2D mCollider;
	public Transform mGraphicPivot;

	protected override void Reaction ()
	{
		base.Reaction ();
		ghostCollider.enabled = true;
		ghostGraphicPivot.gameObject.SetActive(true);
		this.enabled = false;
		mGraphicPivot.gameObject.SetActive(false);
		mCollider.enabled = false;
	}
}

