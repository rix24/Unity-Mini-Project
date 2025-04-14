using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int id;
    private bool canPress = false; // Default to false
    private GameObject signpostText;

    private void Start()
    {
        signpostText = transform.GetChild(0).gameObject;
        signpostText.SetActive(false); // Ensure the signpost text is hidden initially
    }

    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            HandleButtonPress();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = true; // Allow interaction
            signpostText.SetActive(true); // Show the signpost text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = false; // Disallow interaction
            signpostText.SetActive(false); // Hide the signpost text
        }
    }

    private void HandleButtonPress()
    {
        // Call the GatePuzzle instance with the specific button ID
        GatePuzzle.instance.OnButtonPressed(id);
    }
}