using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer = 6;
    public Transform playerCamera;

    public InteractionUI interactionUI;

    private IInteractable _currentInteractable;
    private Outline _currentOutline;

    void Update()
    {
        CheckForInteractable();

        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    ResetOutline();

                    _currentInteractable = interactable;
                    _currentOutline = hit.collider.GetComponent<Outline>();

                    if (_currentOutline != null)
                        _currentOutline.enabled = true;

                    interactionUI.Show(_currentInteractable.GetInteractionText());
                }

                return;
            }
        }

        ResetOutline();
    }

    private void ResetOutline()
    {
        if (_currentOutline != null)
        {
          _currentOutline.enabled = false;
        }


        interactionUI.Hide();

        _currentInteractable = null;
        _currentOutline = null;
    }
}