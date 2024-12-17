using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public string keyName;
    public string GetInteractionPrompt()
    {
        return $"Press [E] to pick up {keyName} key";
    }

    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();

        player.keys.Add(keyName);

        UIManager.instance.ChangeText(1, UIManager.instance.extraText, $"Picked up {keyName} key");
    }
}
