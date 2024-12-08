using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterZone : MonoBehaviour
{
    public string eventName;
    public bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            EncounterManager.instance.TriggerEvent(eventName);
            hasTriggered = true;
        }
    }
}
