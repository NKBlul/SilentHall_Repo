using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEncounterZone : MonoBehaviour
{
    public string eventName;
    public bool hasTriggered = false;
    [SerializeField] public GameObject triggers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            EventManager.instance.TriggerEvent(eventName, triggers);
            Destroy(gameObject);
        }
    }
}
