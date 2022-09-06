using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObjectScript : MonoBehaviour
{
    [SerializeField] protected int id;
    [SerializeField] protected bool isDialog;

    public int GetId()
    {
        return id;
    }

    public bool GetDialog()
    {
        return isDialog;
    }

    public abstract void Interact();
}
