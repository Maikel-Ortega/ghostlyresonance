using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour {

	public AudioSource [] footsteps,click;
	AudioSource sound;
	public AudioSource chimea,chimeb,chimec,chimed, door, cristal, wall,clock,pulse,cuco,wrong;

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

	public void PlayGlass(){

		if (cristal	 != null) 
		{
			cristal.Play ();
		} 
		else 
		{
			Debug.Log(cristal.name + " is null");
		}

	}

	public void PlayWall(){

		if (wall!= null) 
		{
			wall.Play ();
		} 
		else 
		{
			Debug.Log(wall.name + " is null");
		}

	}



	public void PlayDoor(){

		if (door != null) 
		{
			door.Play ();
		} 
		else 
		{
			Debug.Log(door.name + " is null");
		}

	}




	public void PlayChime(int id){

	
		switch(id){

		case 0: 
			chimea.Play ();
			break;
		case 1: 
			chimeb.Play ();
			break;
		case 2: 
			chimec.Play ();
			break;
		case 3: 
			chimed.Play ();
			break;

		}


	}

	public void PlayWrong(){
	
		wrong.Play ();
	
	}


	public void PlayClock(){
	
		clock.Play ();

	
	}

	public void PlayCuco(){
	
		cuco.Play ();
	
	}


	public void PlayPulse(){
	
		pulse.Play ();
	
	}


	public void PlayFootsteps(){
	
		int random;

		if(footsteps!=null){
			
		random = Random.Range (0, footsteps.Length);

			footsteps [random].Play ();
		}
		else
		{
			Debug.Log ( "list is null");
		}
	}

	public void PlayClick(){

		int random;

		if(click!=null){

			random = Random.Range (0, click.Length);

			click [random].Play ();
		}
		else
		{
			Debug.Log ( "list is null");
		}
	}

	public void RandomPitch(){

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
