using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public string keyName;
    public string GetInteractionPrompt(GameObject trigger)
    {
        return $"Press [E] to pick up {keyName} key";
    }

    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();

        player.pickable.Add(keyName);

        UIManager.instance.ChangeText(1, UIManager.instance.extraText, $"Picked up {keyName} key");

        Destroy(gameObject);
    }
}
