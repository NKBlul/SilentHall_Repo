using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IInteractable
{
    public GameObject jarContent;
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
                GameObject organ = Instantiate(PrefabManager.instance.GetPrefab(jarContent.name), player.leftHand);
                //UIManager.instance.ChangeText(1.5f, UIManager.instance.extraText, $"Press [Q] to drop item");
                player.haveLeftItem = true;
                isTaken = true;
                player.pickable.Add(jarContent.name);
                Destroy(jarContent);
            }
        }
    }
}
