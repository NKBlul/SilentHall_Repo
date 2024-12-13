using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLock : MonoBehaviour, IInteractable
{
    public string password;
    public List<GameObject> gears = new List<GameObject>();
    int rotateAmount = 36;

    public string GetInteractionPrompt()
    {
        return "Press [E] to unlock";
    }

    public void OnInteract()
    {
        UIManager.instance.ActivateNumlockUI(this);
    }
}
