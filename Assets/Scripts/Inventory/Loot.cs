using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CircleCollider2D collider;
    [SerializeField] private float moveSpeed;

    public Item item;
    private AudioManager audioManager;

    public void SetItem(Item newItem)
    {
        item = newItem;
        Initialize(item);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if item is initialized
            if (item == null)
            {
                Debug.LogWarning("Item is not initialized.");
                return;
            }

            // Call InitializeItem with the existing item
            Initialize(item);

            bool canAdd = InventoryManager.instance.AddItem(item);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }
        }
    }

    public void Initialize(Item newItem)
    {
        Debug.Log("Initialize method called.");
        if (newItem == null)
        {
            Debug.LogError("Item is null.");
            return;
        }

        this.item = newItem;
        item.image = sr.sprite;


        Debug.Log("Item initialized successfully: " + item.name);
        Debug.Log("Item type: " + item.type);
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(collider);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}