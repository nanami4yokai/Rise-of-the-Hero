using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractable
{
    private IInteractable interactable;

    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable newInteractable = collision.GetComponent<IInteractable>();
        if (newInteractable != null)
        {
            interactable = newInteractable;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable exitedInteractable = collision.GetComponent<IInteractable>();
        if (exitedInteractable != null && exitedInteractable == interactable)
        {
            interactable.StopInteract();
            interactable = null;
        }
    }


    public void StopInteract() { }
}
