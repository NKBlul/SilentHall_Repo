using System.Collections;
using UnityEngine;

public class DDoor : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [SerializeField]Vector3 openLeftDoorRot = new Vector3(0, -90, 0);
    [SerializeField]Vector3 openRightDoorRot = new Vector3(0, 90, 0);

    public bool useKey = false;
    public string requiredKey;
    public bool useNumLock = false;
    float openSpeed = 2f;
    [SerializeField] bool isLocked = true;

    public string GetInteractionPrompt(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();
        if (isLocked)
        {
            if (useNumLock)
            {
                return $"Unlock the lock";
            }
            if (useKey)
            {
                if (player.pickable.Contains(requiredKey))
                {
                    return $"Press [E] to unlock door";
                }
                return $"Door is Locked, Find a Key";
            }
            return "You shouldn't be getting this text";
        }
        else
        {
            return $"Press [E] to open door";
        }
    }


    public void OnInteract(GameObject trigger)
    {
        PlayerController player = trigger.GetComponent<PlayerController>();
        NumLock numLock = GetComponentInChildren<NumLock>();
        if (isLocked)
        {
            if (useNumLock)
            {
                //numLock.OnInteract(trigger);
            }
            if (useKey)
            {
                if (player.pickable.Contains(requiredKey))
                {
                    UnlockAndOpen();
                }
            }
        }
        else
        {
            OpenDoors();
        } 
    }

    public void UnlockAndOpen()
    {
        isLocked = false; // Mark the door as unlocked.
        OpenDoors(); // Open the doors.
    }

    void OpenDoors()
    {
        StartCoroutine(OpenDoor(leftDoor, openLeftDoorRot));
        StartCoroutine(OpenDoor(rightDoor, openRightDoorRot));
        gameObject.layer = default;
    }

    IEnumerator OpenDoor(GameObject door, Vector3 endRot)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = door.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(endRot);

        while (elapsedTime < openSpeed)
        {
            door.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.transform.localRotation = endRotation; // Ensure final rotation is correct.
    }
}
