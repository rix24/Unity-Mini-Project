using UnityEngine;

public class PlayerControllerTranslate : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float moveSpeed = 5f;
    public Vector2 xRange = new Vector2(-5f, 5f);
    public Vector2 yRange = new Vector2(-3f, 3f);
    public bool isClamped = true;


    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
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

        Vector3 move = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;
        
        transform.position = ClampPosition(transform.position + move);
    }

    Vector3 ClampPosition(Vector3 targetPos)
    {
        float clampedX = Mathf.Clamp(targetPos.x, xRange.x, xRange.y);
        float clampedY = Mathf.Clamp(targetPos.y, yRange.x, yRange.y);
        return new Vector3(clampedX, clampedY, targetPos.z);
    }
}