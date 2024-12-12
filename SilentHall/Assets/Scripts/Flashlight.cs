using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Flashlight : MonoBehaviour, IUseable, IInteractable
{
    [SerializeField] Light spotLight;
    bool isOn;

    public string GetInteractionPrompt()
    {
        return $"Press [E] to pick-up";
    }

    public void OnInteract()
    {
        PlayerController player = FindObjectOfType<PlayerController>(); // Find the player controller
        if (player != null)
        {
            player.Pickup(gameObject); // Call the Pickup method on the player and pass the flashlight as the object to pick up
            GetComponent<Rigidbody>().isKinematic = true;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            //gameObject.layer = default;
            gameObject.layer = LayerMask.NameToLayer("Default");

            UIManager.instance.ChangeText(1f, UIManager.instance.extraText, $"Press [F] to toggle flashlight");
            UIManager.instance.flashlight.SetActive(true);
        }
    }

    public void Use()
    {
        if (isOn) 
        {
            spotLight.enabled = false;
            isOn = false;
        }
        else
        {
            spotLight.enabled = true;
            isOn = true;
        }
    }
}
