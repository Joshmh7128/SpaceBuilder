using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockClass : MonoBehaviour
{
    public abstract void Interact(); // what happens when we interact with this object?
    public abstract void Pickup(); // what happens when we pickup this object?
    public abstract void HighlightControl(); // what happens when we pickup this object?

    public GameObject highlightObject; // our highlight object
    public bool isHighlighted;
    public PlayerTransformController player;

    // our custom block update step for checks
    public abstract void BlockUpdate();
    public abstract void BlockStart();

    private void Start()
    {
        // get our player
        if (player == null)
        {
            player = FindObjectOfType<PlayerTransformController>();
        }
        // start our block step
        StartCoroutine(BlockStep());
        // run block start
        BlockStart();
    }

    IEnumerator BlockStep()
    {
        yield return new WaitForSeconds(0.05f);
        BlockUpdate();
        StartCoroutine(BlockStep());
    }
}
