using Artemis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    public GameObject playerCameraStop;

    public AudioSource audioSource; 

    private Queue<string> sentences;
    private Dialogue currentDialogue;
    private int sentenceIndex = 0;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        sentenceIndex = 0;

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

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (currentDialogue.audioClips != null &&
            sentenceIndex < currentDialogue.audioClips.Length)
        {
            AudioClip clip = currentDialogue.audioClips[sentenceIndex];

            if (clip != null && audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

        sentenceIndex++;

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

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCameraStop.GetComponent<FPController>().enabled = true;
    }
}