using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Scene1Script : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private TMP_Text Response1Text = null;
    [SerializeField] private TMP_Text Response2Text = null;
    [SerializeField] private TMP_Text Response3Text = null;
    [SerializeField] private Button ContinueBtn = null;
    [SerializeField] private Button Response1Btn = null;
    [SerializeField] private Button Response2Btn = null;
    [SerializeField] private Button Response3Btn = null;
    [SerializeField] private TMP_Text PromptText = null;
    [SerializeField] private TMP_Text CharName = null;
    [SerializeField] private Image CharImage = null;
    private string FirstChar = "FirstChar";
    private string Scene1Pos = "Scene1Pos";
    private Queue<string> Sentences;

    // Start is called before the first frame update
    void Start()
    {
        CharName.text = FirstChar;
        string[] initialDialogue = new string[]{"Sample text for typing purposes", "A pretty long sentence just to see how the scrolling thing works and whether or not speed should be adjusted.", "1-3", "1-4"};
        Sentences = new Queue<string>();

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        
        PlayerPrefs.SetString(Scene1Pos, "1");

        queueDialogue(initialDialogue);

        Debug.Log(Sentences);
        
        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        ContinueBtn.gameObject.SetActive(true);
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (Sentences.Count == 0)
        {
            ContinueBtn.gameObject.SetActive(false);
            DisplayButtons();
            return;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        PromptText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            PromptText.text += letter;
            for (int i = 0; i < 5; i++) {
                yield return null;
            }
        }
    }

    void DisplayButtons()
    {
        Response1Btn.gameObject.SetActive(true);
        Response2Btn.gameObject.SetActive(true);
        Response3Btn.gameObject.SetActive(true);

        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        
        switch(currentLocation)
        {
            case "1":
                Response1Text.text = "2";
                Response2Text.text = "3";
                Response3Text.text = "4";
                break;
            case "2":
                Response1Text.text = "5";
                Response2Text.text = "6";
                Response3Text.text = "7";
                break;
        }
    }

    public void ClickOne()
    {
        ClickHandler(1);   
    }

    public void ClickTwo()
    {
        ClickHandler(2);
    }

    public void ClickThree()
    {
        ClickHandler(3);
    }

    void ClickHandler(int Pressed)
    {
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        string[] dialogue;

        switch(currentLocation)
        {
            case "1":
                switch(Pressed)
                {
                    case 1:
                        PlayerPrefs.SetString(Scene1Pos, "2");
                        dialogue = new string[]{"2-1", "2-2"};
                        queueDialogue(dialogue);
                        break;
                    case 2:
                        PlayerPrefs.SetString(Scene1Pos, "3");
                        dialogue = new string[]{"3-1", "3-2", "3-3"};
                        queueDialogue(dialogue);
                        break;
                    case 3:
                        PlayerPrefs.SetString(Scene1Pos, "4");
                        dialogue = new string[]{"4-1", "4-2", "4-3"};
                        queueDialogue(dialogue);
                        break;
                }
                break;
            case "2":
                switch(Pressed)
                {
                    case 1:
                        PlayerPrefs.SetString(Scene1Pos, "5");
                        dialogue = new string[]{"5-1", "5-2"};
                        queueDialogue(dialogue);
                        break;
                    case 2:
                        PlayerPrefs.SetString(Scene1Pos, "6");
                        dialogue = new string[]{"6-1", "6-2", "6-3"};
                        queueDialogue(dialogue);
                        break;
                    case 3:
                        PlayerPrefs.SetString(Scene1Pos, "7");
                        dialogue = new string[]{"7-1", "7-2", "7-3"};
                        queueDialogue(dialogue);
                        break;
                }
                break;
        }

        ContinueDialogue();
    }

    void queueDialogue(string[] dialogue)
    {
        foreach (string sentence in dialogue)
        {
            Debug.Log(sentence);
            Sentences.Enqueue(sentence);
        }
    }

    void enterMicrogame(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }

    void incorrectAnswer(string reasoning)
    {
        
    }
}
