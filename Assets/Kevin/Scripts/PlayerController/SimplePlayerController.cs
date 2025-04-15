using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float verticalInput;
    private float horizontalInput;
    public float speed = 2f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f)
            verticalInput = 0f;
        else if (Mathf.Abs(verticalInput) > 0.1f)
            horizontalInput = 0f;

        Vector3 move = new Vector3(horizontalInput, verticalInput, 0f);

        transform.Translate(move * Time.deltaTime * speed);
        // playerRb.velocity = move * speed;
    }

}