using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class RadioSystem : MonoBehaviour
{
    public AudioSource radioSource;
    public TMP_Text subtitleText;
    public GameObject subtitleContainer;

    private Queue<RadioMessage> messageQueue = new Queue<RadioMessage>();
    private bool isPlaying = false;

    public void PlayRadio(RadioMessage message)
    {
        if (message == null)
        {
            return;
        }

            messageQueue.Enqueue(message);

        if (!isPlaying)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    IEnumerator ProcessQueue()
    {
        isPlaying = true;

        while (messageQueue.Count > 0)
        {
            RadioMessage message = messageQueue.Dequeue();

            subtitleContainer.SetActive(true);

            if (message.controlClip != null)
            {
                radioSource.clip = message.controlClip;
                radioSource.Play();
                subtitleText.text = message.controlSubtitle;

                yield return new WaitForSeconds(message.controlClip.length);
            }

            if (message.responseClip != null)
            {
                yield return new WaitForSeconds(message.responseDelay);

                radioSource.clip = message.responseClip;
                radioSource.Play();
                subtitleText.text = message.responseSubtitle;

                yield return new WaitForSeconds(message.responseClip.length);
            }
        }

        subtitleText.text = "";
        subtitleContainer.SetActive(false);

        isPlaying = false;
    }
}