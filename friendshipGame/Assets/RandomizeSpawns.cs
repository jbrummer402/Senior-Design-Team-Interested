using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RandomizeSpawns : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] List<GameObject> badges;

    private List<Vector2> objectSpawns = new List<Vector2>();
    private System.Random rng = new System.Random();

    void Awake() {
      for (int i = 0; i < objects.Count; i++) {
        var temp = objects[i].GetComponent<RectTransform>();
        Debug.Log("temp:" + temp);
        Debug.Log("anchoredPos:" + temp.anchoredPosition);
        objectSpawns.Add(temp.anchoredPosition);
      }

      var shuffledSpawns = objectSpawns.OrderBy(a => rng.Next()).ToList();
      // objectSpawns.Shuffle();

      for (int i = 0; i < objects.Count; i++) {
        objects[i].GetComponent<RectTransform>().anchoredPosition = shuffledSpawns[i];
      }

      for (int i = 0; i < badges.Count; i++) {
        badges[i].GetComponent<Image>().color = new Color32(0,0,0,255);
      }
    }
}
