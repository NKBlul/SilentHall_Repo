using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour
{
    int rotateAmount = 36;
    int gearIndex = 0;

    private void OnEnable()
    {
        transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit this object
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log($"{gameObject.name} was clicked!");
                    RotateGear();
                }
            }
        }
    }

    void RotateGear()
    {
        transform.Rotate(Vector3.up, 36f, Space.Self);  // Rotate 36 degrees around the local Y-axis

        gearIndex++;

        if (gearIndex + 1 > 9)
        {
            gearIndex = 0;
        }
    }
}
