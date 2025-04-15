using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton;
    public GameObject menuUI;
    public Dictionary<string, int> userScores = new Dictionary<string, int>();

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        startButton.onClick.AddListener(SubmitUser);
    }

    private void SubmitUser()
    {
        if (nameInputField.text == "") { return; }
        if (!userScores.ContainsKey(nameInputField.text))
        {
            userScores[nameInputField.text] = 1000;
        }
        menuUI.SetActive(false);
        // Debug.Log(userScores[nameInputField.text]);

        SceneManager.LoadScene(1);
    }
}

