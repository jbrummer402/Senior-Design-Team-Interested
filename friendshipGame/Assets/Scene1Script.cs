using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class Scene1Script : MonoBehaviour
{
    public class Dialog 
    {
        private string path;
        public string title;
        public string response;
        public string who;
        public string ptr;
        public Dialog[] options;

        public static Dialog CreateFromJSON(string jsonString) {
            Dialog d = JsonConvert.DeserializeObject<Dialog>(jsonString);
            // Dialog d = new Dialog();
            d.fillPaths("1");
            return d;
        }

        public static void fillPaths(Dialog d) {
            if(d.options != null) {
                for(int i=0; i<d.options.Length; ++i){
                    d.options[i].path = d.path + "/" + (i+1);
                    fillPaths(d.options[i]);
                }
            }
        }


        public void fillPaths(string path) {
            this.path = path;
            fillPaths(this);
        }

        public string GetPath() {
            return this.path;
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
    private Dialog rootDialog;

    Dialog getDialog(string path) { 
        //1-1-1 -> rootDialog.options[0].options[0]
        Dialog d = rootDialog;
        string[] pathArr = path.Split('/');
        
        for(int i=1; i<pathArr.Length; ++i) { // Starting at 1 to exclude the root path
            d = d.options[int.Parse(pathArr[i])-1];
        }

        return d;
    }


    // Start is called before the first frame update
    void Start()
    {
        ///TODO load from a file
        string jsonDialog = @"{
	        ""title"":""Begin"",
	        ""response"": ""Time to get dressed. What outfit would you like to wear?"",
	        ""who"": "" "",
	        ""options"":[
		        {
			        ""title"":""Tuxedo and a Suit"",
			        ""response"": ""That seems a bit much for the occasion. A more casual outfit would be better suited.\fWhat outfit would you like to wear?"",
			        ""ptr"": ""1""
		        },
		        {
			        ""title"":""Jeans/khakis and a polo/button-up shirt"",
                    ""response"": ""Good choice! Time to pack now"",
			        ""options"": [
				        {
					        ""title"": ""[packing game placeholder]"",
					        ""response"": ""It's time for breakfast. What do you want to eat?"",
					        ""options"": [
						        {
							        ""title"": ""Fruit"",
							        ""response"": ""That is a good and healthy choice. You feel ready to take on the day!"",
								        ""options"": [
									        {
										        ""title"": ""[hygeine game]"",
										        ""response"": ""It's time to go now. You're destination is 30 minutes away. When should you leave to arrive on time by 11:00"",
										        ""options"": [
											        {
												        ""title"": ""10:20"",
												        ""response"": ""You get there 10 minutes before everyone. It may be better to leave slightly later next time.""
												
											        },
											        {
												        ""title"": ""10:30"",
												        ""response"": ""You show up on time and begin studying.""
												
											        },									
											        {
												        ""title"": ""10:40"",
												        ""response"": ""You get there 10 minutes after everyone. It may be better to leave slightly earlier next time.""
											        }
										        ]
									        }
								        ]
						        },
						        {
							        ""title"": ""Cracker"",
							        ""response"": ""Fruit may have been a better option."",
							        ""ptr"": ""1/2/1/1""
						        },
						        {
							        ""title"": ""Candy bar"",
							        ""response"": ""Fruit may have been a better option."",
							        ""ptr"": ""1/2/1/1""
						        }
					        ]
				        }
			        ]
		        },
		        {
			        ""title"":""Shorts and a t-shirt"",
			        ""response"": ""It may be a better idea to be slightly better dressed for the occasion.\fTime to pack now"",
			        ""ptr"": ""1/2""
		        }
	        ]
        }";
        rootDialog = Dialog.CreateFromJSON(jsonDialog);
        
        CharName.text = rootDialog.who;
        Sentences = new Queue<string>();

        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);
        
        PlayerPrefs.SetString(Scene1Pos, "1");

        queueDialogue(rootDialog);
        
        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        ContinueBtn.gameObject.SetActive(true);
        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
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

    void DisplayButtons() {
        Response1Btn.gameObject.SetActive(false);
        Response2Btn.gameObject.SetActive(false);
        Response3Btn.gameObject.SetActive(false);

        string currentLocation = PlayerPrefs.GetString(Scene1Pos);
       

        ///TODO null check
        Dialog d = getDialog(currentLocation);
        ///TODO Support mre than 3 buttons
        List<string> responses = new List<string>();
        foreach(Dialog option in d.options) {
            if(option.ptr != null && option.title == null)
                responses.Add(getDialog(option.ptr).title);
            else
                responses.Add(option.title);
            
        }

        if(responses.Count > 0) {
            Response1Btn.gameObject.SetActive(true);
            Response1Text.text = responses[0];

        }
        if(responses.Count > 1) {
            Response2Btn.gameObject.SetActive(true);
            Response2Text.text = responses[1];
        }
        if(responses.Count > 2) {
            Response3Btn.gameObject.SetActive(true);
            Response3Text.text = responses[2];
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

        
        currentLocation += "/" + Pressed;
        Dialog d = getDialog(currentLocation);

        queueDialogue(d);
        
        if(d.ptr != null) {
            currentLocation = getDialog(d.ptr).GetPath();
        }
        
        PlayerPrefs.SetString(Scene1Pos, currentLocation);
        ContinueDialogue();

        //ContinueDialogue();
    }

    void queueDialogue(Dialog d)
    {
        /*if(d.ptr != null) {
            d = getDialog(d.ptr);
        }*/
        if(d.who != "")
            CharName.text = d.who;            
        if(d.response == null || d.response == "") {
            Sentences.Enqueue("A SENTENCE WAS NULL HOW DID THIS HAPPEN");
        }
        else foreach (string sentence in d.response.Split('\f'))
        {
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
