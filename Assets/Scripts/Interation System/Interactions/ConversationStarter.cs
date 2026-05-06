using UnityEngine;
using DialogueEditor;

public class ConversationStarter: MonoBehaviour, IInteractable
{
    [SerializeField] private NPCConversation myConversation;

    public void Interact()
    {
        StartConversation();
    }

    public void StartConversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }

    public string GetInteractionText()
    {
        return "Talk";
    }
}
