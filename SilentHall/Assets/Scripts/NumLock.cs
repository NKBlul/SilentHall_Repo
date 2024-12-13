using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLock : MonoBehaviour, IInteractable
{
    public string password;
    public List<GameObject> gears = new List<GameObject>();
    int rotateAmount = 36;
    public bool isInteracting = false;

    public string GetInteractionPrompt()
    {
        if (!isInteracting)
        {
            return "Press [E] to unlock";
        }
        return " ";
    }

    public void OnInteract()
    {
        UIManager.instance.ActivateNumlockUI(this);
        isInteracting = true;
    }
}
