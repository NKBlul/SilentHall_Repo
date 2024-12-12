using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IInteractable
{
    public PaperData paperData;

    public string GetInteractionPrompt()
    {
        return $"Press [E] to read the paper";
    }

    public void OnInteract()
    {
        
    }
}
