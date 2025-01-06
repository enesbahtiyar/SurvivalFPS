using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPositon;
    Transform startParent;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag metodu çalıştı");

        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        startPositon = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = this.gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.position = transform.position;
        itemBeingDragged = null;

        if(transform.parent == startParent)
        {
            transform.position = startPositon;
            transform.SetParent(startParent);
        }
        else if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() != null)
        {
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        }
        else
        {
            InventorySystem.Instance.RemoveFromInventoryToWorld(this.gameObject);
        }


        if(!transform.CompareTag("QuickSlot"))
        {
            itemBeingDragged.GetComponent<InventoryItem>().isOnQuickSlot = false;
        }
        if (transform.CompareTag("QuickSlot"))
        {
            itemBeingDragged.GetComponent<InventoryItem>().isOnQuickSlot = true;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
