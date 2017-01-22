using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractuable : InteractuableItem
{
    public Transform transformFocus;
    public float yAxis;
    private PlayerLogic playerLogic;

    public bool started = false;

    private void Awake()
    {
        playerLogic = FindObjectOfType<PlayerLogic>();
    }

    public void TranslateToTransform()
    {
        CameraBehaviour.Instance.FocusOnTransform(transformFocus, yAxis);
        CameraBehaviour.Instance.OnFocusFinished += HandleOnTranslateFinihed;
    }

    private void HandleOnTranslateFinihed(Transform t)
    {
        CameraBehaviour.Instance.OnFocusFinished -= HandleOnTranslateFinihed;
        InteractionFinished();
    }

    private void OnDestroy()
    {
        if (CameraBehaviour.InstanceExists)
        {
            CameraBehaviour.Instance.OnFocusFinished -= HandleOnTranslateFinihed;
        }
    
    }


    public override void Interact()
    {
        base.Interact();
        if (!started && canInteract)
        {
            TranslateToTransform();
            started = true;
        }
    }

    public void InteractionFinished()
    {
        playerLogic.interacting = false;
        playerLogic.interactuableOnArea = null;
        canInteract = false;
        interacting = false;
        started = false;
        Invoke("ResetInteraction", 0.5f);
    }

    void ResetInteraction()
    {
        canInteract = true;
    }

    void Reset()
    {

    }

}
