using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBlock : BlockClass
{
    Rigidbody rigidbody;

    // runs in the start event
    public override void BlockStart()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // block update runs according to the time step set in the blockclass 
    public override void BlockUpdate()
    {
        // check if we are the highlighted block
        if (player.highlightedBlock == this)
        {
            isHighlighted = true;
        }
        else if (player.highlightedBlock != this)
        {
            isHighlighted = false;
        }

        // run the function that does things based on highlighted state
        HighlightControl();

        // make sure to update our faces
        if (player.heldBlock)
        {
            faceParent.SetActive(true);
        } else if (!player.heldBlock)
        {
            faceParent.SetActive(false);
        }
    }

    public override void Interact()
    {
        // turn our rigidbody gravity on or off
        // rigidbody.useGravity = !rigidbody.useGravity;
    }

    public override void Pickup()
    {
        // pickup or drop this object
        transform.parent = null;
        rigidbody.useGravity = false;
        // turn off our collider
        gameObject.GetComponent<Collider>().enabled = false;
        // set our position to the player's hold point
        transform.position = player.holdPoint.position;
        // set that as out parent
        transform.parent = player.holdPoint;
        // make sure our local position is zeroed out
        transform.localPosition = Vector3.zero;
    }

    public override void Place()
    {

    }

    public override void Place(Transform parent)
    {

        throw new System.NotImplementedException();
    }

    public override void Place(Transform parent, Vector3 position)
    {
        transform.position = position;
        transform.parent = parent;
    }

    public override void HighlightControl()
    {
        highlightObject.SetActive(isHighlighted);
    }
}
