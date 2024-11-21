using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    [SerializeField] float rotationSpeed = 2f; // Speed of rotation
    [SerializeField] Vector3 openLeftRotation = new Vector3(0, 90, 0); // Target rotation when open
    [SerializeField] Vector3 openRightRotation = new Vector3(0, -90, 0); // Target rotation when open
    [SerializeField] Vector3 closedRotation = new Vector3(0, 0, 0); // Target rotation when closed
    private Coroutine doorCoroutine;
    bool isOpen = false;

    public string GetInteractionPrompt()
    {
        if (!isOpen)
        {
            return $"Press [E] to open door";
        }

        return $"Press [E] to close door";
    }

    public void OnInteract()
    {
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);

        if (isOpen)
        {
            doorCoroutine = StartCoroutine(RotateDoor(closedRotation, closedRotation));
        }
        else
        {
            doorCoroutine = StartCoroutine(RotateDoor(openLeftRotation, openRightRotation));
        }

        isOpen = !isOpen;
    }

    IEnumerator RotateDoor(Vector3 targetRotation1, Vector3 targetRotation2)
    {
        Quaternion startLeftRotation = leftDoor.localRotation;
        Quaternion startRightRotation = rightDoor.localRotation;
        Quaternion endRotation1 = Quaternion.Euler(targetRotation1);
        Quaternion endRotation2 = Quaternion.Euler(targetRotation2);

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            leftDoor.localRotation = Quaternion.Lerp(startLeftRotation, endRotation1, time);
            rightDoor.localRotation = Quaternion.Lerp(startRightRotation, endRotation2, time);
            yield return null;
        }

        leftDoor.localRotation = endRotation1; // Snap to exact final rotation
        rightDoor.localRotation = endRotation2; // Snap to exact final rotation
    }
}
