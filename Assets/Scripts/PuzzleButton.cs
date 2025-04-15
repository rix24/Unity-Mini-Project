using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public int id;
    private bool canPress = false; // Default to false
    private GameObject chestText;
    private Animator chestAnimator;
    private AudioSource chestAudio;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
        chestText = transform.GetChild(0).gameObject;
        chestText.SetActive(false); // Ensure the signpost text is hidden initially
        chestAudio = GetComponent<AudioSource>();
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
            chestText.SetActive(true); // Show the signpost text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPress = false; // Disallow interaction
            chestText.SetActive(false); // Hide the signpost text
        }
    }

    private void HandleButtonPress()
    {
        // Call the GatePuzzle instance with the specific button ID
        chestAnimator.SetTrigger("openChest");
        chestAudio.Play();
        GatePuzzle.instance.OnButtonPressed(id);
    }
}