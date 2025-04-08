using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private bool isJumpPressed = false;

    private float moveInput;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator anim;


    // Animation states
    private string currentState;
    private const string PLAYER_IDLE = "player_idle";
    private const string PLAYER_RUN = "player_run";
    private const string PLAYER_FALL = "player_fall";


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Horizontal Movement
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Face right
        }
        else if (moveInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Face left
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if the player is falling
        if (!isGrounded && rb.linearVelocity.y < 0f) // Only fall if not grounded and moving downward
        {

            ChangeAnimationState(PLAYER_FALL);
        }

        if (isGrounded)
        {
            // Update animation state
            if (moveInput != 0f)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }

            if (isJumpPressed)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumpPressed = false;
            }
        }
    }


    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
