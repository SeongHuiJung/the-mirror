using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    public delegate void EventCallDelegate(int id);
    public EventCallDelegate eventCallDelegate;
    [SerializeField]
    public int dialogID;
    // Start is called before the first frame update
    public void InitEvent(EventCallDelegate d)
    {
        eventCallDelegate = d;
    }
    private void Start()
    {
        //event body
    }
    private void Awake()
    {
        //event body
    }
    
}