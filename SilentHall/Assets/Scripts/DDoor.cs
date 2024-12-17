using System.Collections;
using UnityEngine;

public class DDoor : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [SerializeField]Vector3 openLeftDoorRot = new Vector3(0, -90, 0);
    [SerializeField]Vector3 openRightDoorRot = new Vector3(0, 90, 0);

    float openSpeed = 2f;
    //bool isOpen = false;
    [SerializeField] bool isLocked = true;

    public string GetInteractionPrompt(GameObject trigger)
    {
        if (isLocked)
        {
            return $"Door is Locked, Find a Key";
        }
        else
        {
            //if (!isOpen)
            //{
            //    return $"Press [E] to open door";
            //}
            //return $"Press [E] to close door";
            return $"Press [E] to open door";
        }
    }


    public void OnInteract(GameObject trigger)
    {
        if (isLocked)
        {
            isLocked = false;
        }
        else
        {
            //if (!isOpen)
            //{
            //
            //}
            //else
            //{
            //
            //}
            //isOpen = !isOpen;
            Debug.Log($"Opening door");
            StartCoroutine(OpenDoor(leftDoor, openLeftDoorRot));
            StartCoroutine(OpenDoor(rightDoor, openRightDoorRot));
            gameObject.layer = default;
        } 
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
