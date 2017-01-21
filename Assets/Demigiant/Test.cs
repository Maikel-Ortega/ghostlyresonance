using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour {
    AudioSource audioSourcePrueba;
    // Use this for initialization
    void Start () {

        audioSourcePrueba.DOFade(0f, 1f); //A silencio en 1 segundo
        audioSourcePrueba.DOPitch(0.8f, 2f); //menos pitch en 2 segundos

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
