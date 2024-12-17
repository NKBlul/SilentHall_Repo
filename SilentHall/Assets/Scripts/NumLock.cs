using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLock : MonoBehaviour, IInteractable
{
    public string password;
    public List<GameObject> gears = new List<GameObject>();
    public bool isInteracting = false;

    public string GetInteractionPrompt(GameObject trigger)
    {
        if (!isInteracting)
        {
            return "Press [E] to unlock";
        }
        return " ";
    }

    public void OnInteract(GameObject trigger)
    {
        UIManager.instance.ActivateNumlockUI(this);
        isInteracting = true;
    }
}
