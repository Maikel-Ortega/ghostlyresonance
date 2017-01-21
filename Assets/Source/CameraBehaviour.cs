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
    public bool followingPlayer = true;
    private float yAxis = 0f;


    void LateUpdate()
    {
        if (followingPlayer)
        {
            Vector3 newTransform = new Vector3(playerTransform.position.x, yAxis, Camera.main.transform.position.z);
            Camera.main.transform.position = newTransform;
        }
        
    }

    public void FocusOnTransform(Transform _objectFocused, float _yAxis)
    {
        this.objectFocused = _objectFocused;
        yAxis = _yAxis;
        Vector3 v3NewCameraPosition = new Vector3(_objectFocused.position.x, yAxis, Camera.main.transform.position.z);
        followingPlayer = false;

        Camera.main.transform.DOMove(v3NewCameraPosition, 2f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            playerTransform.position = new Vector3(_objectFocused.position.x, yAxis, playerTransform.transform.position.z);
            followingPlayer = true;
            
            OnFocusFinished(_objectFocused);

        });

    }
}