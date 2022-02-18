using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject Backpack;
    [SerializeField] ScoreScript ScoreScript;

    private RectTransform rectTransform;
    private RectTransform backpackRectTransform;
    private Vector2 spawnPoint;
    private List<string> accepted = new List<string>();

    private void Awake() {
      rectTransform = GetComponent<RectTransform>();
      backpackRectTransform = Backpack.GetComponent<RectTransform>();
      accepted.Add("Textbooks");
      accepted.Add("PencilCase");
    }

    private void Start() {
      spawnPoint = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
      Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) {
      Debug.Log("OnDrag");
      rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
      Debug.Log("OnEndDrag");
      Vector3[] v = new Vector3[4];
      backpackRectTransform.GetWorldCorners(v);

      Debug.Log("Backpack Min1: " + v[0]);
      Debug.Log("Backpack Max1: " + v[2]);

      var backpackCollider = Physics2D.OverlapArea(v[0], v[2]);
      Debug.Log("backpack collider: " + backpackCollider);
      Debug.Log("backpack collider tag: " + backpackCollider.gameObject.name);


      if (backpackCollider && backpackCollider.gameObject.name == gameObject.name) {
        Debug.Log("Collision");
        Debug.Log("GameObject Name: " + gameObject.name);
        if (accepted.Contains(gameObject.name)) {
          ScoreScript.AddScore();
          gameObject.SetActive(false);
        } else {
          rectTransform.anchoredPosition = spawnPoint;
        }
      }
    }

    public void OnPointerDown(PointerEventData eventData) {
      Debug.Log("OnPointerDown");
    }
}