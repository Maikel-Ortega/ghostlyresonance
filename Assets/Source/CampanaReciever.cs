using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampanaReciever : PulseReceiver
{
    public int campanaId = 0;
    public Collider2D campanaCollider;
    public Transform campanaGraphicPivot;
    public Collider2D mCollider;
    public Transform mGraphicPivot;

    SfxManager sfxManager;

    void Awake()
    {
        sfxManager = FindObjectOfType<SfxManager>();
    }
    protected override void Reaction()
    {
        base.Reaction();

    //    this.enabled = false;
   //     mGraphicPivot.gameObject.SetActive(false);
    //    mCollider.enabled = false;
        // Audiom
        sfxManager.PlayChime(campanaId);

    }
} 

