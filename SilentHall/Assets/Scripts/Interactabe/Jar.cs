using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IInteractable
{
    public GameObject jarContent;
    public string contentName;
    bool isTaken = false;

    public string GetInteractionPrompt(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>(); // Find the player controller

        if (!isTaken) 
        {
            if (player.leftHand.childCount > 0)
            {
                return $"Your hand is full";
            }
            return $"Press [E] to pick up {jarContent.name}";
        }
        return "";
    }

    public void OnInteract(GameObject trigger)
    {
        if (!isTaken) 
        {
            PlayerController player = trigger.GetComponent<PlayerController>();

            if (!player.haveLeftItem)
            {
                PrefabManager.instance.InstantiatePrefab(jarContent.name, player.leftHand);
                player.haveLeftItem = true;
                isTaken = true;
                player.pickable.Add(jarContent.name);
                jarContent.SetActive(false);
            }
        }
    }

    public void ResetJar()
    {
        if (isTaken)
        {
            jarContent.SetActive(true);
            isTaken = false;
        }
    }
}
