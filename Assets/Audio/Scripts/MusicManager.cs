using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicManager : MonoBehaviour {

	public AudioSource [] ghost;
	public AudioSource Ambient;
	public AudioSource cbass,violins,celesta1,celesta2,celesta3,celesta4,celesta5,piano,fagot,pipe,horn,balalika;
	public int  time = 5;
	public float timecont=0;
	public int instruments= 7;
	public bool control=true;


	void PlayGhost(int id){
	
		ghost [id].Play();

	}

	void bajarvolumen(AudioSource audio){
		float time = 0;
	
			
	}

	void GeneralPlay(AudioSource audio){

		if (!audio.isPlaying) 
		{
			audio.volume = 0;
			audio.Play ();
			audio.DOFade (1f, 2f);

		} 
		else 
		{
			audio.DOFade (0f, 3f).OnComplete (() => {
				audio.Stop ();

			}
			);

		}


	}


	void MusicSystem(){

		int random;
		random = Random.Range (0, instruments);

		switch(random) //donde opción es la variable a comparar
		{
		case 0: 


			GeneralPlay (cbass);
			break;
		
		case 1: 
			GeneralPlay (violins);

		
			break;


		case 2: 
			
			GeneralPlay (celesta1);
			break;


		case 3: 

			GeneralPlay (celesta2);
			break;


		case 4: 

			GeneralPlay (celesta3);
			break;
		case 5: 

			GeneralPlay (celesta4);
			break;
		case 6: 

			GeneralPlay (celesta5);
			break;
	    }

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		timecont += 1 *Time.deltaTime ;

		if (timecont >= time && timecont<time+1 && control) {
		
			MusicSystem ();
			control = false;

		} 


		if (timecont > time+1) 
		
		{
			timecont = 0;
			control = true;
		}


	}

}
