using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject Backpack;

    private RectTransform rectTransform;
    private RectTransform backpackRectTransform;

    private void Awake() {
      rectTransform = GetComponent<RectTransform>();
      backpackRectTransform = Backpack.GetComponent<RectTransform>();
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
      Debug.Log("Obj Max: " + rectTransform.offsetMax);
      Debug.Log("Obj Min: " + rectTransform.offsetMin);
      Debug.Log("Backpack Max: " + backpackRectTransform.offsetMax);
      Debug.Log("Backpack Min: " + backpackRectTransform.offsetMin);
      var backpackCollider = Physics2D.OverlapArea(backpackRectTransform.offsetMin, backpackRectTransform.offsetMax);
      // var collider = Physics2D.OverlapArea(rectTransform.offsetMin, rectTransform.offsetMax);
      // Debug.Log("backpack collider: " + backpackCollider);
      // Debug.Log("collider: " + collider);

      if (backpackCollider) {
        Debug.Log("Collision");
      }

      // if (Physics2D.IsTouching(collider, backpackCollider)) {
      //   Debug.Log("Collided w/ Backpack");
      // }
    }

    public void OnPointerDown(PointerEventData eventData) {
      Debug.Log("OnPointerDown");
    }
}