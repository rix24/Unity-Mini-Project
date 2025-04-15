using System.Collections;
using TMPro;
using UnityEngine;

public class SignpostController : MonoBehaviour
{
    public GameObject gameController;
    private GatePuzzle gatePuzzle;
    private GameObject signpostTextObject;
    private TextMeshPro signpostText;
    void Start()
    {
        gatePuzzle = gameController.GetComponent<GatePuzzle>();
        signpostTextObject = transform.GetChild(0).gameObject;
        signpostText = signpostTextObject.GetComponent<TextMeshPro>();
        StartCoroutine(SetSignpostText()); // Start the coroutine to set the signpost text
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            signpostTextObject.SetActive(true); // Show the signpost text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            signpostTextObject.SetActive(false); // Hide the signpost text
        }
    }

    private IEnumerator SetSignpostText()
    {
        yield return null;
        Debug.Log(gatePuzzle.correctSequence[0]);
        signpostText.text = $"Find chest {gatePuzzle.correctSequence[0]}, chest {gatePuzzle.correctSequence[1]}, then finally chest {gatePuzzle.correctSequence[2]}";
        signpostTextObject.SetActive(false);
    }
}
