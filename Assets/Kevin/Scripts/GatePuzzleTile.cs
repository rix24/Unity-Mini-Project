using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GatePuzzleTile : MonoBehaviour
{
    private TileBase originalTile;
    private AudioSource playerAudioSource;
    private int secondsPassed = 0;
    private bool isGameActive = true;
    private List<int> correctSequence = new List<int> { 2, 3, 1 };

    public Tilemap tilemap;
    public TileBase replaceTile;
    public Vector3Int tilePosGate = new Vector3Int(-3, 1, 0);
    public List<Vector3Int> buttonTilePositions;
    public bool isRandomised = true;
    
    public List<int> currentSequence = new List<int>();

    // Lazy Way
    public AudioClip win;
    public AudioClip buttonPress;
    public AudioClip openGate;
    public AudioClip fail;
    public TextMeshProUGUI winLabel;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (isRandomised)
        {
            SortSequence(correctSequence);
        }
        // hardwired by position could of used tile.name etc

        buttonTilePositions = new List<Vector3Int> { new Vector3Int(-9, -1, 0), new Vector3Int(-7, -1, 0), new Vector3Int(-4, -1, 0) };
        StartCoroutine(SecondsTimer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<TilemapCollider2D>() != null)
        {
            if (collision.contacts.Length > 0)
            {
                Vector3 contactWorldPos = collision.contacts[0].point;
                Vector3Int tilePos = tilemap.WorldToCell(contactWorldPos);
                
                if (tilePos == tilePosGate) { return; }

                int index = buttonTilePositions.IndexOf(tilePos) + 1;

                if (!currentSequence.Contains(index)  && index != 0)
                {
                    currentSequence.Add(index);
                }

                TileBase tile = tilemap.GetTile(tilePos);

                if (tile != null)
                {
                    originalTile = tile; // doesn't matter if we overwrite for now
                    playerAudioSource.PlayOneShot(buttonPress);
                    tilemap.SetTile(tilePos, replaceTile);
                }


                if (currentSequence.Count == correctSequence.Count)
                {
                    for (int i = 0; i < currentSequence.Count; i++)
                    {
                        if (currentSequence[i] != correctSequence[i])
                        {
                            playerAudioSource.PlayOneShot(fail);
                            ResetAllButtons();
                            return;
                        }
                    }

                    tilemap.SetTile(tilePosGate, null);
                    Debug.Log("Gate opened!");
                    
                    playerAudioSource.PlayOneShot(openGate);
                    // could run a coroutine to seperate

                    currentSequence.Clear();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            if (isGameActive)
            {
                isGameActive = false;
                playerAudioSource.PlayOneShot(win);
                winLabel.text = $"Win! {secondsPassed} sec";
                winLabel.gameObject.SetActive(true);
            }
        }
    }

    private void SortSequence(List<int> list)
    {
        int limit = list.Count;

        for (int i = 0; i < limit; i++)
        {
            int j = Random.Range(i, limit);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private void ResetAllButtons()
    {
        foreach (Vector3Int position in buttonTilePositions)
        {
            tilemap.SetTile(position, originalTile);
        }

        currentSequence.Clear();

        Debug.Log("Wrong sequence, reset.");
    }

    private IEnumerator SecondsTimer()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1f);
            secondsPassed++;
        }
    }
}