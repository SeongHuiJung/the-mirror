using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionEvent : MonoBehaviour
{

    public abstract void Interact(GameObject player);
    public abstract void Approach(GameObject player);
    public abstract void StopInteract(GameObject player);
    
}
