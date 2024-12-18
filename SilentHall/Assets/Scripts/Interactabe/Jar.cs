using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IInteractable
{
    public GameObject jarContent;
    bool isTaken = false;

    public string GetInteractionPrompt(GameObject trigger)
    {
        if (!isTaken) 
        {
            return $"Press [E] to pick up {jarContent.name}";
        }
        return "";
    }

    public void OnInteract(GameObject trigger)
    {
        if (!isTaken) 
        {
            PlayerController player = trigger.GetComponent<PlayerController>();
            isTaken = true;
            player.pickable.Add(jarContent.name);
            Destroy(jarContent);
        }
    }
}
