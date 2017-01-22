using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampanaReciever : PulseReceiver
{
    public int campanaId = 0;
	public Animator amt;

    SfxManager sfxManager;

    void Awake()
    {
        sfxManager = FindObjectOfType<SfxManager>();
    }
    protected override void Reaction()
    {
        base.Reaction();
		amt.SetTrigger("chime");

    //    this.enabled = false;
   //     mGraphicPivot.gameObject.SetActive(false);
    //    mCollider.enabled = false;
        // Audiom
        sfxManager.PlayChime(campanaId);

    }
} 

