using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IInteractable
{
    public PaperData paperData;
    public bool isReading;

    public string GetInteractionPrompt()
    {
        //if (!isReading)
        //{
        //    return $"Press [E] to read the paper";
        //}
        //return "";

        return $"Press [E] to read the paper";
    }

    public void OnInteract(GameObject trigger)
    {
        //if (!isReading)
        //{
        //    isReading = true;
        //    UIManager.instance.ReadPaper(paperData);
        //}
        //
        //isReading = true;
        UIManager.instance.ReadPaper(paperData);
    }
}
