using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
      isPaused = true;
    }

    public void Unpause() {
      isPaused = false;
    }

    public void Pause() {
      isPaused = true;
    }
}
