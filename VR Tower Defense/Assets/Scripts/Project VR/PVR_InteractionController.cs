using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_InteractionController : MonoBehaviour {
    public Transform snapColliderOrigin;
    public GameObject ControllerModel;

    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector3 angularVelocity;
    [HideInInspector]
    public bool gripIsHeld;
    [HideInInspector]
    public bool shootingEnabled;

    private PVR_InteractionObject objectBeingInteractedWith;

    private SteamVR_TrackedObject trackedObj;

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public PVR_InteractionObject InteractionObject
    {
        get { return objectBeingInteractedWith; }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void CheckForInteractionObject()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(snapColliderOrigin.position,
            snapColliderOrigin.lossyScale.x / 2f);

        foreach(Collider overlappedCollider in overlappedColliders)
        {
            if(overlappedCollider.CompareTag("InteractionObject") &&
                overlappedCollider.GetComponent<PVR_InteractionObject>().IsFree())
            {
                objectBeingInteractedWith = overlappedCollider.GetComponent<PVR_InteractionObject>();
                objectBeingInteractedWith.OnTriggerWasPressed(this);
                return;
            }
        }

        /*.Log(objectBeingInteractedWith.name);

        if(objectBeingInteractedWith.name == "Enfield")
        { shootingEnabled = true; }
        else
        { shootingEnabled = false; }*/
    }

    public bool GripIsHeld()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            gripIsHeld = true;
            return gripIsHeld;
        }
        else
        {
            gripIsHeld = false;
            return gripIsHeld;
        }
    }

    public virtual void Update()
    {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                CheckForInteractionObject();
            }

            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (objectBeingInteractedWith)
                {
                    if (objectBeingInteractedWith)
                    {
                        objectBeingInteractedWith.OnTriggerIsBeingPressed(this);
                    }
                }
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                if (objectBeingInteractedWith)
                {
                    objectBeingInteractedWith.OnTriggerWasReleased(this);
                    objectBeingInteractedWith = null;
                }
            }

    }

    private void UpdateVelocity()
    {
        velocity = Controller.velocity;
        angularVelocity = Controller.angularVelocity;
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    public void HideControllerModel()
    {
        ControllerModel.SetActive(false);
    }

    public void ShowControllerModel()
    {
        ControllerModel.SetActive(true);
    }

    public void Vibrate(ushort strength)
    {
        Controller.TriggerHapticPulse(strength);
    }

    public void SwitchInteractionObjectTo(PVR_InteractionObject interactionObject)
    {
        objectBeingInteractedWith = interactionObject;
        objectBeingInteractedWith.OnTriggerWasPressed(this);
    }
}
