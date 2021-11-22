using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerIngame : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameContainer;
    [SerializeField] private GameObject audioMenu;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenu.gameObject.SetActive(true);
            gameContainer.gameObject.SetActive(false);
            audioMenu.gameObject.SetActive(false);
        }
    }
}
