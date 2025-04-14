using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GatePuzzle : MonoBehaviour
{
    public Button[] buttons; // Assign 3 buttons in inspector
    public List<int> correctSequence = new List<int> { 2, 3, 1 };
    public GameObject gateToOpen; // Assign gameObject gate

    private List<int> inputSequence = new List<int>();

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonID = i + 1;
            buttons[i].onClick.AddListener(() => OnButtonPressed(buttonID));
        }
    }

    void OnButtonPressed(int id)
    {
        inputSequence.Add(id);

        if (inputSequence.Count > correctSequence.Count)
        {
            inputSequence.Clear(); // Too many presses = reset
            return;
        }

        for (int i = 0; i < inputSequence.Count; i++)
        {
            if (inputSequence[i] != correctSequence[i])
            {
                inputSequence.Clear(); // Wrong order = reset
                // play wrong sound
                // have takedamage
                return;
            }
        }

        if (inputSequence.Count == correctSequence.Count)
        {
            gateToOpen.SetActive(false); // Open gate
            
            // play succes tune
            inputSequence.Clear(); // Reset after success if needed
        }
    }
}

