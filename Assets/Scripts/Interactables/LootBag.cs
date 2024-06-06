using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<GameObject> droppedItemPrefabs = new List<GameObject>();
    public GameObject lootContainer;

    [Range(1, 100)]
    public int randomNumber;

    private void Start()
    {
        // Deactivate all loot items initially
        foreach (var itemPrefab in droppedItemPrefabs)
        {
            itemPrefab.SetActive(false);
        }
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        foreach (GameObject prefab in droppedItemPrefabs)
        {
            if (Random.Range(1, 101) <= randomNumber)
            {
                GameObject lootGameObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

                // Parent the loot item to the loot container
                lootGameObject.transform.SetParent(lootContainer.transform);

                // Activate the loot item
                lootGameObject.SetActive(true);

                // Move the loot item from the chest position to its target position
                StartCoroutine(MoveToPosition(lootGameObject.transform, GetRandomTargetPosition()));
            }
        }

        Debug.Log("Loot dropped");
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 targetPosition)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private Vector3 GetRandomTargetPosition()
    {
        float range = 1.2f; // !!! play around with value !!!
        Vector3 chestPosition = transform.position;
        Vector3 randomOffset = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0f);
        return chestPosition + randomOffset;
    }
}
