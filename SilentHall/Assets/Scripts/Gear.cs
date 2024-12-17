using UnityEngine;

public class Gear : MonoBehaviour
{
    float rotateAmount = 36;
    public int gearIndex = 0;

    private void OnEnable()
    {
        transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            OnClick(rotateAmount, 1);
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            OnClick(-rotateAmount, -1);
        }
    }

    void OnClick(float rotationAngle, int gearChange)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            Debug.Log($"{gameObject.name} was clicked!");
            Rotate(rotationAngle, gearChange);
        }
    }

    private void Rotate(float rotationAngle, int gearChange)
    {
        transform.Rotate(Vector3.up, rotationAngle, Space.Self);

        gearIndex += gearChange;

        // Wrap gearIndex to stay within valid range (0-9)
        if (gearIndex > 9)
        {
            gearIndex = 0;
        }
        else if (gearIndex < 0)
        {
            gearIndex = 9;
        }

        UINumLock uiNumLock = GetComponentInParent<UINumLock>();
        uiNumLock?.GetCombination(); // Call GetCombination if UINumLock is not null
    }
}
