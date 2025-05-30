using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider boxCollider;


    [Header("Movement: ")]
    [SerializeField] Vector3 move;
    public float walkingSpeed = 3f;
    public float runningSpeed = 5f;
    bool isRunning = false;
    public bool canMove = true;

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
    [SerializeField] float raycastDist = 5f;
    [SerializeField] float raycastDist2 = 15f;
    private float raycastTimer = 0f;
    private const float raycastInterval = 0.15f; // Raycast interval
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask eventLayer;
    private IInteractable currentInteractable; // Cache the last interactable object

    [Header("Pickable: ")]
    public Transform rightHand;
    public Transform leftHand;
    public bool haveRightItem = false;
    public bool haveLeftItem = false;
    public List<string> pickable = new List<string>();

    [Header("Crouching")]
    bool isCrouching;
    float crouchSpeed = 1.5f;
    public Vector3 originalColliderSize;
    public Vector3 crouchColliderSize;
    public Vector3 originalColliderPos;
    public Vector3 crouchColliderPos;
    public float crouchTransitionSpeed = 5f;
    public Vector3 cameraDefaultPosition;
    public Vector3 cameraCrouchPosition;
    void Start()
    {
        currentStamina = maxStamina;
        UIManager.instance.UpdateStaminaBar(currentStamina, maxStamina);
        UIManager.instance.ActivateStamina(false);
        cameraDefaultPosition = cam.transform.localPosition;
        originalColliderSize = boxCollider.size;
        originalColliderPos = boxCollider.center;
    }

    void Update()
    {
        if (!canMove) return;

        GetInput();
        raycastTimer += Time.deltaTime;
        if (raycastTimer >= raycastInterval)
        {
            CastRayCast();
            CastEventRayCast();
            raycastTimer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteract(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && haveRightItem)
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Q) && haveLeftItem)
        {
            Drop();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.instance.TogglePlayerMovement(false);
            GameManager.instance.ShowCursor();
            UIManager.instance.pianoUI.SetActive(true);
        }
        RegenStamina();
        UpdateAnimator();
        Crouch();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Walk();
            Look();
        }
    }

    void UpdateAnimator()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0);
            //animator.SetBool("IsRunning", false);
            animator.SetBool("IsCrouching", false);
            return;
        }
        // Calculate speed based on the current movement state
        float speed = move.magnitude * (isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed));
        animator.SetFloat("Speed", speed);        // For blend tree
        //animator.SetBool("IsRunning", isRunning); // For additional animations
        animator.SetBool("IsCrouching", isCrouching); // For crouch state
        if (isCrouching)
        {
            if (speed > 0.1f) // Crouch walking
            {
                animator.SetFloat("CrouchBlend", 1); // Blend to CrouchWalk
            }
            else // Idle crouch
            {
                animator.SetFloat("CrouchBlend", 0); // Blend to StandToCrouch
            }
        }
        else
        {
            animator.SetFloat("CrouchBlend", 2); // Blend to CrouchToStand
        }
    }

    void GetInput()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        look = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensitivity;
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            boxCollider.center = crouchColliderPos;
            boxCollider.size = crouchColliderSize;
            StopAllCoroutines(); // Stop any ongoing camera movement coroutines
            StartCoroutine(TransitionCameraPosition(cameraCrouchPosition));
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            boxCollider.center = originalColliderPos;
            boxCollider.size = originalColliderSize;
            StopAllCoroutines(); // Stop any ongoing camera movement coroutines
            StartCoroutine(TransitionCameraPosition(cameraDefaultPosition));
        }

        //// Smoothly transition the camera position
        //Vector3 targetPosition = isCrouching ? cameraCrouchPosition : cameraDefaultPosition;
        //cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPosition, Time.deltaTime * crouchTransitionSpeed);
    }

    private IEnumerator TransitionCameraPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = cam.transform.localPosition;
        float elapsedTime = 0f;
        float transitionDuration = 0.5f; // Adjust this value for transition speed

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            cam.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        // Ensure the final position is exactly the target position
        cam.transform.localPosition = targetPosition;
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
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isCrouching && move.magnitude > 0.01f)
        {
            isRunning = true;
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            UIManager.instance.UpdateStaminaBar(currentStamina, maxStamina);
            //rb.velocity = desiredMoveDirection * runningSpeed;
            rb.MovePosition(rb.position + desiredMoveDirection * runningSpeed * Time.fixedDeltaTime);
            staminaRegenTimer = 0f;
            //Debug.Log($"Current Stamina: {currentStamina}");
        }
        else if (isCrouching)
        {
            rb.MovePosition(rb.position + desiredMoveDirection * (crouchSpeed) * Time.fixedDeltaTime);
        }
        else if (currentStamina <= 0) // Stamina depleted
        {
            isRunning = false;

            // Slow down to a crawl or stop entirely
            rb.MovePosition(rb.position + desiredMoveDirection * (walkingSpeed * 0.5f) * Time.fixedDeltaTime);

            //Debug.Log("Stamina depleted. Slowing down...");
        }
        else
        {
            isRunning = false;
            //rb.velocity = desiredMoveDirection * walkingSpeed;
            rb.MovePosition(rb.position + desiredMoveDirection * walkingSpeed * Time.fixedDeltaTime);
            UIManager.instance.ActivateStamina(false);
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

    void CastRayCast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDist, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    currentInteractable = interactable; // Update the cached interactable
                    UIManager.instance.ChangeText(UIManager.instance.interactableText, interactable.GetInteractionPrompt(gameObject));
                }
                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable = null;
            UIManager.instance.ClearText(UIManager.instance.interactableText);
        }

        Debug.DrawRay(ray.origin, ray.direction * raycastDist, Color.green, 1f);
    }

    void CastEventRayCast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDist2, eventLayer))
        {
            Debug.Log($"{hit.collider.gameObject} EVENT");
            IEvent events = hit.collider.GetComponent<IEvent>();

            if (events != null)
            {
                events.TriggerEvent();
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * raycastDist2, Color.gray, 1f);

    }

    public void Pickup(GameObject obj, Transform hand)
    {
        haveRightItem = true;
        obj.transform.SetParent(hand);

        currentInteractable = null;
        UIManager.instance.ClearText(UIManager.instance.interactableText);
    }

    void UseItem()
    {
        rightHand.GetComponentInChildren<IUseable>().Use();
    }

    void Drop()
    {
        leftHand.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Interactable");
        leftHand.GetComponentInChildren<Rigidbody>().isKinematic = false;
        leftHand.DetachChildren();
        haveLeftItem = false;
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
                UIManager.instance.UpdateStaminaBar(currentStamina, maxStamina);
            }
        }
    }
}
