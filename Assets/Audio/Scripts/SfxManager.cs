using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour {

	AudioSource [] listSfx;
	AudioSource sound;

	void PlayAnySound(){
	
		if (sound != null) 
		{
			sound.Play ();
		} 
		else 
		{
			Debug.Log(sound.name + " is null");
		}
	
	}

	void PlayAnyRandomSound(){
	
		int random;

		if(listSfx!=null){
			
		random = Random.Range (0, listSfx.Length);

			listSfx [random].Play ();
		}
		else
		{
			Debug.Log ( "list is null");
		}
	}

	void RandomPitch(){

		float randompn,randomvalue;

		randompn = Random.Range (0, 1);
		randomvalue = Random.Range (0.0f, 0.9f);

		if (randompn == 1) {
			randomvalue=randomvalue*(-1);
		}
	
		sound.pitch += randomvalue;
	
	
	}
		


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
