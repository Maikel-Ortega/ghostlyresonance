using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFloor : MonoBehaviour {

    public Transform t;
    public float yDestino;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)){
            CameraBehaviour.Instance.FocusOnTransform(t, yDestino);
        }
    }

}
