using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[UnitySingleton(UnitySingletonAttribute.Type.ExistsInScene, true)]
public class CameraBehaviour : UnitySingleton<CameraBehaviour> {

    // Use this for initialization

    public delegate void OnFocusFinishedDelegate(Transform transform);
    public OnFocusFinishedDelegate OnFocusFinished;


    Transform objectFocused;
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FocusOnTransform(Transform _objectFocused)
    {
        this.objectFocused = _objectFocused;
    }
}
