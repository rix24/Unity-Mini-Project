using System.Collections;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    private AudioSource victoryAudio;

    private void Start()
    {
        victoryAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            victoryAudio.Play();
            StartCoroutine(goToWinScene());
        }
    }

    private IEnumerator goToWinScene()
    {
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen");
    }
}
