using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GHOSTS
{
	MANAGER,
	PETUS,
	HAG,
	SPY,
	HEIR
}

public enum HOURS
{
	TWELVE,
	NINE,
	SIX,
	THREE
}

[System.Serializable]
public class GhostsByPosition
{
	public Transform pivot;
	public GHOSTS ghost;
	public string dialogKey;
}

[System.Serializable]
public class GhostSettingsByHour
{
	public HOURS hour;
	public List<GhostsByPosition> ghostData;
}

[System.Serializable]
public class GhostPrefabsByType
{
	public GHOSTS type;
	public GameObject prefab;
}

public class HotelManager : MonoBehaviour 
{
	public HOURS testHour;

	[Header("ConfigData")]
	public List<GhostSettingsByHour> ghostSettingsByHour;
	public List<GhostPrefabsByType> ghostPrefabData;

	[Header("InGame")]
	public List<GhostPrefabsByType> instantiatedGhosts;


	void Awake()
	{
		SetGhostsConfigByHour(HOURS.TWELVE);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			SetGhostsConfigByHour(testHour);		
		}
	}

	void InstantiateGhost(GHOSTS type, Transform pivot, string dialogKey)
	{
		
		var pr = ghostPrefabData.Find( x => x.type == type).prefab;
		GameObject go = Instantiate(pr, pivot) as GameObject;

		var inGameGhost = new GhostPrefabsByType();
		inGameGhost.type = type;
		inGameGhost.prefab = go;
		instantiatedGhosts.Add(inGameGhost);

		go.transform.localPosition = Vector3.zero;
		go.GetComponent<DialogInteractuable>().dialogKey = dialogKey;
	}

	public void SetGhostsConfigByHour(HOURS hour)
	{
		foreach (var item in instantiatedGhosts) 
		{
			GameObject.Destroy(item.prefab);	
		}
		instantiatedGhosts.Clear();

		List<GhostsByPosition> ghostData =  ghostSettingsByHour.Find(x => x.hour == hour).ghostData;
		foreach (var item in ghostData) 
		{
			InstantiateGhost(item.ghost, item.pivot, item.dialogKey);	
		}
	}
}
