using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicManager : MonoBehaviour {

	public AudioSource [] ghost;
	public AudioSource Ambient, music1, music2,music3;
	public AudioSource cbass,violins,pedvi;
	public int  time = 20;
	public float timecont=0;
	public int instruments= 2;
	public bool control=true;


	void PlayGhost(int id){
	
		ghost [id].Play();

	}

	void bajarvolumen(AudioSource audio){
		float time = 0;
	
			
	}


	void MusicSystem(){

		int random;
		random = Random.Range (0, instruments);

		switch(random) //donde opción es la variable a comparar
		{
		case 0: 

			if (!violins.isPlaying) 
			{
			   
				violins.Play ();
				violins.DOFade (1f, 2f);
			
			} 
			else 
			{
				violins.DOFade (0f, 2f).OnComplete (() => {
					violins.Stop ();
				}
				);

			}


			break;
		
		case 1: 

			if (!pedvi.isPlaying) 
			{

				pedvi.Play ();
				pedvi.DOFade (1f, 2f);

			} 
			else 
			{

				pedvi.DOFade (0f, 2f).OnComplete (() => {
					pedvi.Stop ();
				}
				);


			}

			break;
	    }

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		timecont += 1 *Time.deltaTime ;

		if (timecont >= 5 && timecont<6 && control) {
		
			MusicSystem ();
			control = false;

		} 


		if (timecont > 6) 
		
		{
			timecont = 0;
			control = true;
		}


	}

}
