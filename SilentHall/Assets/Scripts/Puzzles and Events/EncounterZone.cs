using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EncounterZone : MonoBehaviour
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
        }
    }
}
