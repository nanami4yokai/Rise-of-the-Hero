using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator animator;
    public Item requiredKey;
    private InventoryManager inventoryManager;
    private Collider2D gateCollider;
    private Player player;

    private bool isOpen;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        player = FindObjectOfType<Player>();
        gateCollider = GetComponent<Collider2D>();
    }

    public void Interact()
    {
        Debug.Log("INTERACT has been called");
        if (!isOpen && inventoryManager.HasKey(requiredKey))
        {
            Debug.Log("Gate can be opened with key");
            OpenGate();
        }
        else
        {
            Debug.Log("Gate cannot be opened");
        }
    }

    private void OpenGate()
    {
        isOpen = true;
        animator.SetTrigger("OpenGate");
        AudioManager.instance.PlayAudio(AudioManager.instance.openGate);
        StartCoroutine(WaitForAnimationAndDisableCollider());
    }

    private IEnumerator WaitForAnimationAndDisableCollider()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animator.IsInTransition(0));

        gateCollider.enabled = false;

        Debug.Log("Removing key from inventory");
        inventoryManager.RemoveItem(requiredKey);

        Debug.Log("Gate is now open and collider is disabled");
    }

    public void StopInteract() { }
}
