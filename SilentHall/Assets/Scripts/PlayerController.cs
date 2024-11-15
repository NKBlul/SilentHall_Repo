using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;

    [SerializeField] Vector3 move;
    public float walkingSpeed = 3f;
    public float runningSpeed = 5f;

    [SerializeField] Vector2 look;
    public float sensitivity = 2f;
    public float verticalRotationLimit = 80f;  // Limit vertical look to prevent flipping
    private float rotationX = 0f;  // Vertical rotation tracker

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = desiredMoveDirection * runningSpeed;
        }
        else
        {
            rb.velocity = desiredMoveDirection * walkingSpeed;
        }
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
}
