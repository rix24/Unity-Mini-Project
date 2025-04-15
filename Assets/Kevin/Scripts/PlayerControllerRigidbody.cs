using UnityEngine;

public class PlayerControllerRigidbody : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isClamped = true;
    public float moveSpeed = 5f;
    public Vector2 xRange = new Vector2(-5f, 5f);
    public Vector2 yRange = new Vector2(-3f, 3f);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveX) > 0.1f)
            moveY = 0f;
        else if (Mathf.Abs(moveY) > 0.1f)
            moveX = 0f;
        
        spriteRenderer.flipX = moveX < 0;

        animator.SetFloat("moveRight", moveX);
        animator.SetFloat("moveUp", moveY);

        Vector2 move = new Vector2(moveX, moveY) * moveSpeed;
        rb.linearVelocity = move;

        if (isClamped)
        {
            Vector2 clamped = ClampPosition(rb.position);
            rb.position = clamped;
        }
    }

    Vector2 ClampPosition(Vector2 targetPos)
    {
        float clampedX = Mathf.Clamp(targetPos.x, xRange.x, xRange.y);
        float clampedY = Mathf.Clamp(targetPos.y, yRange.x, yRange.y);
        return new Vector2(clampedX, clampedY);
    }
}