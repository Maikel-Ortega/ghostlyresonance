using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTest : MonoBehaviour {

    public Transform focusTransform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CameraBehaviour.Instance.FocusOnTransform(focusTransform);
        }
    }
}
