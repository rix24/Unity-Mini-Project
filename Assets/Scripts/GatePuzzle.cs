using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GatePuzzle : MonoBehaviour
{
    public Button[] buttons; // Assign 3 buttons in inspector
    public List<int> correctSequence = new List<int> { 2, 3, 1 };
    private List<int> inputSequence = new List<int>();

    public GameObject gateToOpen; // Assign gameObject gate
    private GateController gateController;

    public static GatePuzzle instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gateController = gateToOpen.GetComponent<GateController>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonID = i + 1;
            buttons[i].onClick.AddListener(() => OnButtonPressed(buttonID));
        }
    }

    public void OnButtonPressed(int id)
    {
        Debug.Log("Button " + id + " pressed");
        inputSequence.Add(id);

        if (inputSequence.Count > correctSequence.Count)
        {
            inputSequence.Clear(); // Too many presses = reset
            return;
        }

        if (inputSequence.Count == correctSequence.Count)
        {
            CheckSequence();
        }
        

        if (inputSequence.Count == correctSequence.Count)
        {
            gateController.OpenGate(); // Open the gate

            // play succes tune
            inputSequence.Clear(); // Reset after success if needed
        }
    }

    private void CheckSequence()
    {
        for (int i = 0; i < inputSequence.Count; i++)
        {
            if (inputSequence[i] != correctSequence[i])
            {
                Debug.Log("Wrong sequence");
                inputSequence.Clear(); // Wrong order = reset
                // play wrong sound
                // have takedamage
                return;
            } else
            {
                Debug.Log("Correct Sequence");
                inputSequence.Clear(); // Reset after success if needed
                gateController.OpenGate(); // Open the gate
                return;
            }
        }
    }
}

