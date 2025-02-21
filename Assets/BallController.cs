using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        // Get camera's forward and right directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Movement direction relative to the camera
        Vector3 move = (forward * vertical + right * horizontal).normalized;

        // Apply movement speed
        moveDirection = move * moveSpeed;

        // Gravity Handling
        if (controller.isGrounded)
        {
            verticalVelocity = -0.1f; // Small value to keep grounded check accurate

            // Jumping
            if (Input.GetButtonDown("Jump")) // Default mapped to Space
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; // Apply gravity when in the air
        }

        // Apply vertical movement (jumping/gravity)
        moveDirection.y = verticalVelocity;

        // Move the player
        controller.Move(moveDirection * Time.deltaTime);

        // Rotate player towards movement direction
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
