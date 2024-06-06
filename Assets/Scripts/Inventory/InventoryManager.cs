using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    public int maxStackItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    // we put -1 because we wanna indicate that nothing is being selected by default (since indexing starts at 0)
    int selectedSlot = -1;

    private List<Item> inventory = new List<Item>();
    private AudioManager audioManager;

    private Item item;

    public Player player;
    private bool isWeaponEquipped = false;

    private void Awake()
    {
        instance = this;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void RemoveItem(Item item)
    {
        Debug.Log("Attempting to remove item: " + item.itemName);

        // Find the index of the item in the internal inventory list
        int itemIndex = inventory.FindIndex(i => i == item);

        // Check if the item exists in the internal inventory list
        if (itemIndex == -1)
        {
            Debug.LogWarning("Item not found in internal inventory");
            return;
        }

        // Remove the item from the internal inventory list
        inventory.RemoveAt(itemIndex);
        Debug.Log("Item removed from internal inventory");

        // Remove the item from the UI inventory slots
        bool itemRemovedFromSlot = false;
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                if (itemInSlot.count > 1)
                {
                    itemInSlot.count--;
                    itemInSlot.RefreshCount();
                    Debug.Log("Reduced item count in slot");
                }
                else
                {
                    Debug.Log("Removing item from slot in hierarchy: " + itemInSlot.gameObject.name);
                    Destroy(itemInSlot.gameObject);
                    Debug.Log("Item removed from UI slot");
                }
                itemRemovedFromSlot = true;
                break;
            }
        }

        if (!itemRemovedFromSlot)
        {
            Debug.LogWarning("Item not found in any inventory slot");
        }
    }

    private void Update()
    {
        // Use arcade machine buttons A, B, X, and Y for interacting with inventory
        if (Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame || Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            UseSelectedItem(0); // slot 1 button A
        }
        else if (Keyboard.current != null && Keyboard.current.yKey.wasPressedThisFrame || Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            UseSelectedItem(1); // slot 2 button Y
        }
        else if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame || Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            UseSelectedItem(2); // slot 3 button X
        }
        else if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame || Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            UseSelectedItem(3); // slot 4 button B
        }
    }

    void UseSelectedItem(int slotIndex)
    {
        // Check if the provided slot index is valid
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[slotIndex];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item != null)
            {
                if (itemInSlot.item.type == Item.ItemType.Key)
                {
                    if (IsPlayerNearInteractable())
                    {
                        TryToUseKey(itemInSlot.item);
                    }
                    else
                    {
                        Debug.Log("Player is not near an interactable object.");
                    }
                }
                else if (itemInSlot.item.type == Item.ItemType.Consumable)
                {
                    player.UseConsumable(itemInSlot.item);
                    audioManager.PlayAudio(audioManager.drink);
                }
            }
        }
    }

    bool IsPlayerNearInteractable()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(player.transform.position, 1.0f);
        foreach (Collider2D collider in interactables)
        {
            if (collider.GetComponent<IInteractable>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void TryToUseKey(Item key)
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(player.transform.position, 1.0f);
        foreach (Collider2D collider in interactables)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                RemoveItem(key);
                return;
            }
        }
        Debug.Log("No interactable object found.");
    }

    public bool AddItem(Item item)
    {
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.LogError("Inventory slots are not properly initialized.");
            return false;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                // Check if the item's equip slot matches the allowed slot type of the slot
                if (item.equipSlot != Item.EquipSlotType.None && item.equipSlot != slot.allowedEquipSlot)
                {
                    continue;
                }

                SpawnNewItem(item, slot);
                inventory.Add(item);
                UpdatePlayerGear(item, true);
                return true;
            }
            else if (itemInSlot.item == item && item.stackable)
            {
                if (itemInSlot.count < maxStackItems)
                {
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
            else if (itemInSlot.item == null && item.equipSlot == slot.allowedEquipSlot)
            {
                SpawnNewItem(item, slot);
                inventory.Add(item);
                UpdatePlayerGear(item, true);
                return true;
            }
        }
        return false;
    }


    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        if (selectedSlot >= 0 && selectedSlot < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            if (slot != null)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null)
                {
                    Item item = itemInSlot.item;
                    if (use == true)
                    {
                        itemInSlot.count--;
                        if (itemInSlot.count <= 0)
                        {
                            Destroy(itemInSlot.gameObject);
                        }
                        else
                        {
                            itemInSlot.RefreshCount();
                        }

                        inventory.Remove(item);
                        UpdatePlayerGear(item, false);
                    }
                    return item;
                }
            }
        }
        return null;
    }

    public bool HasKey(Item key)
    {
        foreach (Item item in inventory)
        {
            Debug.Log("Comparing sprites:");
            Debug.Log("Item sprite: " + item.image);
            Debug.Log("Key sprite: " + key.image);

            if (item.image == key.image && item.type == Item.ItemType.Key)
            {
                Debug.Log("Key found in inventory");
                return true;
            }
        }
        Debug.Log("Key not found in inventory");
        return false;
    }

    void UpdatePlayerGear(Item item, bool equip)
    {
        if (item.equipSlot == Item.EquipSlotType.Head)
        {
            player.EquipHelmet(equip);
            AudioManager.instance.PlayAudio(AudioManager.instance.gearUp);
        }
        else if (item.equipSlot == Item.EquipSlotType.Body)
        {
            player.EquipBodyArmor(equip);
            AudioManager.instance.PlayAudio(AudioManager.instance.gearUp);
        }
        else if (item.equipSlot == Item.EquipSlotType.Weapon)
        {
            isWeaponEquipped = equip;
            AudioManager.instance.PlayAudio(AudioManager.instance.gearUp);
        }
    }

    public bool IsWeaponEquipped()
    {
        return isWeaponEquipped;
    }

    public bool HasHpPotion()
    {
        foreach (Item item in inventory)
        {
            if (item.type == Item.ItemType.Consumable && item.typeCategory == Item.ItemType_Category.HpPotion)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveHpPotion()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.type == Item.ItemType.Consumable && itemInSlot.item.typeCategory == Item.ItemType_Category.HpPotion)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                inventory.Remove(itemInSlot.item);
                return;
            }
        }
    }
}