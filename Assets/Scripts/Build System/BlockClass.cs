using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockClass : MonoBehaviour
{
    public string blockName; // our block's name
    public abstract void Interact(); // what happens when we interact with this object?
    public abstract void Pickup(); // what happens when we pickup this object?
    public abstract void Place(); // what happens when we place this object?
    public abstract void Place(Transform parent); // what happens when we place this object?
    public abstract void Place(Transform parent, Vector3 position); // what happens when we place this object?
    public abstract void HighlightControl(); // what happens when we highlight this object?

    public GameObject highlightObject; // our highlight object
    public GameObject faceParent; // our face parent
    public bool isHighlighted, isHeld;
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
