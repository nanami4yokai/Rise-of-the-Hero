using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public Item.EquipSlotType allowedEquipSlot;
    public Item.ItemType? allowedEquipType;
    public Item.ItemType_Category? allowedEquipCategory;

    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

            // Check if the item's equip slot matches the allowed slot type of the slot
            if (inventoryItem != null && (inventoryItem.item.equipSlot == allowedEquipSlot || allowedEquipSlot == Item.EquipSlotType.None))
            {
                // Check if the item's type and category match the slot's allowed type and category
                if ((allowedEquipType == null || inventoryItem.item.type == allowedEquipType) &&
                    (allowedEquipCategory == null || inventoryItem.item.typeCategory == allowedEquipCategory))
                {
                    // Check if the slot already contains an item of the same type
                    if (GetComponentInChildren<InventoryItem>() == null)
                    {
                        inventoryItem.parentAfterDrag = transform;
                    }
                }
            }
        }
    }
}