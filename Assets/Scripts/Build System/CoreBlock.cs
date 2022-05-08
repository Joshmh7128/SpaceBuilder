using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBlock : BlockClass
{



    // runs in the start event
    public override void BlockStart()
    {

    }

    // block update runs according to the time step set in the blockclass 
    public override void BlockUpdate()
    {
        // check if we are the highlighted block
        if (player.highlightedBlock == this)
        {
            isHighlighted = true;
        } else if (player.highlightedBlock != this)
        {
            isHighlighted = false;
        }

        // run the function that does things based on highlighted state
        HighlightControl();

    }

    public override void Interact()
    {
        // turn our rigidbody gravity on or off

    }

    public override void Pickup()
    {
        // pickup or drop this object

        throw new System.NotImplementedException();
    }

    public override void HighlightControl()
    {
        highlightObject.SetActive(isHighlighted);
    }
}
