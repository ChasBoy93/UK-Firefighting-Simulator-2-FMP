using UnityEngine;

public class PumpControlTutorial : MonoBehaviour
{

    // Firefighter to talk to.
    [SerializeField] GameObject firefighter;
    [SerializeField] GameObject nextFirefighter;


    // Trigger
    [SerializeField] GameObject theTrigger;
    [SerializeField] GameObject theNextTrigger;



    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            firefighter.SetActive(false);
            nextFirefighter.SetActive(true);
            theTrigger.SetActive(false);
            theNextTrigger.SetActive(true);
        }
    }
}
