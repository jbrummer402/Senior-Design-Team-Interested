using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptObjects : MonoBehaviour
{
   public GameObject p3;

   void OnCollisionEnter(Collision col) 
   { 
        Debug.Log(col);
        if(col.gameObject.name == "Backpack")
        {
           Debug.Log("Collision detected");
           p3.GetComponent<Renderer>().enabled = false;
        }
   } 
}