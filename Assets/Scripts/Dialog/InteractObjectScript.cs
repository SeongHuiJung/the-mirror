using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjectScript : MonoBehaviour
{
    [SerializeField] private int id;

    public int GetId()
    {
        return id;
    }
}
