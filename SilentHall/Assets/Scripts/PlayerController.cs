using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;

    [Header("Movement: ")]
    [SerializeField] Vector3 move;
    public float walkingSpeed = 3f;
    public float runningSpeed = 5f;
    bool isRunning = false;

    [Header("Stamina System: ")]
    public float currentStamina;
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 5f;
    public float staminaRegenDelay = 2f;
    public float staminaRegenTimer = 0f;

    [Header("Look: ")]
    [SerializeField] Vector2 look;
    public float sensitivity = 2f;
    public float verticalRotationLimit = 80f;  // Limit vertical look to prevent flipping
    private float rotationX = 0f;  // Vertical rotation tracker

    [Header("Raycast: ")]
    [SerializeField] Transform item;
    [SerializeField] float raycastDist = 7f;
    [SerializeField] LayerMask interactableLayer;
    bool haveItem = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = maxStamina;
    }

    void Update()
    {
        GetInput();
        CastRayCast();
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (!haveItem)
        //    {
        //        //RaycastCheck();
        //    }
        //    else
        //    {
        //        UseItem();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.F) && haveItem)
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Q) && haveItem)
        {
            Drop();
        }

        RegenStamina();
    }

    private void FixedUpdate()
    {
        Walk();
        Look();
    }

    void GetInput()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensitivity;
    }

    void Walk()
    {
        // Calculate the forward and right directions relative to the camera
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // We want to ignore the Y-axis (up/down) to prevent the player from moving vertically
        forward.y = 0f;
        right.y = 0f;

        // Normalize the vectors to make sure the movement is smooth
        forward = forward.normalized;
        right = right.normalized;

        // Determine movement direction based on input and camera orientation
        Vector3 desiredMoveDirection = forward * move.z + right * move.x;

        // Apply movement speed
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isRunning = true;
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            //rb.velocity = desiredMoveDirection * runningSpeed;
            rb.MovePosition(rb.position + desiredMoveDirection * runningSpeed * Time.fixedDeltaTime);
            staminaRegenTimer = 0f;
            Debug.Log($"Current Stamina: {currentStamina}");
        }
        else if (currentStamina <= 0) // Stamina depleted
        {
            isRunning = false;

            // Slow down to a crawl or stop entirely
            rb.MovePosition(rb.position + desiredMoveDirection * (walkingSpeed * 0.5f) * Time.fixedDeltaTime);

            Debug.Log("Stamina depleted. Slowing down...");
        }
        else
        {
            isRunning = false;
            //rb.velocity = desiredMoveDirection * walkingSpeed;
            rb.MovePosition(rb.position + desiredMoveDirection * walkingSpeed * Time.fixedDeltaTime);
        }
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, isRunning ? 75 : 60, Time.deltaTime * 10f);
    }

    void Look()
    {
        // Horizontal look (rotate the player object around the Y-axis)
        transform.Rotate(Vector3.up * look.x);  // Rotate the player horizontally based on Mouse X input

        // Vertical look (rotate the camera around the X-axis)
        rotationX -= look.y;  // Invert the Y-axis to make the camera look up when moving mouse up
        rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);  // Clamp the vertical rotation to avoid flipping the camera
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);  // Apply the vertical look to the camera's local rotation
    }

    /*void RaycastCheck()
    {
        // Create a ray from the camera's position and forward direction
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        // Store information about what the ray hit
        RaycastHit hit;

        // Check if the ray hit something
        if (Physics.Raycast(ray, out hit, raycastDist))
        {
            // Log the name of the object hit
            Debug.Log("Hit object: " + hit.collider.name);

            //// Optionally, apply logic to the hit object
            if (hit.collider.CompareTag("Pickable") && !haveItem)
            {
                Debug.Log("Interacted with: " + hit.collider.name);
                Pickup(hit.collider.gameObject);
            }
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Interacted with: " + hit.collider.name);
                hit.collider.gameObject.GetComponent<IInteractable>().OnInteract();
            }

            // Draw a debug ray in the editor for visualization
            Debug.DrawRay(ray.origin, ray.direction * raycastDist, Color.green, 1f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * raycastDist, Color.red, 1f);
            Debug.Log("No hit detected.");
        }
    }*/

    void CastRayCast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDist))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                    hit.collider.gameObject.GetComponent<IInteractable>().OnInteract();
                }
                if (hit.collider.gameObject.CompareTag("Pickable"))
                {
                    Pickup(hit.collider.gameObject);
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * raycastDist, Color.green);
    }

    void Pickup(GameObject obj)
    {
        haveItem = true;
        obj.transform.SetParent(item);
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    void UseItem()
    {
        item.GetComponentInChildren<IUseable>().Use();
    }

    void Drop()
    {
        //item.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponentInChildren<Rigidbody>().isKinematic = false;
        item.DetachChildren();
        haveItem = false;
    }

    void RegenStamina()
    {
        if (!isRunning && currentStamina < maxStamina)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= staminaRegenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
            Debug.Log($"Current Stamina: {currentStamina}");
        }
    }
}
