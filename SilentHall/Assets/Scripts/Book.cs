using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt()
    {
        return $"Press [E] to read the book";
    }


    public void OnInteract()
    {
        
    }
}
