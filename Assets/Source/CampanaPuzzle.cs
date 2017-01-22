using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampanaPuzzle : MonoBehaviour 
{
	public List<CampanaReciever> bells;
	public int[] combination = {3,3,1,4};
	public int currentIndex = 0;
	public GameObject door;

	void Awake()
	{
		foreach (var item in bells) 
		{
			item.OnCampanaReact += Item_OnCampanaReact;	
		}
	}

	void Item_OnCampanaReact (CampanaReciever c)
	{
		if(combination[currentIndex]-1 == c.campanaId)
		{
			if(currentIndex == combination.Length-1)
			{
				OnCorrectCombination();
			}else
			{
				currentIndex++;
			}
		}
		else
		{
			OnWrongCampana();
		}
	}

	void OnCorrectCombination()
	{
		door.SetActive(false);
	}

	void OnWrongCampana()
	{
		currentIndex = 0;
		//FindObjectOfType<SfxManager>().PlayWrong;
	}
}
