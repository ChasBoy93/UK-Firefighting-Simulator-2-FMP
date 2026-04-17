using Artemis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    public Queue<string> sentences;
    public GameObject playerCameraStop;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCameraStop.GetComponent<FPController>().enabled = false;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }

}
