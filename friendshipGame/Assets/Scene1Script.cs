using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class Scene1Script : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private TMP_Text Response1Text = null;
    [SerializeField] private TMP_Text Response2Text = null;
    [SerializeField] private TMP_Text Response3Text = null;
    [SerializeField] private TMP_Text Response4Text = null;
    [SerializeField] private Button ContinueBtn = null;
    [SerializeField] private Button Response1Btn = null;
    [SerializeField] private Button Response2Btn = null;
    [SerializeField] private Button Response3Btn = null;
    [SerializeField] private Button Response4Btn = null;
    [SerializeField] private TMP_Text PromptText = null;
    [SerializeField] private TMP_Text CharName = null;
    [SerializeField] private Image CharImage = null;
    [SerializeField] private string DialogLocation = null;
    [SerializeField] private TMP_InputField TextInputField = null;
    [SerializeField] private Button SubmitTextBtn = null;

    [Header("Debug")]
    [SerializeField] private bool resetDialogPos = false;
    private string FirstChar = "FirstChar";
    private static string Scene1Pos = "Scene1Pos";
    private Queue<string> Sentences;
    private Dialog dialog;
    // private Dictionary<string, string> vars;


    // Start is called before the first frame update
    void Start() {
        print("start");
        ///TODO load from a file
        //string jsonDialog = @"";
        //rootDialog = Dialog.CreateFromJSON(jsonDialog);

        //vars = new Dictionary<string, string>();
        //PlayerPrefs.set

        //dialog = Dialog.CreateFromFile(DialogLocation);
        dialog = Dialog.CreateFromFile("3");


        //CharName.text = rootDialog.who;
        Sentences = new Queue<string>();

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        Response4Btn.gameObject.SetActive(false);

        print(dialog.GetEntry("start").response);
        print(PlayerPrefs.GetString(Scene1Pos));

        if(!PlayerPrefs.HasKey(Scene1Pos) || resetDialogPos)
            PlayerPrefs.SetString(Scene1Pos, "start");

        CharName.text = null;

        queueDialog(dialog.GetEntry(PlayerPrefs.GetString(Scene1Pos)).response);

        ContinueDialogue();
        //ShowDialog(dialog.GetEntry("start").response);
        //DisplayButtons();
    }

    public void SubmitText() {
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        Dialog.Entry e = dialog.GetEntry(currentLocation);
        print(e.input.id);
        //vars.Add(e.input.var, TextInputField.text);
        PlayerPrefs.SetString(e.input.var, TextInputField.text);
        //vars.Add()
        TextInputField.text = "";
        TextInputField.gameObject.SetActive(false);
        SubmitTextBtn.gameObject.SetActive(false);
        PlayerPrefs.SetString(Scene1Pos, e.input.id);
        DisplayButtons();
    }

    public void ContinueDialogue() {
        print("ContinueDialogue");

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        Response4Btn.gameObject.SetActive(false);
        ContinueBtn.gameObject.SetActive(true);
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        string sentence = Sentences.Dequeue();
        Dialog.Entry entry = dialog.GetEntry(currentLocation);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if(Sentences.Count == 0) {
            ContinueBtn.gameObject.SetActive(false);
            if(entry.input != null) {
                ContinueBtn.gameObject.SetActive(false);
                TextInputField.gameObject.SetActive(true);
                SubmitTextBtn.gameObject.SetActive(true);
            }
            else {
                DisplayButtons();
            }
            //
            return;
        }
    }

    IEnumerator TypeSentence(string sentence) {
        PromptText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            PromptText.text += letter;
            for(int i = 0; i < 5; i++) {
                yield return null;
            }
        }
    }

    void DisplayButtons() {
        print("DisplayButtons");

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        Response4Btn.gameObject.SetActive(false);

        string currentLocation = PlayerPrefs.GetString(Scene1Pos);


        Dialog.Entry e = dialog.GetEntry(currentLocation);
        if(e == null) {
            return;
        }
        ///TODO Support more than 3 buttons
        List<string> buttonTitles = new List<string>();
        foreach(Dialog.Entry.Option r in e.options) {
            buttonTitles.Add(fillVars(r.title));
        }

        if(buttonTitles.Count > 0) {
            Response1Btn.gameObject.SetActive(true);
            Response1Text.text = buttonTitles[0];

        }
        if(buttonTitles.Count > 1) {
            Response2Btn.gameObject.SetActive(true);
            Response2Text.text = buttonTitles[1];
        }
        if(buttonTitles.Count > 2) {
            Response3Btn.gameObject.SetActive(true);
            Response3Text.text = buttonTitles[2];
        }
        if(buttonTitles.Count > 3) {
            Response4Btn.gameObject.SetActive(true);
            Response4Text.text = buttonTitles[3];
        }
    }

    public void ClickOne() {
        ClickHandler(1);
    }

    public void ClickTwo() {
        ClickHandler(2);
    }

    public void ClickThree() {
        ClickHandler(3);
    }

    public void ClickFour() {
        ClickHandler(4);
    }

    void ClickHandler(int Pressed) {
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);

        Dialog.Entry entry = dialog.GetEntry(currentLocation);
        Dialog.Entry.Option option = entry.options[Pressed - 1];

        if(entry.var != null && option.val != null) {
            PlayerPrefs.SetString(entry.var, option.val);
        }

        currentLocation = option.id;
        PlayerPrefs.SetString(Scene1Pos, currentLocation);

        entry = dialog.GetEntry(currentLocation);
        if(entry == null)
            return;

        if(entry.character != null) {
            CharName.text = entry.character;
        }
        
        if(option.response != null) {
            queueDialog(option.response);
        }

        queueDialog(dialog.GetEntry(currentLocation).response);
        ContinueDialogue();

        string microgame = dialog.GetEntry(currentLocation).microgame;
        if(microgame != null) {
            enterMicrogame(microgame);
        }


    }


    string fillVars(string text) { // Replaces all text in brackets with the corresponding variable from PlayerPrefs
        Regex regex = new Regex(@"\[([A-Za-z]+)\]");

        foreach(Match match in regex.Matches(text)) {
            string varName = match.Value.Trim('[').Trim(']');
            text = text.Replace(match.Value, PlayerPrefs.GetString(varName));
        }
        return text;
    }

    void queueDialog(string text) {
        print(text);
        foreach(string sentence in text.Split('\f')) {
            Sentences.Enqueue(fillVars(sentence));
        }
    }

    void enterMicrogame(string gameScene) {
        SceneManager.LoadSceneAsync(gameScene);
    }

    void incorrectAnswer(string reasoning) {

    }
}
