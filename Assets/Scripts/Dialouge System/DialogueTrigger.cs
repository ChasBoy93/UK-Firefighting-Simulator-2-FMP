using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    public void Interact()
    {
        TriggerDialogue();
    }

    void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public string GetInteractionText()
    {
        return "Talk to " + dialogue.name;
    }
}
