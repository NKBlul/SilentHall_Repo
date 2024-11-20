using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ODoor : MonoBehaviour, IInteractable
{
    bool isOpen = false;
    [SerializeField] Transform hinge;
    [SerializeField] float rotationSpeed = 2f; // Speed of rotation
    [SerializeField] Vector3 openRotation = new Vector3(0, 135, 0); // Target rotation when open
    [SerializeField] Vector3 closedRotation = new Vector3(0, 0, 0); // Target rotation when closed

    private Coroutine doorCoroutine;
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
            doorCoroutine = StartCoroutine(RotateDoor(closedRotation));
        }
        else
        {
            doorCoroutine = StartCoroutine(RotateDoor(openRotation));
        }

        isOpen = !isOpen;
    }

    IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRotation = hinge.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            hinge.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, time);
            yield return null;
        }

        hinge.transform.localRotation = endRotation; // Snap to exact final rotation
    }
}
