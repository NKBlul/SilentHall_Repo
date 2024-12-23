using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganPuzzle : MonoBehaviour, IInteractable
{
    public string requiredOrgan;
    string organName;
    public bool organPlaced = false;
    public Transform organPos;

    public string GetInteractionPrompt(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();
        if (!organPlaced) 
        {
            if (player.haveLeftItem)
            {
                organName = player.leftHand.GetChild(0).name;
                string newName = organName.Substring(0, organName.Length - 7);
                return $"Press [E] to place the {newName}";
            }
            return $"Find the needed organ";
        }
        return " ";
    }

    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();

        if (player.haveLeftItem && !organPlaced)
        {
            organName = player.leftHand.GetChild(0).name;
            string placedOrgan = organName.Substring(0, organName.Length - 7);

            if (placedOrgan == requiredOrgan)
            {
                PuzzleManager.instance.correctOrgan++;
            }

            GameObject organ = PrefabManager.instance.InstantiatePrefab(placedOrgan, organPos);
            PuzzleManager.instance.organPlaced++;
            organPlaced = true;

            UIManager.instance.ClearText(UIManager.instance.interactableText);

            player.pickable.Remove(placedOrgan);
            player.haveLeftItem = false;
            Destroy(player.leftHand.GetChild(0).gameObject);
            PuzzleManager.instance.CheckOrganPuzzle();
        }
    }
}
