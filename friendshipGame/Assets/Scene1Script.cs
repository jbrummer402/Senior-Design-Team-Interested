using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System;

public class Scene1Script : MonoBehaviour
{

    public class Dialog
    {
        public class Entry
        {
            public class Option
            {
                public string id;
                public string title;
                public string response;
            }

            public string who;
            public string response;
            public IList<Option> options;
        }

        public int scene;
        public IDictionary<string, Entry> entries;

        public static Dialog CreateFromJSON(string jsonString) {
            return JsonConvert.DeserializeObject<Dialog>(jsonString);
        }

        public Boolean HasEntry(string id) {
            return entries.ContainsKey(id);
        }

        public Entry GetEntry(string id) {
            if(!HasEntry(id))
                return null;
            return entries[id];
        }

    }

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
    private string Scene1Pos = "1";
    private Queue<string> Sentences;
    private Dialog dialog;

    // Start is called before the first frame update
    void Start() {
        ///TODO load from a file
        string jsonDialog = @"{
	        ""scene"": 1,
	        ""entries"": {
		        ""start"": {
			        ""response"": ""Time to get dressed. What outfit would you like to wear?"",
			        ""options"": [
				        {
					        ""id"": ""start"",
					        ""title"": ""Tuxedo and a suit"",
					        ""response"": ""That seems a bit much for the occasion. A more casual outfit would be better suited.""
				        },
				        {
					        ""id"": ""breakfast"",
					        ""title"": ""Jeans/khakis and a polo/button-up shirt""
				        },
				        {
					        ""id"": ""breakfast"",
					        ""title"": ""Shorts and a t-shirt"",
					        ""response"": ""It may be a better idea to be slightly better dressed for the occasion.""
				        }
			        ]
		        },
		        ""breakfast"": {
			        ""response"": ""It's time for breakfast. What do you want to eat?"",
			        ""options"": [
				        {
					        ""id"": ""packingGame"",
					        ""title"": ""Fruit"",
					        ""image"": ""apple"",
					        ""response"": ""That is a good and healthy choice. You feel ready to take on the day!""
				        },
				        {
					        ""id"": ""packingGame"",
					        ""title"": ""Toast"",
					        ""response"": ""That wasn't a bad choice, but the apple would have been a healthier option.""
				        },
				        {
					        ""id"": ""breakfast"",
					        ""title"": ""Candy bar"",
					        ""response"": ""A healthier choice would be a better way to start your day.""
				        }
			        ]
		        },
		        ""packingGame"": {
			        ""scene"": ""packingGame"",
			        ""response"": ""Good job packing! Are you ready to brush your teeth now?"",
			        ""options"": [
				        {
					        ""id"": ""hygeineGame"",
					        ""title"": ""Yes!""
				        }
			        ]
		        },
		        ""hygeineGame"": {
			        ""scene"": ""hygeineGame"",
			        ""response"": ""You feel fresh and clean!\fYour destination is 30 minutes away. When should you leave to arrive on time by 11AM?"",
			        ""options"": [
				        {
					        ""id"": ""end"",
					        ""title"": ""10:20"",
					        ""response"": ""You get there 10 minutes before everyone. You can leave a bit later and still arrive on time in the future.""
				        },
				        {
					        ""id"": ""end"",
                            ""title"": ""10:30"",
					        ""response"": ""You show up on time and begin studying.""
				        },
				        {
					        ""id"": ""end"",
                            ""title"": ""10:40"",
					        ""response"": ""You get there 10 minutes after everyone. It may be better to leave slightly earlier next time.""
				        }
			        ]
		        }
	        }
        }";
        //rootDialog = Dialog.CreateFromJSON(jsonDialog);

        dialog = Dialog.CreateFromJSON(jsonDialog);

        //CharName.text = rootDialog.who;
        Sentences = new Queue<string>();

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);

        PlayerPrefs.SetString(Scene1Pos, "start");

        queueDialog(dialog.GetEntry("start").response);

        ContinueDialogue();
    }

    public void ContinueDialogue() {
        ContinueBtn.gameObject.SetActive(true);
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        string sentence = Sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if(Sentences.Count == 0) {
            ContinueBtn.gameObject.SetActive(false);
            DisplayButtons();
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
        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);

        string currentLocation = PlayerPrefs.GetString(Scene1Pos);


        Dialog.Entry e = dialog.GetEntry(currentLocation);
        if(e == null) {
            return;
        }
        ///TODO Support more than 3 buttons
        List<string> buttonTitles = new List<string>();
        foreach(Dialog.Entry.Option r in e.options) {
            buttonTitles.Add(r.title);
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

    void ClickHandler(int Pressed) {
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);

        Dialog.Entry entry = dialog.GetEntry(currentLocation);
        Dialog.Entry.Option option = entry.options[Pressed - 1];
        currentLocation = option.id;

        if(option.response != null) {
            queueDialog(option.response);
        }

        PlayerPrefs.SetString(Scene1Pos, currentLocation);
        if(dialog.HasEntry(currentLocation))
            queueDialog(dialog.GetEntry(currentLocation).response);
        ContinueDialogue();
    }

    void queueDialog(string text) {
        foreach(string sentence in text.Split('\f')) {
            Sentences.Enqueue(sentence);
        }
    }

    void enterMicrogame(string gameScene) {
        SceneManager.LoadScene(gameScene);
    }

    void incorrectAnswer(string reasoning) {

    }
}
