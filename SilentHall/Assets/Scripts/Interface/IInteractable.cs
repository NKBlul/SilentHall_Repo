using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetInteractionPrompt(GameObject trigger);
    void OnInteract(GameObject trigger);
}
