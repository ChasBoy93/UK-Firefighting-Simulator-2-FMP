using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DoorAnimation : MonoBehaviour, IInteractable
{
	public Animator animator;
	public string onTriggerEnterParameterName;
	public string onTriggerExitParameterName;
	public AudioClip OpenSound;
	public AudioClip CloseSound;
	bool isDoorOpen;


	void Start()
	{
		if(animator == null)
		{
			animator = GetComponent<Animator>();
			if(animator == null)
			{

			}
		}
	}

	public void Interact()
	{
		if(isDoorOpen)
		{
			CloseDoor();
		}
		else
		{
			OpenDoor();
		}
    }

    public string GetInteractionText()
    {
        if (isDoorOpen)
		{
            return "Close Pump Bay Door";
        }
		
		return "Open Pump Bay Door";
    }


    /*void OnTriggerEnter()
	{
		if(onTriggerEnterParameterName != null)
		{
			gameObject.GetComponent<AudioSource>().PlayOneShot(OpenSound);
			animator.SetTrigger(onTriggerEnterParameterName);
		}
	}*/

    void OpenDoor()
	{

            gameObject.GetComponent<AudioSource>().PlayOneShot(OpenSound);
            animator.SetTrigger(onTriggerEnterParameterName);
            isDoorOpen = true;
    }

	void CloseDoor()
	{
			gameObject.GetComponent<AudioSource>().PlayOneShot(CloseSound);
			animator.SetTrigger(onTriggerExitParameterName);
			isDoorOpen = false;
    }

    void OnTriggerExit()
	{
        if (tag == "Fire Appliance" && onTriggerExitParameterName != null)
		{
            StartCoroutine(CloseTheDoor());
        }
    } 

	IEnumerator CloseTheDoor()
	{
		yield return new WaitForSeconds(5);
        gameObject.GetComponent<AudioSource>().PlayOneShot(CloseSound);
        animator.SetTrigger(onTriggerExitParameterName);
    }
}
