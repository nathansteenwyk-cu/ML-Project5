using UnityEngine;

public class RollerBall : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float friction = 0.95f; // Friction factor
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed;
        rb.AddForce(movement, ForceMode.Force);
        ApplyFriction();
    }

    void ApplyFriction()
    {

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * friction, rb.linearVelocity.y, rb.linearVelocity.z * friction);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}