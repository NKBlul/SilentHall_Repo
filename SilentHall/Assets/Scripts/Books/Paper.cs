using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IInteractable
{
    public PaperData paperData;
    public bool isReading;

    public string GetInteractionPrompt()
    {
        if (!isReading)
        {
            return $"Press [E] to read the paper";
        }
        return "";
    }

    public void OnInteract()
    {
        if (!isReading)
        {
            //UIManager.instance.paper.SetActive(true);
            //UIManager.instance.ChangeText(UIManager.instance.paperText, paperData.paperText);
        }
    }
}
