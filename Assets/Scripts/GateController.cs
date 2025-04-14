using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private List<SpriteRenderer> doorSpriteRenderer = new List<SpriteRenderer>();
    public Sprite doorClosed;
    public Sprite doorOpen;
    private BoxCollider2D gateCollider;

    void Start()
    {        
        doorSpriteRenderer.AddRange(GetComponentsInChildren<SpriteRenderer>());
        gateCollider = GetComponent<BoxCollider2D>();   
        foreach (var spriteRenderer in doorSpriteRenderer)
        {
            spriteRenderer.sprite = doorClosed;
        }
    }

    void Update()
    {
        
    }

    public void OpenGate()
    {        
        foreach (var spriteRenderer in doorSpriteRenderer)
        {
            spriteRenderer.sprite = doorOpen;
        } 
        gateCollider.enabled = false; // Disable the collider to allow passage
    }
}
