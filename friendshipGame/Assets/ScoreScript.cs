using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    void Update() {
        if (score==5) {
          // win
        }        
    }

    public void AddScore() {
      score++;
      Debug.Log("Score: " + score);
    }
}
