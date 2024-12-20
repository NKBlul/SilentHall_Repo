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
        PuzzleChecker checker = GetComponentInParent<PuzzleChecker>();
        //if (!organPlaced)
        //{
        //    GameObject organ = Instantiate(PrefabManager.instance.GetOrganPrefab(requiredOrgan), organPos);
        //
        //    player.pickable.Remove(requiredOrgan);
        //    checker.CheckOrganPuzzle();
        //}
        if (player.haveLeftItem && !organPlaced)
        {
            organPlaced = true;
            UIManager.instance.ClearText(UIManager.instance.interactableText);
            organName = player.leftHand.GetChild(0).name;
            string newName = organName.Substring(0, organName.Length - 7);
            GameObject organ = Instantiate(PrefabManager.instance.GetOrganPrefab(newName), organPos);
            player.pickable.Remove(newName);
            player.haveLeftItem = false;
            Destroy(player.leftHand.GetChild(0).gameObject);
        }
    }
}
