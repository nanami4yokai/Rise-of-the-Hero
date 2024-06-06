using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;
    public Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    public void InitialiseItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.Log("newItem is null");
            return;
        }

        if (newItem.image == null)
        {
            Debug.Log("newItem.image is null");
            return;
        }

        if (image == null)
        {
            Debug.Log("image is null");
            return;
        }

        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }


    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find the closest slot and snap the item to it
        foreach (InventorySlot slot in InventoryManager.instance.inventorySlots)
        {
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(slotRect, Input.mousePosition))
            {
                transform.SetParent(slotRect); // Set parent to the slot
                transform.localPosition = Vector3.zero;
                image.raycastTarget = true;
                return; // Stop searching for slots
            }
        }

        // If the item is not dropped into a slot, return it to its original position
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }

    public void UseItem()
    {
        Debug.Log("Using item: " + item.itemName);
        if (item.type == Item.ItemType.Consumable)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.UseConsumable(item);
            }
        }
    }

}