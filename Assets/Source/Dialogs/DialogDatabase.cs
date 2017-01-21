using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogsByKey
{
	public string key;
	public DialogScriptable dialogScriptable;
}

[CreateAssetMenu()]
public class DialogDatabase : ScriptableObject
{
	public List<DialogsByKey> englishDialogs;
	public List<DialogsByKey> spanishDialogs;

	public DialogScriptable GetDialog(string key, bool english)
	{
		if(english)
		{
			return englishDialogs.Find(x=> x.key == key).dialogScriptable;
		}
		else
		{
			return spanishDialogs.Find(x=> x.key == key).dialogScriptable;
		}
	}
}
