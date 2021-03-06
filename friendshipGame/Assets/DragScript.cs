using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject Backpack;
    [SerializeField] ScoreScript ScoreScript;
    [SerializeField] PauseGame PauseGame;
    [SerializeField] private GameObject badge;
    [SerializeField] private Image negativeFeedbackBox;
    [SerializeField] private TMP_Text negativeFeedback;
    [SerializeField] private string negativeFeedbackText;

    private RectTransform rectTransform;
    private RectTransform backpackRectTransform;
    private Vector2 spawnPoint;
    private List<string> accepted = new List<string>();

    private void Awake() {
      rectTransform = GetComponent<RectTransform>();
      backpackRectTransform = Backpack.GetComponent<RectTransform>();
      accepted.Add("Textbooks");
      accepted.Add("PencilCase");
      accepted.Add("Notebooks");
      accepted.Add("Laptop");
    }

    private void Start() {
      spawnPoint = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
      Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) {
      Debug.Log("OnDrag");
      if (!PauseGame.isPaused) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
      }
    }

    public void OnEndDrag(PointerEventData eventData) {
      Debug.Log("OnEndDrag");
      Vector3[] v = new Vector3[4];
      backpackRectTransform.GetWorldCorners(v);

      Debug.Log("Backpack Min1: " + v[0]);
      Debug.Log("Backpack Max1: " + v[2]);

      var backpackCollider = Physics2D.OverlapArea(v[0], v[2]);

      GetComponent<Image>().color = new Color32(255,255,225,255);

      if (backpackCollider && backpackCollider.gameObject.name == gameObject.name) {
        Debug.Log("Collision");
        Debug.Log("GameObject Name: " + gameObject.name);
        if (accepted.Contains(gameObject.name)) {
          ScoreScript.AddScore();
          badge.GetComponent<Image>().color = new Color32(255,255,255,255);
          gameObject.SetActive(false);
        } else {
          PauseGame.Pause();
          rectTransform.anchoredPosition = spawnPoint;
          negativeFeedback.text = negativeFeedbackText;
          negativeFeedbackBox.gameObject.SetActive(true);
        }
      }
    }

    public void OnPointerDown(PointerEventData eventData) {
      Debug.Log("OnPointerDown");
    }
}