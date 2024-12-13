using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINumLock : MonoBehaviour
{
    public string password;
    public string currentRotation;
    public LayerMask numLock;

    private bool isRotating = false;
    private Vector3 lastMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRotating = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }

        if (Input.GetMouseButton(0) && isRotating)
        {
            RotateNumLock();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
    }

    private void OnMouseClick()
    {
        Debug.Log($"{gameObject.name} was clicked!");
        // Add your custom logic here
    }

    public void RotateNumLock()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        // Rotate the object based on the horizontal mouse movement
        float horizontalRotation = mouseDelta.x * 0.1f; // Horizontal movement for Y-axis rotation
        float verticalRotation = mouseDelta.y * 0.1f;   // Vertical movement for X-axis rotation

        transform.Rotate(Vector3.up, horizontalRotation, Space.World); // Horizontal rotation
        transform.Rotate(Vector3.right, verticalRotation, Space.World); // Vertical rotation

        Debug.Log("Rotating gear...");
        UpdateCurrentRotation();
    }

    private void UpdateCurrentRotation()
    {
        // Update current rotation logic (convert the rotation to some value)
        float rotationY = transform.eulerAngles.y % 360;
        currentRotation = Mathf.RoundToInt(rotationY / 36).ToString(); // Assuming each rotation step is 36 degrees
        Debug.Log($"Current rotation: {currentRotation}");
    }

    public void RotateGear()
    {
        Debug.Log("HELO");
    }
    
    public bool CheckCombination()
    {
        return password == currentRotation;
    }
}
