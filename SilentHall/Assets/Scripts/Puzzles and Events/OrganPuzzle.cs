using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganPuzzle : MonoBehaviour, IInteractable
{
    public string requiredOrgan;
    public bool haveOrgan = false;
    public bool organPlaced = false;
    public Transform organPos;
    public string GetInteractionPrompt(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();
        if (player.pickable.Contains(requiredOrgan))
        {
            haveOrgan = true;
        }

        if (haveOrgan) 
        {
            return $"Press [E] to place {requiredOrgan}";
        }
        return $"Find the needed organ";
    }

    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();
        PuzzleChecker checker = GetComponentInParent<PuzzleChecker>();
        if (haveOrgan && !organPlaced)
        {
            GameObject organ = Instantiate(PrefabManager.instance.GetOrganPrefab(requiredOrgan), organPos);
            player.pickable.Remove(requiredOrgan);
            checker.currentOrgan++;
            checker.CheckOrganPuzzle();
        }
    }
}
