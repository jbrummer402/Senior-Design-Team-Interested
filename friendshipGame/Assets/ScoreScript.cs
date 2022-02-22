using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private int numCorrectObjects;
    [SerializeField] private GameObject endScreen;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    void Update() {
        if (score==numCorrectObjects) {
          endScreen.SetActive(true);
        }        
    }

    public void AddScore() {
      score++;
      Debug.Log("Score: " + score);
    }
}
