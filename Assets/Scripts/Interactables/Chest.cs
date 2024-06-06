using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite openSprite, closedSprite;
    public Item requiredKey;
    private InventoryManager inventoryManager;

    private bool isOpen;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void Interact()
    {
        Debug.Log("INTERACT has been called");
        if (!isOpen && inventoryManager.HasKey(requiredKey))
        {
            Debug.Log("Chest can be opened with key");
            OpenChest();
            inventoryManager.RemoveItem(requiredKey);
        }
        else
        {
            Debug.Log("Chest cannot be opened");
        }
    }


    private void OpenChest()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;

        AudioManager.instance.PlayAudio(AudioManager.instance.openChest);

        LootBag lootBag = GetComponent<LootBag>();
        if (lootBag == null)
        {
            Debug.Log("No LootBag component found on the chest.");
            return;
        }

        lootBag.InstantiateLoot(transform.position);
    }

    public void StopInteract() { }
}
