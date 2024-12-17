using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SlidingDoor : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    Vector3 closeDoorPos;
    Vector3 openDoorPos;

    float openSpeed = 2f;
    bool isOpen = false;
    [SerializeField] bool isLocked = true;
    bool haveKey = false;
    string requiredKey;

    void Start()
    {
        closeDoorPos = leftDoor.transform.localPosition;
        openDoorPos = new Vector3(0, closeDoorPos.y, closeDoorPos.z);
    }

    public string GetInteractionPrompt()
    {
        if (isLocked)
        {
            return $"Door is locked, find a key";
        }
        else
        {
            if (!isOpen)
            {
                return $"Press [E] to open door";
            }
            return $"Press [E] to close door";
        }
    }


    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();

        if (isLocked) 
        { 
            isLocked = false;
        }
        else
        {
            if (!isOpen)
            {
                StartCoroutine(SlideDoor(leftDoor, closeDoorPos, openDoorPos));
            }
            else
            {
                StartCoroutine(SlideDoor(leftDoor, openDoorPos, closeDoorPos));
            }
            isOpen = !isOpen;
        }

        if (isLocked)
        {
            if (!player.keys.Contains(requiredKey))
            {

            }
        }
    }

    bool HaveKey(string keyRequired)
    {
        if (requiredKey == keyRequired)
        {
            haveKey = true;
        }
        haveKey = false;
        return haveKey;
    }

    IEnumerator SlideDoor(GameObject door, Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < openSpeed)
        {
            door.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.transform.localPosition = endPos;
    }
}
