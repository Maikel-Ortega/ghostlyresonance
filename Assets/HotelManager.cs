using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GHOSTS
{
	RECEPCIONISTA,
	VIEJA
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
		var inGameGhost = instantiatedGhosts.Find(x => x.type == type);
		if(inGameGhost != null)
		{
			GameObject.Destroy(inGameGhost.prefab);
			instantiatedGhosts.Remove(inGameGhost);
		}
		var pr = ghostPrefabData.Find( x => x.type == type).prefab;
		GameObject go = Instantiate(pr, pivot) as GameObject;

		inGameGhost = new GhostPrefabsByType();
		inGameGhost.type = type;
		inGameGhost.prefab = go;
		instantiatedGhosts.Add(inGameGhost);

		go.transform.localPosition = Vector3.zero;
		go.GetComponent<DialogInteractuable>().dialogKey = dialogKey;
	}

	public void SetGhostsConfigByHour(HOURS hour)
	{
		List<GhostsByPosition> ghostData =  ghostSettingsByHour.Find(x => x.hour == hour).ghostData;
		foreach (var item in ghostData) 
		{
			InstantiateGhost(item.ghost, item.pivot, item.dialogKey);	
		}
	}
}
