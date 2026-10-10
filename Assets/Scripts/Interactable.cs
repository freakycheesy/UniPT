using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Look,
        Zoom,
        Click,
    }

    public InteractionType type;

    public UnityEvent onInteract;

    public void Interact(InteractionType interactionType)
    {
        if (interactionType == type)
        {
            onInteract.Invoke();
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {

    }
}
