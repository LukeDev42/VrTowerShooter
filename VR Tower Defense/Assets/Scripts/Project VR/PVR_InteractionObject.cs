using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_InteractionObject : MonoBehaviour {

    protected Transform cachedTransform;
    [HideInInspector]
    public PVR_InteractionController currentController;

    public virtual void OnTriggerWasPressed(PVR_InteractionController controller)
    {
        currentController = controller;
    }

    public virtual void OnTriggerIsBeingPressed(PVR_InteractionController controller)
    {
    }

    public virtual void OnTriggerWasReleased(PVR_InteractionController controller)
    {
        currentController = null;
    }

    public virtual void Awake()
    {
        cachedTransform = transform;
        if(!gameObject.CompareTag("InteractionObject"))
        {
            Debug.LogWarning("this InteractionObject does not have the correct tag, setting it now.",
                gameObject);
            gameObject.tag = "InteractionObject";
        }
    }

    public bool IsFree()
    {
        return currentController == null;
    }

    public virtual void OnDestroy()
    {
        if(currentController)
        {
            OnTriggerWasReleased(currentController);
        }
    }
}
