using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzle : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt(GameObject trigger)
    {
        return "Press E to interact with piano puzzle";
    }

    public void OnInteract(GameObject trigger)
    {
        GameManager.instance.TogglePlayerMovement(false);
        GameManager.instance.ShowCursor();
        UIManager.instance.pianoUI.SetActive(true);
    }
}
