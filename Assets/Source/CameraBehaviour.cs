using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[UnitySingleton(UnitySingletonAttribute.Type.ExistsInScene, true)]
public class CameraBehaviour : UnitySingleton<CameraBehaviour>
{

    // Use this for initialization

    public delegate void OnFocusFinishedDelegate(Transform transform);
    public OnFocusFinishedDelegate OnFocusFinished;
    public Transform playerTransform;


    Transform objectFocused;
    bool followingPlayer = true;


    void FixedUpdate()
    {
        if (followingPlayer)
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = newTransform;
        }
        
    }

    public void FocusOnTransform(Transform _objectFocused)
    {
        this.objectFocused = _objectFocused;
        Vector3 v3NewCameraPosition = new Vector3(_objectFocused.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        followingPlayer = false;
        Camera.main.transform.DOMove(v3NewCameraPosition, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            followingPlayer = true;
            OnFocusFinished(_objectFocused);

        });

    }
}