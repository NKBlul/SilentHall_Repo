using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEvent : MonoBehaviour, IEvent
{
    public string eventName;
    public bool hasTriggered = false;
    public GameObject triggers;

    public void TriggerEvent()
    {
        if (!hasTriggered) 
        {
            EventManager.instance.TriggerEvent(eventName, triggers);
            hasTriggered = true;
        }
    }
}
