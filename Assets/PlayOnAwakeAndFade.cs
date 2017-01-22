using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayOnAwakeAndFade : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        AudioSource sour = GetComponent<AudioSource>() ;
        sour.volume = 0f;
        sour.Play();
        sour.DOFade(1f, 2f);
	}
	
}
