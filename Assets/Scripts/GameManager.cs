using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InteractionController interactionController;
    private Player player;
    private InventoryManager inventoryManager;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, Mathf.Infinity, 64);

            if (hit.collider != null && hit.collider.tag == "Interactable")
            {
                if (interactionController != null)
                {
                    interactionController.Interact();
                }
                else
                {
                    Debug.LogError("InteractionController not assigned!");
                }
            }
        }
    }
}
